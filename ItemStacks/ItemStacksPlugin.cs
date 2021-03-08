using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ItemStacks
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class ItemStacksPlugin : BaseUnityPlugin
    {
        public const string GUID = "net.mtnewton.itemstacks";

        public const string NAME = "ItemStacks";

        public const string VERSION = "1.2.0";

        public static ManualLogSource logger;

        public static ConfigFile config;

        private static ConfigEntry<bool> stackSizeEnabledConfig;
        private static ConfigEntry<float> stackSizeMultiplierConfig;
        private static ConfigEntry<bool> weightEnabledConfig;
        private static ConfigEntry<float> weightMultiplierConfig;

        private static readonly Dictionary<string, ItemTracker> itemTrackers = new Dictionary<string, ItemTracker>();

        void Awake()
        {
            logger = Logger;
            config = Config;

            stackSizeEnabledConfig = config.Bind(NAME + ".ItemStackSize", "enabled", true,
                new ConfigDescription(
                    "Should item stack size be modified?",
                    null,
                    new ConfigurationManagerAttributes { Order = 1 }
                )
            );

            weightEnabledConfig = config.Bind(NAME + ".ItemWeight", "enabled", true,
                new ConfigDescription(
                    "Should item weight be modified?",
                    null,
                    new ConfigurationManagerAttributes { Order = 1 }
                )
            );

            stackSizeMultiplierConfig = config.Bind(NAME + ".ItemMultipliers", "stack_size_multiplier", 10f,
                "Multiply the original item stack size by this value\n" +
                "Minimum resulting stack size is 1\n" +
                "Overwritten by individual item _stack_size values."
            );

            weightMultiplierConfig = config.Bind(NAME + ".ItemMultipliers", "weight_multiplier", .1f,
                "Multiply the original item weight by this value\n" +
                "Overwritten by individual item _weight values."
            );

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        static void ModifyItemStackSizeAndWeight(ObjectDB __instance)
        {
            bool stackSizeEnabled = stackSizeEnabledConfig.Value;
            float stackSizeMultiplier = Mathf.Clamp(stackSizeMultiplierConfig.Value, 0, int.MaxValue);

            bool weightEnabled = weightEnabledConfig.Value;
            float weightMultiplier = Mathf.Clamp(weightMultiplierConfig.Value, 0, int.MaxValue);

            if (!(stackSizeEnabled || weightEnabled)) return;

            foreach (ItemDrop.ItemData.ItemType type in (ItemDrop.ItemData.ItemType[])Enum.GetValues(typeof(ItemDrop.ItemData.ItemType)))
            {
                foreach (ItemDrop item in __instance.GetAllItems(type, ""))
                {
                    if (item.m_itemData.m_shared.m_name.StartsWith("$item_"))
                    {
                        ItemTracker tracker = GetItemTracker(item);

                        if (stackSizeEnabled && (tracker.OriginalStackSize > 1)) tracker.SetStackSize(stackSizeMultiplier, item);

                        if (weightEnabled) tracker.SetWeight(weightMultiplier, item);
                    }
                }
            }
        }

        private static ItemTracker GetItemTracker(ItemDrop item)
        {
            bool gotIt = itemTrackers.TryGetValue(item.m_itemData.m_shared.m_name, out ItemTracker tracker);
            if (gotIt)
            {
                return tracker;
            }
            tracker = new ItemTracker(item);
            itemTrackers.Add(item.m_itemData.m_shared.m_name, tracker);
            return tracker;
        }
    }
}
