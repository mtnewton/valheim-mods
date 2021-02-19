using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;

namespace ExpertMode
{
    [BepInPlugin(GUID, "ExpertMode", VERSION)]
    [HarmonyPatch]
    public class ExpertModePlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.expertmode";

        private const string VERSION = "1.0.0";

        private static ManualLogSource logger;

        private static ConfigFile config;

        private static ConfigEntry<uint> globalStarIncrease;

        private static List<KeyValuePair<string, ConfigEntry<uint>>> enemyOverrides = new List<KeyValuePair<string, ConfigEntry<uint>>>();

        void Awake()
        {
            logger = Logger;
            config = Config;

            globalStarIncrease = Config.Bind("ExpertMode.Global", "global_level", (uint)3, "What level should enemies be? Stars = Level - 1");

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo("ItemStacks loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Character), "Awake")]
        static void SetEnemyLevel(Character __instance)
        {
            if (__instance.m_name.StartsWith("$enemy_"))
            {
                string enemyName = __instance.m_name.Substring(7, __instance.m_name.Length - 7);
                uint level = GetLevelForEnemy(enemyName);
                __instance.SetLevel((int)level);
                logger.LogInfo(enemyName + "Loaded. Setting to level " + level + " (" + (level - 1) + " stars)");
            }
        }

        public static KeyValuePair<string, ConfigEntry<uint>> BindAndGetConfigForEnemy(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<uint>> enemyOverride = new KeyValuePair<string, ConfigEntry<uint>>(
                enemyName,
                config.Bind("ExpertMode.EnemyOverrides", enemyName + "_override", (uint)0, "Set above 0 to use this value instead of global_level for this enemy.")
            );
            enemyOverrides.Add(enemyOverride);
            return enemyOverride;
        }

        public static uint GetLevelForEnemy(string enemyName)
        {
            KeyValuePair<string, ConfigEntry<uint>> enemyConfig = enemyOverrides.Find(enemyOverride => enemyOverride.Key == enemyName);

            if (enemyConfig.Equals(default(KeyValuePair<string, ConfigEntry<uint>>)))
            {
                enemyConfig = BindAndGetConfigForEnemy(enemyName);
            }

            if (enemyConfig.Value.Value == 0)
            {
                return globalStarIncrease.Value;
            }

            return enemyConfig.Value.Value;
        }
    }
}
