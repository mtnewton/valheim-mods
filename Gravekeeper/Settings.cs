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
            public static ConfigEntry<bool> KeepEquipment { get; private set; }
            public static ConfigEntry<bool> KeepOneHandedWeapons { get; private set; }
            public static ConfigEntry<bool> KeepTwoHandedWeapons { get; private set; }
            public static ConfigEntry<bool> KeepShields { get; private set; }
            public static ConfigEntry<bool> KeepBows { get; private set; }
            public static ConfigEntry<bool> KeepHelmets { get; private set; }
            public static ConfigEntry<bool> KeepChests { get; private set; }
            public static ConfigEntry<bool> KeepLegs { get; private set; }
            public static ConfigEntry<bool> KeepHands { get; private set; }
            public static ConfigEntry<bool> KeepShoulders { get; private set; }
            public static ConfigEntry<bool> KeepUtility { get; private set; }
            public static ConfigEntry<bool> KeepTorchs { get; private set; }
            public static ConfigEntry<bool> KeepAmmo { get; private set; }
            public static ConfigEntry<bool> KeepMaterials { get; private set; }
            public static ConfigEntry<bool> KeepConsumables { get; private set; }
            public static ConfigEntry<bool> KeepTrophies { get; private set; }
            public static ConfigEntry<bool> KeepMisc { get; private set; }
            public static ConfigEntry<bool> KeepTools { get; private set; }

            public static void Init(ConfigFile config)
            {
                string name = "KeepInventory";

                Enabled = config.Bind(name, "Enabled", true,
                    "Should Gravekeeper modify what is kept on death?\n" +
                    "Turn on the below options to change what is kept on death."
                );

                KeepAll = config.Bind(name, "KeepAll", true,
                    "Keep all items on death.\n" +
                    "If set to false, the below options can be set to true to allow for only keeping specific item types."
                );

                KeepHotbar = config.Bind(name, "KeepHotbar", false,
                    "Items on the hotbar are kept on death"
                );

                KeepEquipped = config.Bind(name, "KeepEquipped", false,
                    "Equipped items are kept on death"
                );

                KeepEquipment = config.Bind(name, "KeepEquipment", false,
                    "Any equip-able items are kept on death\n" +
                    "Includes: OneHandedWeapon, TwoHandedWeapon, Shield, Bow, Helmet, Chest, Legs, Hands, Shoulder, Utility, Torch, Ammo"
                );

                KeepOneHandedWeapons = config.Bind(name, "KeepOneHandedWeapons", false,
                    "One handed weapons are kept on death"
                );

                KeepTwoHandedWeapons = config.Bind(name, "KeepTwoHandedWeapons", false,
                    "Two handed weapons are kept on death"
                );

                KeepShields = config.Bind(name, "KeepShields", false,
                    "Shields are kept on death"
                );

                KeepBows = config.Bind(name, "KeepBows", false,
                    "Bows are kept on death"
                );

                KeepHelmets = config.Bind(name, "KeepHelmets", false,
                    "Helmets are kept on death"
                 );

                KeepChests = config.Bind(name, "KeepChests", false,
                    "Chests are kept on death"
                );

                KeepLegs = config.Bind(name, "KeepLegs", false,
                    "Legs are kept on death"
                );

                KeepHands = config.Bind(name, "KeepHands", false,
                    "Hands are kept on death"
                );

                KeepShoulders = config.Bind(name, "KeepShoulders", false,
                    "Shoulders are kept on death"
                );

                KeepUtility = config.Bind(name, "KeepUtility", false,
                    "Utility items are kept on death (Wishbone, ...)"
                );

                KeepTorchs = config.Bind(name, "KeepTorchs", false,
                    "Torches are kept on death"
                );

                KeepAmmo = config.Bind(name, "KeepAmmo", false,
                    "Ammo is kept on death"
                );

                KeepMaterials = config.Bind(name, "KeepMaterials", false,
                    "Materials are kept on death (Wood, Stone, ...)"
                );

                KeepConsumables = config.Bind(name, "KeepConsumables", false,
                    "Consumables are kept on death"
                );

                KeepTrophies = config.Bind(name, "KeepTrophies", false,
                    "Trophies are kept on death"
                );

                KeepMisc = config.Bind(name, "KeepMisc", false,
                    "Misc items are kept on death (CryptKey, DragonEgg, ...)"
                );

                KeepTools = config.Bind(name, "KeepTools", false,
                    "Tools are kept on death"
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
