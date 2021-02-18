using BepInEx.Configuration;
using HarmonyLib;
using System;
using static ItemDrop.ItemData;

namespace ItemStacks
{
    [HarmonyPatch(typeof(ObjectDB), "Awake")]
    class ModifyItemStackSizeAndWeight
    {
        static void Postfix(ObjectDB __instance)
        {
            foreach (ItemType type in (ItemType[])Enum.GetValues(typeof(ItemType)))
            {
                ConfigEntry<bool> stackSizeEnabled = ItemStacksPlugin.GetConfig().Bind("ItemStacks.ItemStackSize", "enabled", true);
                ConfigEntry<bool> weightEnabled = ItemStacksPlugin.GetConfig().Bind("ItemStacks.ItemWeight", "enabled", true);

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
                            ConfigEntry<int> itemStackSizeConfig = ItemStacksPlugin.GetConfig().Bind("ItemStacks.ItemStackSize", itemName + "_stack_size", item.m_itemData.m_shared.m_maxStackSize * 10);
                            item.m_itemData.m_shared.m_maxStackSize = itemStackSizeConfig.Value;
                            ItemStacksPlugin.Log(itemName + " - max stack size set to " + item.m_itemData.m_shared.m_maxStackSize);
                        }

                        if (stackSizeEnabled.Value)
                        {
                            ConfigEntry<float> itemWeightConfig = ItemStacksPlugin.GetConfig().Bind("ItemStacks.ItemWeight", itemName + "_weight", item.m_itemData.m_shared.m_weight * 0.1f);
                            item.m_itemData.m_shared.m_weight = itemWeightConfig.Value;
                            ItemStacksPlugin.Log(itemName + " - item weight set to " + item.m_itemData.m_shared.m_weight);
                        }
                    }
                }
            }
        }
    }
}
