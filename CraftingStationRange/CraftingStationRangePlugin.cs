using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;

namespace CraftingStationRange
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    class CraftingStationRangePlugin : BaseUnityPlugin
    {
        const string GUID = "net.mtnewton.craftingstationrange";

        const string NAME = "CraftingStationRange";

        const string VERSION = "1.0.2";

        private static ConfigFile config;

        private static ManualLogSource logger;

        private static List<KeyValuePair<string, float>> stationRanges = new List<KeyValuePair<string, float>>();

        void Awake()
        {
            logger = Logger;
            config = Config;

            BindStationRange("$piece_workbench");
            BindStationRange("$piece_forge");
            BindStationRange("$piece_stonecutter");
            BindStationRange("$piece_artisanstation");

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            Logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(CraftingStation), "Start")]
        public static void LogStations(CraftingStation __instance)
        {
            float stationRange = GetStationRange(__instance.m_name);

            if (stationRange == 0)
            {
                Destroy(__instance.m_areaMarker);
                __instance.m_areaMarker = null;
                __instance.m_rangeBuild = float.MaxValue;
                logger.LogInfo(__instance.m_name + " set to max range.");
                return;
            }

            __instance.m_rangeBuild = stationRange;
            __instance.m_areaMarker.GetComponent<CircleProjector>().m_radius = stationRange;
            __instance.m_areaMarker.GetComponent<CircleProjector>().m_nrOfSegments = (int)Math.Ceiling(Math.Max(5f, 4f * stationRange));
            logger.LogInfo(__instance.m_name + " set to " + stationRange + " range");

        }

        public static float GetStationRange(string stationName)
        {
            KeyValuePair<string, float> stationNameRangePair = stationRanges.Find(station => station.Key == stationName);

            if (stationNameRangePair.Equals(default(KeyValuePair<string, float>)))
            {
                stationNameRangePair = BindStationRange(stationName);
            }

            return stationNameRangePair.Value;
        }

        public static KeyValuePair<string, float> BindStationRange(string stationName)
        {
            KeyValuePair<string, float> stationNameRangePair = new KeyValuePair<string, float>(
                stationName,
                Math.Abs(config.Bind(
                    "CraftingStationRange", 
                    stationName, 
                    (float) 0.0, 
                    "This crafting stations range."
                ).Value)
            );

            stationRanges.Add(stationNameRangePair);

            return stationNameRangePair;
        }
    }
}
