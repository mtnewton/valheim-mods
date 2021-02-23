using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Linq;

namespace Gravekeeper
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class GravekeeperPlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.gravekeeper";

        private const string NAME = "Gravekeeper";

        private const string VERSION = "1.2.0";

        private static ManualLogSource logger;

        private static bool keepGravestone;

        private static bool keepItemsEquipped;

        private static ItemDrop.ItemData stoneItemData = null;

        void Awake()
        {
            logger = Logger;

            keepGravestone = Config.Bind(NAME, "KeepGravestone", false,
                "Should the gravestone stay in the world? (you still keep your items) One stone is added to the graves inventory to keep it standing."
            ).Value;

            keepItemsEquipped = Config.Bind(NAME, "KeepItemsEquipped", true,
                "Should items stay equipped when you respawn?"
            ).Value;

            var harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Inventory), "MoveInventoryToGrave")]
        static bool MoveInventoryToGrave_PrefixPatch(ref Inventory __instance)
        {
            __instance.m_inventory.Clear();
            if (keepGravestone)
            {
                ItemDrop.ItemData stone = GetStoneItemData().Clone();
                if (stone != null)
                {
                    __instance.m_inventory.Add(stone);
                    logger.LogInfo("1 stone added to gravestone so that it remains.");
                }
            }
            __instance.Changed();
            return false;
        }

        private static MethodInfo UnequipAllItems = AccessTools.Method(typeof(Humanoid), "UnequipAllItems");

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Player), "CreateTombStone")]
        static IEnumerable<CodeInstruction> CreateTombstone_TranspilerPatch(IEnumerable<CodeInstruction> instructions)
        {
            if (!keepItemsEquipped)
            {
                logger.LogInfo("Equipment will be unequipped.");

                return instructions;
            }

            List<CodeInstruction> il = instructions.ToList();
           
            for (int i = 0; i < il.Count; ++i)
            { 
                if (il[i].Calls(UnequipAllItems))
                {
                    il[i - 1].opcode = OpCodes.Nop;
                    il[i].opcode = OpCodes.Nop;
                }
            }

            logger.LogInfo("Equipment will remain equipped.");

            return il.AsEnumerable();
        }

        private static ItemDrop.ItemData GetStoneItemData()
        {
            if (stoneItemData != null)
            {
                return stoneItemData;
            }

            List<ItemDrop> items = ObjectDB.m_instance.GetAllItems(ItemDrop.ItemData.ItemType.Material, "Stone");
            foreach (ItemDrop item in items)
            {
                if (item.m_itemData.m_shared.m_name == "$item_stone")
                {
                    stoneItemData = item.m_itemData;
                    break;
                }
            }
            return stoneItemData;
        }
    }
}
