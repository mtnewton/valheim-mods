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
                    new ConfigDescription(
                        "Should Gravekeeper modify what is kept on death?\n" +
                        "Turn on the below Keep options to change what is kept on death\n" +
                        "(If using ConfigManager, the Keep options are Advanced Settings)",
                        null,
                        new ConfigurationManagerAttributes { Order = 100 }
                    )
                );

                KeepAll = config.Bind(name, "KeepAll", true,
                    new ConfigDescription(
                        "Keep all items on death.\n" +
                        "If set to false, the below options can be set to true to allow for only keeping specific item types.",
                        null,
                        new ConfigurationManagerAttributes { Order = 70, IsAdvanced = true }
                    )
                );

                KeepHotbar = config.Bind(name, "KeepHotbar", false,
                    new ConfigDescription(
                        "Items on the hotbar are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepEquipped = config.Bind(name, "KeepEquipped", false,
                    new ConfigDescription(
                        "Equipped items are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepEquipment = config.Bind(name, "KeepEquipment", false,
                    new ConfigDescription(
                        "Any equip-able items are kept on death\n" +
                        "Includes: OneHandedWeapon, TwoHandedWeapon, Shield, Bow, Helmet, Chest, Legs, Hands, Shoulder, Utility, Torch, Ammo",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepOneHandedWeapons = config.Bind(name, "KeepOneHandedWeapons", false,
                    new ConfigDescription(
                        "One handed weapons are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepTwoHandedWeapons = config.Bind(name, "KeepTwoHandedWeapons", false,
                    new ConfigDescription(
                        "Two handed weapons are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepShields = config.Bind(name, "KeepShields", false,
                    new ConfigDescription(
                        "Shields are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepBows = config.Bind(name, "KeepBows", false,
                    new ConfigDescription(
                        "Bows are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepHelmets = config.Bind(name, "KeepHelmets", false,
                    new ConfigDescription(
                        "Helmets are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                 );

                KeepChests = config.Bind(name, "KeepChests", false,
                    new ConfigDescription(
                        "Chests are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepLegs = config.Bind(name, "KeepLegs", false,
                    new ConfigDescription(
                        "Legs are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepHands = config.Bind(name, "KeepHands", false,
                    new ConfigDescription(
                        "Hands are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepShoulders = config.Bind(name, "KeepShoulders", false,
                    new ConfigDescription(
                        "Shoulders are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepUtility = config.Bind(name, "KeepUtility", false,
                    new ConfigDescription(
                        "Utility items are kept on death (Wishbone, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepTorchs = config.Bind(name, "KeepTorchs", false,
                    new ConfigDescription(
                        "Torches are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepAmmo = config.Bind(name, "KeepAmmo", false,
                    new ConfigDescription(
                        "Ammo is kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepMaterials = config.Bind(name, "KeepMaterials", false,
                    new ConfigDescription(
                        "Materials are kept on death (Wood, Stone, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepConsumables = config.Bind(name, "KeepConsumables", false,
                    new ConfigDescription(
                        "Consumables are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepTrophies = config.Bind(name, "KeepTrophies", false,
                    new ConfigDescription(
                        "Trophies are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepMisc = config.Bind(name, "KeepMisc", false,
                    new ConfigDescription(
                        "Misc items are kept on death (CryptKey, DragonEgg, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                KeepTools = config.Bind(name, "KeepTools", false,
                    new ConfigDescription(
                        "Tools are kept on death",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );
            }
        }

        public class Grave
        {
            public static ConfigEntry<bool> Enabled { get; private set; }
            public static ConfigEntry<bool> KeepGrave { get; private set; }
            public static ConfigEntry<bool> DeleteItems { get; private set; }
            public static ConfigEntry<bool> DeleteAll { get; private set; }
            public static ConfigEntry<bool> DeleteHotbar { get; private set; }
            public static ConfigEntry<bool> DeleteEquipped { get; private set; }
            public static ConfigEntry<bool> DeleteEquipment { get; private set; }
            public static ConfigEntry<bool> DeleteOneHandedWeapons { get; private set; }
            public static ConfigEntry<bool> DeleteTwoHandedWeapons { get; private set; }
            public static ConfigEntry<bool> DeleteShields { get; private set; }
            public static ConfigEntry<bool> DeleteBows { get; private set; }
            public static ConfigEntry<bool> DeleteHelmets { get; private set; }
            public static ConfigEntry<bool> DeleteChests { get; private set; }
            public static ConfigEntry<bool> DeleteLegs { get; private set; }
            public static ConfigEntry<bool> DeleteHands { get; private set; }
            public static ConfigEntry<bool> DeleteShoulders { get; private set; }
            public static ConfigEntry<bool> DeleteUtility { get; private set; }
            public static ConfigEntry<bool> DeleteTorchs { get; private set; }
            public static ConfigEntry<bool> DeleteAmmo { get; private set; }
            public static ConfigEntry<bool> DeleteMaterials { get; private set; }
            public static ConfigEntry<bool> DeleteConsumables { get; private set; }
            public static ConfigEntry<bool> DeleteTrophies { get; private set; }
            public static ConfigEntry<bool> DeleteMisc { get; private set; }
            public static ConfigEntry<bool> DeleteTools { get; private set; }
            public static int GraveWidth = 8;
            public static int GraveHeight = 4;

            public static void Init(ConfigFile config)
            {
                string name = "Grave";

                Enabled = config.Bind(name, "Enabled", true,
                    new ConfigDescription(
                        "Should Gravekeeper modify how graves are created?\n" +
                        "Enables this entire section",
                        null,
                        new ConfigurationManagerAttributes { Order = 100 }
                    )
                );

                KeepGrave = config.Bind(name, "KeepGrave", false,
                    new ConfigDescription(
                        "If no graves are to be created, create one with a stone in it.",
                        null,
                        new ConfigurationManagerAttributes { Order = 90 }
                    )
                );

                DeleteItems = config.Bind(name, "DeleteItems", false,
                    new ConfigDescription(
                        "Items that would go to the grave are deleted\n" + 
                        "Control what gets deleted with the below Delete options\n" +
                        "(If using ConfigManager, the Delete options are Advanced Settings)",
                        null,
                        new ConfigurationManagerAttributes { Order = 80 }
                    )
                );

                DeleteAll = config.Bind(name, "DeleteAll", true,
                    new ConfigDescription(
                        "All items are deleted from the grave\n" +
                        "If set to false, the other Delete options can be set to true to allow for only deleting specific item types",
                        null,
                        new ConfigurationManagerAttributes { Order = 70, IsAdvanced = true }
                    )
                );

                DeleteHotbar = config.Bind(name, "DeleteHotbar", false,
                    new ConfigDescription(
                        "Items on the hotbar are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteEquipped = config.Bind(name, "DeleteEquipped", false,
                    new ConfigDescription(
                    "Equipped items are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteEquipment = config.Bind(name, "DeleteEquipment", false,
                    new ConfigDescription(
                    "Any equip-able items are deleted from the grave\n" +
                    "Includes: OneHandedWeapon, TwoHandedWeapon, Shield, Bow, Helmet, Chest, Legs, Hands, Shoulder, Utility, Torch, Ammo",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteOneHandedWeapons = config.Bind(name, "DeleteOneHandedWeapons", false,
                    new ConfigDescription(
                    "One handed weapons are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteTwoHandedWeapons = config.Bind(name, "DeleteTwoHandedWeapons", false,
                    new ConfigDescription(
                    "Two handed weapons are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteShields = config.Bind(name, "DeleteShields", false,
                    new ConfigDescription(
                    "Shields are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteBows = config.Bind(name, "DeleteBows", false,
                    new ConfigDescription(
                    "Bows are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteHelmets = config.Bind(name, "DeleteHelmets", false,
                    new ConfigDescription(
                    "Helmets are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                 );

                DeleteChests = config.Bind(name, "DeleteChests", false,
                    new ConfigDescription(
                    "Chests are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteLegs = config.Bind(name, "DeleteLegs", false,
                    new ConfigDescription(
                    "Legs are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteHands = config.Bind(name, "DeleteHands", false,
                    new ConfigDescription(
                    "Hands are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteShoulders = config.Bind(name, "DeleteShoulders", false,
                    new ConfigDescription(
                    "Shoulders are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteUtility = config.Bind(name, "DeleteUtility", false,
                    new ConfigDescription(
                    "Utility items are deleted from the grave (Wishbone, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteTorchs = config.Bind(name, "DeleteTorchs", false,
                    new ConfigDescription(
                    "Torches are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteAmmo = config.Bind(name, "DeleteAmmo", false,
                    new ConfigDescription(
                    "Ammo is deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteMaterials = config.Bind(name, "DeleteMaterials", false,
                    new ConfigDescription(
                    "Materials are deleted from the grave (Wood, Stone, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteConsumables = config.Bind(name, "DeleteConsumables", false,
                    new ConfigDescription(
                        "Consumables are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteTrophies = config.Bind(name, "DeleteTrophies", false,
                    new ConfigDescription(
                        "Trophies are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteMisc = config.Bind(name, "DeleteMisc", false,
                    new ConfigDescription(
                        "Misc items are deleted from the grave (CryptKey, DragonEgg, ...)",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );

                DeleteTools = config.Bind(name, "DeleteTools", false,
                    new ConfigDescription(
                        "Tools are deleted from the grave",
                        null,
                        new ConfigurationManagerAttributes { IsAdvanced = true }
                    )
                );
            }
        }
    }
}
