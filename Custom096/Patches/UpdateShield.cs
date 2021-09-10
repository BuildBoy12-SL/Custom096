// -----------------------------------------------------------------------
// <copyright file="UpdateShield.cs" company="Build">
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
    /// Patches <see cref="Scp096.UpdateShield"/> to implement <see cref="Health.RechargeRate"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.UpdateShield))]
    internal static class UpdateShield
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            int index = newInstructions.FindIndex(instruction => instruction.OperandIs(PropertySetter(typeof(Scp096), nameof(Scp096.ShieldRechargeRate)))) - 2;

            newInstructions.RemoveRange(index, 2);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Health))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Health), nameof(Health.RechargeRate))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}