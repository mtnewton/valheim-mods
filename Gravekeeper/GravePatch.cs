using HarmonyLib;
using UnityEngine;
using BepInEx.Logging;

namespace Gravekeeper
{
    [HarmonyPatch]
    class GravePatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
        static void Inventory_MoveInventoryToGrave_PrefixPatch(ref Inventory __instance, ref Inventory original)
        {
            if (!Settings.Grave.Enabled.Value)
            {
                GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch grave disbled", LogLevel.Debug);
                return;
            }
            GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch grave enabled", LogLevel.Debug);

            if (Settings.Grave.ExtraGraves.Value)
            {
                GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch extragraves enabled", LogLevel.Debug);
                for (int row = __instance.m_height; row < original.m_height; row += __instance.m_height)
                {
                    CreateAGrave(ref original, row, __instance.m_width, __instance.m_height);
                }
            }
        }

        private static void CreateAGrave(ref Inventory playerInventory, int startingRow, int width, int height)
        {
            GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch starting CreateAGrave", LogLevel.Debug);

            Inventory tempInventory = new Inventory("TempInventory", null, width, height);

            for (int y = startingRow; y < startingRow + height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    ItemDrop.ItemData item = playerInventory.GetItemAt(x, y);
                    if (item != null && !item.m_shared.m_questItem && !item.m_equiped)
                    {
                        GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch moving item to grave " + item.m_shared.m_name, LogLevel.Debug);
                        tempInventory.MoveItemToThis(playerInventory, item, item.m_stack, x, (y-startingRow) % height);
                    }
                }
            }

            if (tempInventory.GetAllItems().Count == 0)
            {
                GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch grave would be empty, not creating", LogLevel.Debug);
                return;
            }

            GravekeeperPlugin.Log("Inventory_MoveInventoryToGrave_PrefixPatch grave has items, creating grave", LogLevel.Debug);

            Player player = Player.m_localPlayer;
            PlayerProfile playerProfile = Game.instance.GetPlayerProfile();

            Vector2 random = Random.insideUnitCircle.normalized;
            Vector3 position = new Vector3(player.GetCenterPoint().x + (random.x * .5f), player.GetCenterPoint().y, player.GetCenterPoint().z + (random.y * .5f));

            GameObject obj = Object.Instantiate(player.m_tombstone, position, player.transform.rotation);
            Inventory graveInventory = obj.GetComponent<Container>().GetInventory();
            graveInventory.MoveAll(tempInventory);
            TombStone component = obj.GetComponent<TombStone>();
            component.Setup(playerProfile.GetName() + Settings.Grave.ExtraGravesSuffix.Value, playerProfile.GetPlayerID());
        }
    }
}
