using System.Collections.Generic;
using System.Linq;

namespace Gravekeeper
{
    class KeepInventory
    {
        public static void Handle(ref Inventory playerInventory, ref Inventory savedInventory)
        {
            if (Settings.KeepInventory.KeepAll.Value)
            {
                savedInventory.MoveAll(playerInventory);
                return;
            }

            List<ItemDrop.ItemData> items = playerInventory.GetAllItems().Select(item => item.Clone()).ToList();

            foreach (ItemDrop.ItemData item in items)
            {
                if (ShouldKeepItem(item))
                {
                    savedInventory.MoveItemToThis(
                        playerInventory,
                        playerInventory.GetItemAt(item.m_gridPos.x, item.m_gridPos.y),
                        item.m_stack,
                        item.m_gridPos.x,
                        item.m_gridPos.y
                    );
                }
            }
        }

        public static bool ShouldKeepItem(ItemDrop.ItemData item)
        {
            return (Settings.KeepInventory.KeepHotbar.Value && item.m_gridPos.y == 0)

                || (Settings.KeepInventory.KeepEquipped.Value && item.m_equiped)

                || (Settings.KeepInventory.KeepEquipment.Value && item.IsEquipable())

                // These 12 categories below is what the game considers Equip-able
                || (Settings.KeepInventory.KeepOneHandedWeapons.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.OneHandedWeapon)
                || (Settings.KeepInventory.KeepTwoHandedWeapons.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.TwoHandedWeapon)
                || (Settings.KeepInventory.KeepShields.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shield)
                || (Settings.KeepInventory.KeepBows.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Bow)
                || (Settings.KeepInventory.KeepHelmets.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Helmet)
                || (Settings.KeepInventory.KeepChests.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Chest)
                || (Settings.KeepInventory.KeepLegs.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Legs)
                || (Settings.KeepInventory.KeepHands.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Hands)
                || (Settings.KeepInventory.KeepShoulders.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Shoulder)
                || (Settings.KeepInventory.KeepUtility.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Utility)
                || (Settings.KeepInventory.KeepTorchs.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Torch)
                || (Settings.KeepInventory.KeepAmmo.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)

                || (Settings.KeepInventory.KeepMaterials.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Material)
                || (Settings.KeepInventory.KeepConsumables.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
                || (Settings.KeepInventory.KeepTrophies.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Trophie)
                || (Settings.KeepInventory.KeepMisc.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Misc)
                || (Settings.KeepInventory.KeepTools.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Tool)
                || item.m_shared.m_questItem;
        }
    }
}
