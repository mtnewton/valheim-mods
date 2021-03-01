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

        public const string VERSION = "2.0.0";

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
    }
}
