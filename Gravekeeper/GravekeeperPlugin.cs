using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace Gravekeeper
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class GravekeeperPlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.gravekeeper";

        private const string NAME = "Gravekeeper";

        private const string VERSION = "1.1.2";

        private static ManualLogSource logger;

        void Awake()
        {
            logger = Logger;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
        static void SaveInventory(out InventoryTracker __state, Inventory __instance, Inventory original)
        {
            __state = new InventoryTracker(__instance, original);
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
        static void RestoreInventory(InventoryTracker __state)
        {
            __state.PlayerInventory.MoveAll(__state.GraveInventory);
            logger.LogInfo(__state.PlayerInventory.GetAllItems().Count + " items recovered.");
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
