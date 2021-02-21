using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace BiggerPickupRadius
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class BiggerPickupRadiusPlugin : BaseUnityPlugin
    {
        const string GUID = "net.mtnewton.biggerpickupradius";

        const string NAME = "BiggerPickupRadius";

        const string VERSION = "1.1.2";

        private static ManualLogSource logger;

        private static ConfigEntry<uint> radius;

        void Awake()
        {
            logger = Logger;

            radius = Config.Bind(NAME, "Radius", (uint) 6, "Player pickup radius. Game default is 2");

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "Awake")]
        static void SetPickupRadius(Player __instance)
        {
            logger.LogInfo(__instance.m_autoPickupRange);
            __instance.m_autoPickupRange = radius.Value;
            logger.LogInfo("Player pickup range set to " + radius.Value) ;
        }
    }
}
