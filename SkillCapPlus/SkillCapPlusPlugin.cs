using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using UnityEngine;

namespace SkillCapPlus
{
    [BepInPlugin(GUID, NAME, VERSION)]
    [HarmonyPatch]
    public class SkillCapPlusPlugin : BaseUnityPlugin
    {
        private const string GUID = "net.mtnewton.skillcapplus";

        private const string NAME = "SkillCapPlus";

        private const string VERSION = "1.0.0";

        private static ManualLogSource logger;

        private static float xpMultiplier;

        private static bool removeLevelUpEffect;

        private static int maxSkillLevel;

        private static bool enableTotalSkillLevelCap;

        private static int totalSkillLevelCap;

        private static bool skillLossEnabled;

        private static float skillLossPercent;

        private static int skillLossLevels;

        private static bool skillLossProgress;

        private static SkillLossType skillLossType;

        private static FieldInfo skill_m_level_fieldInfo = AccessTools.Field(typeof(Skills.Skill), "m_level");

        private static FieldInfo skill_m_accumulator_fieldInfo = AccessTools.Field(typeof(Skills.Skill), "m_accumulator");

        private static MethodInfo mathf_clamp_methodInfo = AccessTools.Method(typeof(Mathf), "Clamp", new Type[] { typeof(float), typeof(float), typeof(float) });

        enum SkillLossType
        {
            Percent,
            Levels
        }

        void Awake()
        {
            logger = Logger;

            xpMultiplier = Config.Bind(NAME, "SkillXpMultiplier", 3f,
                new ConfigDescription("Multiplies the skill experience you get.", new AcceptableValueRange<float>(0, int.MaxValue))
            ).Value;

            removeLevelUpEffect = Config.Bind(NAME, "RemoveLevelUpEffect", false, "Remove the level up effect from running when leveling up?").Value;

            maxSkillLevel = Config.Bind(NAME, "MaxSkillLevel", 100, 
                new ConfigDescription("The max level a skill can be.", new AcceptableValueRange<int>(0, int.MaxValue))
            ).Value;

            enableTotalSkillLevelCap = Config.Bind(NAME, "EnableTotalSkillLevel", false, "Should a player be restricted to have a max total skill level?").Value;

            totalSkillLevelCap = Config.Bind(NAME, "MaxSkillTotalLevel", 600,  
                new ConfigDescription("The max total skill level a player can have.", new AcceptableValueRange<int>(0, int.MaxValue))
            ).Value;

            skillLossEnabled = Config.Bind(NAME, "SkillLossEnabled", false, "Should you lose skill experience on death?").Value;

            skillLossType = Config.Bind(NAME, "SkillLossType", SkillLossType.Levels, "If SkillLossEnabled=true How should the player lose skill experience?").Value;

            skillLossPercent = Config.Bind(NAME, "SkillLossPercent", .25f,
                new ConfigDescription("If SkillLossType=Percent What percent of skill levels should be lost on death?", new AcceptableValueRange<float>(0, 1))
            ).Value;

            skillLossLevels = Config.Bind(NAME, "SkillLossLevels", 0,
                new ConfigDescription("If SkillLossType=Levels How many skill levels should be lost on death?", new AcceptableValueRange<int>(0, int.MaxValue)
            )).Value;

            skillLossProgress = Config.Bind(NAME, "SkillLossProgress", true,
                "Should the progress to the next level be lost?"
            ).Value;

            Harmony harmony = new Harmony(GUID);
            harmony.PatchAll();

            logger.LogInfo(NAME + " loaded.");
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(Skills), "Awake")]
        static void SetSkillLossAndCap_PostfixPatch(ref Skills __instance)
        {
            float factor = 0f;
            if (skillLossType == SkillLossType.Percent)
            {
                factor = skillLossPercent;
            }

            logger.LogInfo("PostfixPatch: Skills.Awake set m_DeathLowerFactor");
            __instance.m_DeathLowerFactor = factor;

            logger.LogInfo("PostfixPatch: Skills.Awake set m_useSkillCap");
            __instance.m_useSkillCap = enableTotalSkillLevelCap;

            logger.LogInfo("PostfixPatch: Skills.Awake set m_totalSkillCap");
            __instance.m_totalSkillCap = totalSkillLevelCap;

        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(Player), "OnSkillLevelup")]
        static bool RemoveLevelUpEffect_PrefixPatch()
        {
            if (removeLevelUpEffect)
            {
                logger.LogInfo("PrefixPatch: Preventing level up effect");
                return false;
            }
            return true;
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Player), "RaiseSkill")]
        static IEnumerable<CodeInstruction> XpMultiplier_TranspilerPatch(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();

            for (int i = 0; i < il.Count; ++i)
            {
                // float multiplier = 1f;
                //IL_0000: ldc.r4 1
                //IL_0005: stloc.0
                if (il[i].opcode == OpCodes.Stloc_0 &&
                    il[i-1].opcode == OpCodes.Ldc_R4 && il[i-1].operand.Equals(1f)
                ) {
                    logger.LogInfo("TranspilerPatch: Player.RaiseSkill replace 1f in (multiplier = xpMultiplier) with xpMultiplier");
                    il[i-1].operand = xpMultiplier;
                }
            }

            return il.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Skills), "LowerAllSkills")]
        static IEnumerable<CodeInstruction> ModifySkillLoss_TranspilerPatch(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();

            for (int i = 0; i < il.Count; ++i)
            {

                if (skillLossEnabled && skillLossType == SkillLossType.Levels)
                {
                    //IL_002d: ldfld float32 Skills / Skill::m_level
                    //IL_0032: ldloc.2
                    //IL_0033: sub
                    if (il[i].opcode == OpCodes.Sub &&
                        il[i - 1].opcode == OpCodes.Ldloc_2 &&
                        il[i - 2].opcode == OpCodes.Ldfld && il[i - 2].operand.Equals(skill_m_level_fieldInfo)
                    ) {
                        logger.LogInfo("TranspilerPatch: Skills.LowerAllSkills replace num in (m_level -= num) with skillLevelLoss");
                        il[i - 1] = new CodeInstruction(OpCodes.Ldc_R4, (float)skillLossLevels);

                        //after sub
                        //load float 0 
                        //load float maxskilllevel
                        //call mathf clamp
                        logger.LogInfo("TranspilerPatch: Skills.LowerAllSkills add Mathf.Clamp(m_level, 0, maxSkillLevel)");
                        il.Insert(i + 1, new CodeInstruction(OpCodes.Ldc_R4, 0f));
                        il.Insert(i + 2, new CodeInstruction(OpCodes.Ldc_R4, (float)maxSkillLevel));
                        il.Insert(i + 3, new CodeInstruction(OpCodes.Call, mathf_clamp_methodInfo));

                    }
                }

                if (skillLossEnabled == false || skillLossProgress == false)
                {
                    //IL_0039: ldloca.s 1
                    //IL_003b: call instance !1 valuetype[mscorlib]System.Collections.Generic.KeyValuePair`2 < valuetype Skills / SkillType, class Skills/Skill>::get_Value()
                    //IL_0040: ldc.r4 0.0
                    //IL_0045: stfld float32 Skills/Skill::m_accumulator
                    if (il[i].opcode == OpCodes.Stfld && il[i].operand.Equals(skill_m_accumulator_fieldInfo) &&
                        il[i - 1].opcode == OpCodes.Ldc_R4 && il[i - 1].operand.Equals(0f) &&
                        il[i - 2].opcode == OpCodes.Call
                    ) {
                        logger.LogInfo("TranspilerPatch: Skills.LowerAllSkills remove m_accumulator = 0f");
                        il[i - 1].opcode = OpCodes.Nop;
                        il[i].opcode = OpCodes.Nop;
                    }
                }
            }

            return il.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Skills.Skill), "Raise")]
        static IEnumerable<CodeInstruction> ModifyMaxSkillLevel_TranspilerPatch(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();

            for (int i = 0; i < il.Count; ++i)
            {
                // if (m_level >= 100f)
                //IL_0000: ldarg.0
                //IL_0001: ldfld float32 Skills / Skill::m_level
                //IL_0006: ldc.r4 100
                //IL_000b: blt.un.s IL_000f  <- ? logs as - blt.un System.Reflection.Emit.Label
                if (
                    il[i].opcode == OpCodes.Ldc_R4 && il[i - 1].operand.Equals(100f) &&
                    il[i - 1].LoadsField(skill_m_level_fieldInfo) &&
                    il[i - 2].opcode == OpCodes.Ldarg_0
                ) {
                    logger.LogInfo("TranspilerPatch: Skill.Raise replace 100f in (m_level >= 100f) with maxSkillLevel");
                    il[i].operand = (float)maxSkillLevel;
                }

                // Mathf.Clamp(m_level, 0f, 100f)
                //IL_004f: ldfld float32 Skills / Skill::m_level
                //IL_0054: ldc.r4 0.0
                //IL_0059: ldc.r4 100
                //IL_005e: call float32[UnityEngine.CoreModule]UnityEngine.Mathf::Clamp(float32, float32, float32)
                if (il[i].opcode == OpCodes.Call && il[i].operand.Equals(mathf_clamp_methodInfo) &&
                    il[i - 1].opcode == OpCodes.Ldc_R4 && il[i - 1].operand.Equals(100f) &&
                    il[i - 2].opcode == OpCodes.Ldc_R4 && il[i - 2].operand.Equals(0f) &&
                    il[i - 3].opcode == OpCodes.Ldfld && il[i - 3].operand.Equals(skill_m_level_fieldInfo)
                ) {
                    logger.LogInfo("TranspilerPatch: Skill.Raise replace 100f in Mathf.Clamp(m_level, 0f, 100f) with maxSkillLevel");
                    il[i - 1].operand = (float)maxSkillLevel;
                }
            }
            il.ForEach(i => logger.LogInfo($"{i.opcode.ToString()} {i.operand ? .ToString()}"));
            return il.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(typeof(Skills), "RebalanceSkills")]
        static IEnumerable<CodeInstruction> FixDivByZero_TranspilerPatch(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> il = instructions.ToList();

            for (int i = 0; i < il.Count; ++i)
            {
                // skillDatum2.Value.m_level = skillDatum2.Value.m_level / num2 * num
                //IL_00a3: ldfld float32 Skills/Skill::m_level
                //IL_00a8: ldloc.2
                //IL_00a9: div
                //IL_00aa: ldloc.1
                //IL_00ab: mul
                //IL_00ac: stfld float32 Skills / Skill::m_level
                if (il[i].opcode == OpCodes.Stfld && il[i].operand.Equals(skill_m_level_fieldInfo) &&
                    il[i - 1].opcode == OpCodes.Mul &&
                    il[i - 2].opcode == OpCodes.Ldloc_1 &&
                    il[i - 3].opcode == OpCodes.Div
                ) {
                    logger.LogInfo("TranspilerPatch: Skills.RebalanceSkills add Mathf.Clamp(newLevel, 0, maxSkillLevel)");
                    il.Insert(i, new CodeInstruction(OpCodes.Ldc_R4, 0f));
                    il.Insert(i + 1, new CodeInstruction(OpCodes.Ldc_R4, (float)maxSkillLevel));
                    il.Insert(i + 2, new CodeInstruction(OpCodes.Call, mathf_clamp_methodInfo));
                }
            }

            return il.AsEnumerable();
        }
    }
}