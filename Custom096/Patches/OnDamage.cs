// -----------------------------------------------------------------------
// <copyright file="OnDamage.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
#pragma warning disable SA1118
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using Custom096.Configs;
    using HarmonyLib;
    using NorthwoodLib.Pools;
    using PlayableScps;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="Scp096.OnDamage"/> to implement <see cref="Rage.EnrageOnDamage"/> and <see cref="Health.RechargeDelayAfterDamage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.OnDamage))]
    internal static class OnDamage
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldarg_0);
            Label setRechargeLabel = newInstructions[index].labels[0];

            index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Brfalse_S) + 1;
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Rage))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Rage), nameof(Rage.EnrageOnDamage))),
                new CodeInstruction(OpCodes.Brfalse_S, setRechargeLabel),
            });

            index = newInstructions.FindLastIndex(instruction => instruction.opcode == OpCodes.Ldc_R4);
            newInstructions.RemoveAt(index);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Health))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Health), nameof(Health.RechargeDelayAfterDamage))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}