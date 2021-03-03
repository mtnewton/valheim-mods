using BepInEx.Configuration;
using System.Collections.Generic;
using UnityEngine;

namespace Gravekeeper
{
    class Settings
    {
        private static ItemDrop.ItemData EmptyGraveItem = null;

        public static void Init(ConfigFile config)
        {
            KeepInventory.Init(config);
            Grave.Init(config);
        }

        public static ItemDrop.ItemData GetStoneItemData()
        {
            if (EmptyGraveItem != null)
            {
                return EmptyGraveItem.Clone();
            }

            List<ItemDrop> items = ObjectDB.m_instance.GetAllItems(ItemDrop.ItemData.ItemType.Material, "Stone");
            foreach (ItemDrop item in items)
            {
                if (item.m_itemData.m_shared.m_name == "$item_stone")
                {
                    string prefabName = item.GetPrefabName(item.gameObject.name);
                    GameObject itemPrefab = ObjectDB.instance.GetItemPrefab(prefabName);
                    item.m_itemData.m_dropPrefab = itemPrefab;
                    EmptyGraveItem = item.m_itemData;
                    break;
                }
            }
            return EmptyGraveItem.Clone();
        }

        public class KeepInventory
        {
            public static ConfigEntry<bool> Enabled { get; private set; }
            public static ConfigEntry<bool> KeepAll { get; private set; }
            public static ConfigEntry<bool> KeepHotbar { get; private set; }
            public static ConfigEntry<bool> KeepEquipped { get; private set; }
            public static ConfigEntry<bool> KeepAmmo { get; private set; }
            public static ConfigEntry<bool> KeepConsumables { get; private set; }

            public static void Init(ConfigFile config)
            {
                string name = "KeepInventory";

                Enabled = config.Bind(name, "Enabled", true,
                    "Should Gravekeeper modify what is kept on death?\n" +
                    "Turn on the below options to change what is kept on death."
                );

                KeepAll = config.Bind(name, "KeepAll", true,
                    "Keep all items on death."
                );

                KeepHotbar = config.Bind(name, "KeepHotbar", false,
                    "Items on the hotbar are kept.\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepEquipped = config.Bind(name, "KeepEquipped", false,
                    "Equipped items are kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepAmmo = config.Bind(name, "KeepAmmo", false,
                    "Ammo is kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );

                KeepConsumables = config.Bind(name, "KeepConsumables", false,
                    "Consumables are kept\n" +
                    "Only needed if [KeepInventory] KeepAll is false"
                );
            }
        }

        public class Grave
        {
            public static ConfigEntry<bool> Enabled { get; private set; }
            public static ConfigEntry<bool> KeepGrave { get; private set; }
            public static ConfigEntry<bool> DeleteItems { get; private set; }
            public static int GraveWidth = 8;
            public static int GraveHeight = 4;

            public static void Init(ConfigFile config)
            {
                string name = "Grave";

                Enabled = config.Bind(name, "Enabled", true,
                    "Should Gravekeeper modify how graves are created?"
                );

                DeleteItems = config.Bind(name, "DeleteItems", false,
                    "Whatever is not kept by KeepInventory is deleted before grave creation."
                );

                KeepGrave = config.Bind(name, "KeepGrave", false,
                    "If no graves are to be created, create one with a stone in it."
                );
            }
        }
    }
}
