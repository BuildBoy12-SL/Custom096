// -----------------------------------------------------------------------
// <copyright file="AddResetPatch.cs" company="Build">
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
    /// Patches <see cref="Scp096.AddReset"/> with modified values from <see cref="Configs.Rage"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.AddReset))]
    internal static class AddResetPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);
            var rageConfig = generator.DeclareLocal(typeof(Rage));

            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Stloc_0) - 2;
            List<Label> labels = newInstructions[index].labels;
            newInstructions.RemoveRange(index, 2);
            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))).WithLabels(labels),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Rage))),
                new CodeInstruction(OpCodes.Stloc_S, rageConfig.LocalIndex),
                new CodeInstruction(OpCodes.Ldloc_S, rageConfig.LocalIndex),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Rage), nameof(Rage.RageTimePerTarget))),
            });

            for (int i = 0; i < newInstructions.Count; i++)
            {
                if (newInstructions[i].opcode != OpCodes.Ldc_R4)
                    continue;

                newInstructions.RemoveAt(i);
                newInstructions.InsertRange(i, new[]
                {
                    new CodeInstruction(OpCodes.Ldloc_S, rageConfig.LocalIndex),
                    new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Rage), nameof(Rage.MaximumAddedRageTime))),
                });
            }

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}