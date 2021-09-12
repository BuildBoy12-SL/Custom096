// -----------------------------------------------------------------------
// <copyright file="EndCharge.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

using Exiled.API.Features;

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
    /// Patches <see cref="Scp096.EndCharge"/> to implement <see cref="Charge.Cooldown"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.EndCharge))]
    internal static class EndCharge
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.OperandIs(Method(typeof(Scp096), nameof(Scp096.ParseChargeCooldown)))) - 1;

            newInstructions.RemoveRange(index, 2);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Charge))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Charge), nameof(Charge.Cooldown))),
            });

            newInstructions.InsertRange(newInstructions.Count - 1, new[]
            {
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldsfld, Field(typeof(Scp096), nameof(Scp096._chargeCooldown))),
                new CodeInstruction(OpCodes.Box, typeof(float)),
                new CodeInstruction(OpCodes.Call, Method(typeof(Log), nameof(Log.Error))),
                new CodeInstruction(OpCodes.Pop),
            });

            for (int z = 0; z < newInstructions.Count; z++)
            {
                Log.Info(newInstructions[z]);
                yield return newInstructions[z];
            }

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}