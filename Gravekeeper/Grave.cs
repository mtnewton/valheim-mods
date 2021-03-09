using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;

namespace Gravekeeper
{
    class Grave
    {
        public static void Handle(ref Player player)
        {
            if (Settings.Grave.DeleteItems.Value && Settings.Grave.DeleteAll.Value)
            {
                player.GetInventory().m_inventory.Clear();
                player.GetInventory().Changed();
            }

            int graveCount = 0;

            if (player.GetInventory().m_inventory.Count > 0)
            {
                for (int row = 0; row < player.GetInventory().m_height; row += Settings.Grave.GraveHeight)
                {
                    graveCount += CreateAGrave(ref player, row, graveCount) ? 1 : 0;
                }
            }

            if (Settings.Grave.KeepGrave.Value && graveCount == 0)
            {
                Inventory tempInventory = new Inventory("TempInventory", null, Settings.Grave.GraveWidth, Settings.Grave.GraveHeight);
                tempInventory.AddItem(Settings.GetStoneItemData());
                SpawnAGrave(player, tempInventory, graveCount);
            }
        }

        private static bool CreateAGrave(ref Player player, int startingRow, int graveCount)
        {
            Inventory tempInventory = new Inventory("TempInventory", null, Settings.Grave.GraveWidth, Settings.Grave.GraveHeight);

            for (int y = startingRow; y < startingRow + Settings.Grave.GraveHeight; y++)
            {
                for (int x = 0; x < player.GetInventory().m_width; x++)
                {
                    ItemDrop.ItemData item = player.GetInventory().GetItemAt(x, y);
                    if (item == null || item.m_shared.m_questItem)
                    {
                        continue;
                    }

                    if (item.m_equiped)
                    {
                        player.UnequipItem(item, false);
                        if (item.m_equiped != false)
                        {
                            continue;
                        }
                    }

                    if (Settings.Grave.DeleteItems.Value && ShouldBeDeleted(item))
                    {
                        player.GetInventory().RemoveItem(item);
                        continue;
                    }


                    tempInventory.MoveItemToThis(player.GetInventory(), item, item.m_stack, x, (y - startingRow) % Settings.Grave.GraveHeight);
                }
            }

            if (tempInventory.GetAllItems().Count == 0)
            {
                return false;
            }

            SpawnAGrave(player, tempInventory, graveCount);

            return true;
        }

        private static void SpawnAGrave(Player player, Inventory inventory, int graveCount)
        {
            Vector3 offset = (graveCount > 0) ? (Quaternion.Euler(0, 30 * graveCount, 0) * (new Vector3(.5f, 1f, .5f))) : Vector3.zero;
            GameObject obj = Object.Instantiate(player.m_tombstone, player.GetCenterPoint() + offset, player.transform.rotation);

            Inventory graveInventory = obj.GetComponent<Container>().GetInventory();
            graveInventory.m_name = "GKGrave";
            graveInventory.m_height = Settings.Grave.GraveHeight;
            graveInventory.m_width = Settings.Grave.GraveWidth;
            graveInventory.MoveAll(inventory);

            TombStone component = obj.GetComponent<TombStone>();
            PlayerProfile playerProfile = Game.instance.GetPlayerProfile();
            string graveName = playerProfile.GetName();
            if (graveCount > 0)
            {
                graveName += $"({graveCount})";
            }
            component.Setup(graveName, playerProfile.GetPlayerID());
        }

        public static bool ShouldBeDeleted(ItemDrop.ItemData item)
        {
            return (Settings.Grave.DeleteHotbar.Value && item.m_gridPos.y == 0)

                || (Settings.Grave.DeleteEquipped.Value && item.m_equiped)

                || (Settings.Grave.DeleteEquipment.Value && item.IsEquipable())

                // These 12 categories below is what the game considers Equip-able
                || (Settings.Grave.DeleteOneHandedWeapons.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon)
                || (Settings.Grave.DeleteTwoHandedWeapons.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                || (Settings.Grave.DeleteShields.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                || (Settings.Grave.DeleteBows.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                || (Settings.Grave.DeleteHelmets.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet)
                || (Settings.Grave.DeleteChests.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest)
                || (Settings.Grave.DeleteLegs.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs)
                || (Settings.Grave.DeleteHands.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Hands)
                || (Settings.Grave.DeleteShoulders.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder)
                || (Settings.Grave.DeleteUtility.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility)
                || (Settings.Grave.DeleteTorchs.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Torch)
                || (Settings.Grave.DeleteAmmo.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)

                || (Settings.Grave.DeleteMaterials.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Material)
                || (Settings.Grave.DeleteConsumables.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
                || (Settings.Grave.DeleteTrophies.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Trophie)
                || (Settings.Grave.DeleteMisc.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Misc)
                || (Settings.Grave.DeleteTools.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool);
        }
    }
}
