using BepInEx.Configuration;
using System;
using UnityEngine;

namespace ItemStacks
{
    class ItemTracker
    {
        public string ItemName { get; }

        public int OriginalStackSize { get; }
        public float OriginalWeight { get; }

        private ConfigEntry<int> ItemStackSizeConfig { get; }
        private ConfigEntry<float> ItemWeightConfig { get; }

        public ItemTracker(ItemDrop item)
        {
            ItemName = GetItemName(item);

            OriginalStackSize = item.m_itemData.m_shared.m_maxStackSize;
            OriginalWeight = item.m_itemData.m_shared.m_weight;

            if (item.m_itemData.m_shared.m_maxStackSize > 1)
            {
                ItemStackSizeConfig = ItemStacksPlugin.config.Bind(ItemStacksPlugin.NAME + ".ItemStackSize", ItemName + "_stack_size", 0,
                    new ConfigDescription(
                        "Set above 0 to use this value instead of the stack size multiplier\n" +
                        $"Game default: {OriginalStackSize}",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );
            }

            ItemWeightConfig = ItemStacksPlugin.config.Bind(ItemStacksPlugin.NAME + ".ItemWeight", ItemName + "_weight", 0f,
                new ConfigDescription(
                    "Set above 0 to use this value instead of the weight multiplier\n" +
                    $"Game default: {OriginalWeight}",
                    null,
                    new ConfigurationManagerAttributes { IsAdvanced = true }
                )
            );
        }

        public void SetStackSize(float multiplier, ItemDrop item)
        {
            int configValue = ItemStackSizeConfig.Value;
            int value = (configValue > 0) ? configValue : Mathf.Clamp((int)Math.Round(OriginalStackSize * multiplier), 1, int.MaxValue);
            item.m_itemData.m_shared.m_maxStackSize = value;
            ItemStacksPlugin.logger.LogDebug($"{ItemName} OriginalStackSize:{OriginalStackSize} SetTo:{value}");
        }

        public void SetWeight(float multiplier, ItemDrop item)
        {
            float configValue = ItemWeightConfig.Value;
            float value = (configValue > 0) ? configValue : Mathf.Clamp(OriginalWeight * multiplier, 0, int.MaxValue);
            item.m_itemData.m_shared.m_weight = value;
            ItemStacksPlugin.logger.LogDebug($"{ItemName} OriginalWeight:{OriginalWeight} SetTo:{value}");
        }

        private string GetItemName(ItemDrop item)
        {
            if (item.m_itemData.m_shared.m_name.StartsWith("$item_"))
            {
                return item.m_itemData.m_shared.m_name.Substring(6, item.m_itemData.m_shared.m_name.Length - 6);
            }
            return null;
        }
    }
}
