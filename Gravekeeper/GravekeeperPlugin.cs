using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Gravekeeper
{
    [BepInPlugin("net.mtnewton.gravekeeper", "Gravekeeper", "1.0.1")]
    public class GravekeeperPlugin : BaseUnityPlugin
    {
        private static ManualLogSource manualLogSource;
        void Awake()
        {
            manualLogSource = Logger;
            Log("Gravekeeper loaded.");

            var harmony = new Harmony("mod.gravekeeper");
            harmony.PatchAll();
        }

        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            manualLogSource.Log(level, data);
        }
    }
}
