using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using static ItemDrop.ItemData;

namespace ItemStacks
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class ItemStacksPlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.itemstacks";

        private const string NAME = "ItemStacks";

        private const string VERSION = "1.1.2";

        private static ManualLogSource logger;

        private static ConfigFile config;

        void Awake()
        {
            logger = Logger;
            config = Config;

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(ObjectDB), "Awake")]
        static void ModifyItemStackSizeAndWeight(ObjectDB __instance)
        {
            foreach (ItemType type in (ItemType[])Enum.GetValues(typeof(ItemType)))
            {
                ConfigEntry<bool> stackSizeEnabled = config.Bind(NAME + ".ItemStackSize", "enabled", true);
                ConfigEntry<bool> weightEnabled = config.Bind(NAME + ".ItemWeight", "enabled", true);

                if (!(stackSizeEnabled.Value || weightEnabled.Value))
                {
                    return;
                }

                foreach (ItemDrop item in __instance.GetAllItems(type, ""))
                {
                    if (item.m_itemData.m_shared.m_maxStackSize > 1 && item.m_itemData.m_shared.m_name.StartsWith("$item_"))
                    {
                        string itemName = item.m_itemData.m_shared.m_name.Substring(6, item.m_itemData.m_shared.m_name.Length - 6);

                        if (stackSizeEnabled.Value)
                        {
                            ConfigEntry<int> itemStackSizeConfig = config.Bind(NAME + ".ItemStackSize", itemName + "_stack_size", item.m_itemData.m_shared.m_maxStackSize * 10);
                            item.m_itemData.m_shared.m_maxStackSize = itemStackSizeConfig.Value;
                            logger.LogInfo(itemName + " - max stack size set to " + item.m_itemData.m_shared.m_maxStackSize);
                        }

                        if (stackSizeEnabled.Value)
                        {
                            ConfigEntry<float> itemWeightConfig = config.Bind(NAME + ".ItemWeight", itemName + "_weight", item.m_itemData.m_shared.m_weight * 0.1f);
                            item.m_itemData.m_shared.m_weight = itemWeightConfig.Value;
                            logger.LogInfo(itemName + " - item weight set to " + item.m_itemData.m_shared.m_weight);
                        }
                    }
                }
            }
        }
    }
}
