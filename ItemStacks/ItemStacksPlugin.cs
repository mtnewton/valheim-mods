using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ItemStacks
{
    [BepInPlugin("net.mtnewton.itemstacks", "ItemStacks", "1.0.0")]
    public class ItemStacksPlugin : BaseUnityPlugin
    {
        private static ManualLogSource manualLogSource;

        private static ConfigFile config;

        void Awake()
        {
            manualLogSource = Logger;
            config = Config;

            Harmony harmony = new Harmony("mod.itemstacks");
            harmony.PatchAll();

            Log("ItemStacks loaded.");
        }

        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            manualLogSource.Log(level, data);
        }

        public static ConfigFile GetConfig()
        {
            return config;
        }
    }
}
