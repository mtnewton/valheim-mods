using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;

namespace ExpertMode
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class ExpertModePlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.expertmode";

        private const string NAME = "ExpertMode";

        private const string VERSION = "1.2.0";

        private static ManualLogSource logger;

        private static ConfigFile config;

        private static ConfigEntry<int> globalLevel;
        private static ConfigEntry<int> globalLootLevel;

        private static List<KeyValuePair<string, ConfigEntry<int>>> enemyLevelOverrides = new List<KeyValuePair<string, ConfigEntry<int>>>();
        private static List<KeyValuePair<string, ConfigEntry<int>>> enemyLootLevelOverrides = new List<KeyValuePair<string, ConfigEntry<int>>>();

        void Awake()
        {
            logger = Logger;
            config = Config;

            globalLevel = Config.Bind(NAME + ".Global", "global_level", 3,
                new ConfigDescription(
                    "What level should enemies be? \n" + 
                    "0 prevents changing levels unless the specific enemy has a level override above 0. \n" + 
                    "Stars = Level - 1",
                    new AcceptableValueRange<int>(0, int.MaxValue)
                )
            );

            globalLootLevel = Config.Bind(NAME + ".Global", "global_loot_level", 1,
                new ConfigDescription(
                    "When enemies drop loot, they should act as if they are this level. \n" +
                    "0 prevents changing loot levels unless the specific enemy has an loot level override above 0. \n" + 
                    "Be careful with higher values. Suggest staying below 5.\n" +
                    "Game default would be same as global_level",
                    new AcceptableValueRange<int>(0, int.MaxValue)
                )
            );

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "Awake")]
        static void SetEnemyLevel(ref Character __instance)
        {
            if (__instance.m_name.StartsWith("$enemy_"))
            {
                string enemyName = __instance.m_name.Substring(7, __instance.m_name.Length - 7);
                int level = GetLevelForEnemy(enemyName);
                if (level > 0)
                {
                    __instance.SetLevel(level);
                    logger.LogInfo(enemyName + " Loaded. Setting to level " + level + " (" + (level - 1) + " stars)");
                }
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(CharacterDrop), "GenerateDropList")]
        static void SetEnemyLootLevel(ref CharacterDrop __instance)
        {
            if (__instance.m_character.m_name.StartsWith("$enemy_"))
            {
                string enemyName = __instance.m_character.m_name.Substring(7, __instance.m_character.m_name.Length - 7);
                int level = GetLootLevelForEnemy(enemyName);
                if (level > 0)
                {
                    __instance.m_character.m_level = level;
                    logger.LogInfo("Setting " + enemyName + " to level " + level + " before it generates drops");
                }
            }
        }

        public static int GetLevelForEnemy(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<int>> enemyLevelConfig = enemyLevelOverrides.Find(enemyLevelOverride => enemyLevelOverride.Key == enemyName);

            if (enemyLevelConfig.Equals(default(KeyValuePair<string, ConfigEntry<int>>)))
            {
                enemyLevelConfig = BindAndGetConfigForEnemyLevel(enemyName);
            }

            if (enemyLevelConfig.Value.Value == 0)
            {
                return globalLevel.Value;
            }

            return enemyLevelConfig.Value.Value;
        }

        public static int GetLootLevelForEnemy(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<int>> enemyLootLevelConfig = enemyLootLevelOverrides.Find(enemyLootLevelOverride => enemyLootLevelOverride.Key == enemyName);

            if (enemyLootLevelConfig.Equals(default(KeyValuePair<string, ConfigEntry<int>>)))
            {
                enemyLootLevelConfig = BindAndGetConfigForEnemyLootLevel(enemyName);
            }

            if (enemyLootLevelConfig.Value.Value == 0)
            {
                return globalLootLevel.Value;
            }

            return enemyLootLevelConfig.Value.Value;
        }

        private static KeyValuePair<string, ConfigEntry<int>> BindAndGetConfigForEnemyLevel(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<int>> enemyLevelOverride = new KeyValuePair<string, ConfigEntry<int>>(
                enemyName,
                config.Bind(NAME + ".EnemyLevelOverrides", enemyName + "_level_override", 0,
                    new ConfigDescription(
                        "What level should " + enemyName + "(s) spawn at? \n" +
                        "Set above 0 to override the global_level for " + enemyName + "(s) only. \n",
                        new AcceptableValueRange<int>(0, int.MaxValue)
                    )
                )
            );
            enemyLevelOverrides.Add(enemyLevelOverride);
            return enemyLevelOverride;
        }

        private static KeyValuePair<string, ConfigEntry<int>> BindAndGetConfigForEnemyLootLevel(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<int>> enemyLootLevelOverride = new KeyValuePair<string, ConfigEntry<int>>(
                enemyName,
                config.Bind(NAME + ".EnemyLootLevelOverrides", enemyName + "_loot_level_override", 0,
                    new ConfigDescription(
                        "When " + enemyName + "(s) are killed, loot should drop as if they were this level. \n" +
                        "Set above 0 to override the global_loot_level value for " + enemyName + "(s) only. \n" +
                        "Be careful with higher values. Suggest staying below 5.",
                        new AcceptableValueRange<int>(0, int.MaxValue)
                    )
                )
            );
            enemyLootLevelOverrides.Add(enemyLootLevelOverride);
            return enemyLootLevelOverride;
        }
    }
}
