using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace BiggerPickupRadius
{
    [BepInPlugin("net.mtnewton.biggerpickupradius", "BiggerPickupRadius", "1.0.0")]
    public class BiggerPickupRadiusPlugin : BaseUnityPlugin
    {
        private static ManualLogSource manualLogSource;

        private static ConfigEntry<uint> radiusMultiplier;

        void Awake()
        {
            manualLogSource = Logger;

            radiusMultiplier = Config.Bind(
                "BiggerPickupRadius",
                "RadiusMultiplier",
                (uint) 3,
                "What to multiply the player auto pickup radius by."
            );

            Harmony harmony = new Harmony("mod.biggerpickupradius");
            harmony.PatchAll();

            Log("BiggerPickupRadius loaded.");
        }

        public static uint getRadiusMultiplier() 
        {
            return radiusMultiplier.Value;    
        }

        public static void Log(object data, LogLevel level = LogLevel.Info)
        {
            manualLogSource.Log(level, data);
        }
    }
}
