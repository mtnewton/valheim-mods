using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using BepInEx.Logging;

namespace Gravekeeper
{
    [HarmonyPatch]
    class KeepInventoryPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "CreateTombStone")]
        static void Player_CreateTombstone_PrefixPatch(out KeyValuePair<Player, Inventory> __state, ref Player __instance)
        {
            GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch start", LogLevel.Debug);

            Inventory savedInventory = new Inventory("SavedInventory", null, __instance.m_inventory.m_width, __instance.m_inventory.m_height);
            TransferItemsToKeep(ref __instance.m_inventory, ref savedInventory);
            __state = new KeyValuePair<Player, Inventory>(__instance, savedInventory);

            GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch saved " + savedInventory.GetAllItems().Count + " items", LogLevel.Debug);
            GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch player still has " + __instance.GetInventory().GetAllItems().Count + " items", LogLevel.Debug);

            GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch end", LogLevel.Debug);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Player), "CreateTombStone")]
        static void Player_CreateTombstone_PostfixPatch(KeyValuePair<Player, Inventory> __state)
        {
            GravekeeperPlugin.Log("Player_CreateTombstone_PostfixPatch start", LogLevel.Debug);
            __state.Key.m_inventory.MoveAll(__state.Value);
            GravekeeperPlugin.Log("Player_CreateTombstone_PostfixPatch end", LogLevel.Debug);
        }

        private static void TransferItemsToKeep(ref Inventory playerInventory, ref Inventory savedInventory)
        {

            GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch transfer start", LogLevel.Debug);
            if (Settings.KeepInventory.Enabled.Value)
            {

                GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch keepinventory enabled", LogLevel.Debug);
                if (Settings.KeepInventory.KeepAll.Value)
                {
                    GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch keepall", LogLevel.Debug);
                    savedInventory.MoveAll(playerInventory);
                    return;
                }

                GravekeeperPlugin.Log("Player_CreateTombstone_PrefixPatch check for items to keep", LogLevel.Debug);
                List<ItemDrop.ItemData> items = playerInventory.GetAllItems().Select(item => item.Clone()).ToList();
                foreach (ItemDrop.ItemData item in items)
                {
                    if ((Settings.KeepInventory.KeepHotbar.Value && item.m_gridPos.y == 0)
                        || (Settings.KeepInventory.KeepEquipped.Value && item.m_equiped)
                        || (Settings.KeepInventory.KeepAmmo.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Ammo)
                        || (Settings.KeepInventory.KeepConsumables.Value && item.m_shared.m_itemType == ItemDrop.ItemData.ItemType.Consumable)
                    ) {

                        GravekeeperPlugin.Log($"Player_CreateTombstone_PrefixPatch move item " + item.m_shared.m_name, LogLevel.Debug);
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
        }
    }
}
