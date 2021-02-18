using HarmonyLib;

namespace Gravekeeper
{
    [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
    class KeepItemsOnDeath
    {
        static void Prefix(out InventoryTracker __state, Inventory __instance, Inventory original)
        {
            __state = new InventoryTracker(__instance, original);
        }

        static void Postfix(InventoryTracker __state)
        {
            __state.PlayerInventory.MoveAll(__state.GraveInventory);

            GravekeeperPlugin.Log(__state.PlayerInventory.GetAllItems().Count + " items recovered.");
        }
    }

    class InventoryTracker
    {
        public InventoryTracker(Inventory graveInventory, Inventory playerInventory) 
        {
            GraveInventory = graveInventory;
            PlayerInventory = playerInventory;
        }
        public Inventory GraveInventory { get; set; }
        public Inventory PlayerInventory { get; set; }
    }
}
