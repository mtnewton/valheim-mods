using HarmonyLib;

namespace BiggerPickupRadius
{
    [HarmonyPatch(typeof(Player), "Awake")]
    class IncreasePickupRadius
    {
        static void Postfix(Player __instance)
        {
            float original = __instance.m_autoPickupRange;
            uint mult = BiggerPickupRadiusPlugin.getRadiusMultiplier();
            __instance.m_autoPickupRange *= mult;
            BiggerPickupRadiusPlugin.Log(
                "Player pickup range increased from " + 
                original + " to " + __instance.m_autoPickupRange + 
                " (x" + mult + ")."
            );
        }
    }
}
