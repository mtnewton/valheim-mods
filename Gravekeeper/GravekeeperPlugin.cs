using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Gravekeeper
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class GravekeeperPlugin : BaseUnityPlugin
    {
        public const string GUID = "net.mtnewton.gravekeeper";

        public const string NAME = "Gravekeeper";

        public const string VERSION = "2.2.0";

        private static ManualLogSource logger;

        void Awake()
        {
            logger = Logger;

            Settings.Init(Config);

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }     
        
        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            logger.Log(level, data);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "CreateTombStone")]
        static bool Player_CreateTombstone_PrefixPatch(out Inventory __state, ref Player __instance)
        {
            __state = default;

            if (Settings.KeepInventory.Enabled.Value)
            {
                Inventory savedInventory = new Inventory("SavedInventory", null, __instance.m_inventory.m_width, __instance.m_inventory.m_height);
                KeepInventory.Handle(ref __instance.m_inventory, ref savedInventory);
                __state = savedInventory;
            }

            if (Settings.Grave.Enabled.Value)
            {
                Grave.Handle(ref __instance);
                return false;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "CreateTombStone")]
        static void Player_CreateTombstone_PostfixPatch(Inventory __state, ref Player __instance)
        {
            if (Settings.KeepInventory.Enabled.Value)
            {
                __instance.GetInventory().MoveAll(__state);
            }
        }
    }
}
