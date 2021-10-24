// -----------------------------------------------------------------------
// <copyright file="WindupPatch.cs" company="Build">
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
    /// Patches <see cref="Scp096.Windup"/> to implement <see cref="Rage.WindupTime"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.Windup))]
    internal static class WindupPatch
    {
        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4);

            newInstructions.RemoveAt(index);

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.Rage))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Rage), nameof(Rage.WindupTime))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}