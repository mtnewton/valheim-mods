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
                || (Settings.KeepInventory.KeepAmmo.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)
                || (Settings.KeepInventory.KeepConsumables.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable);
        }
    }
}
