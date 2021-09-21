// -----------------------------------------------------------------------
// <copyright file="TryNotToCryPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
#pragma warning disable SA1118
#pragma warning disable SA1313
    using System.Collections.Generic;
    using System.Reflection.Emit;
    using HarmonyLib;
    using Interactables.Interobjects.DoorUtils;
    using NorthwoodLib.Pools;
    using PlayableScps;
    using UnityEngine;
    using static HarmonyLib.AccessTools;

    /// <summary>
    /// Patches <see cref="Scp096.TryNotToCry"/> to implement <see cref="Configs.TryNotToCry.AnySurface"/> and <see cref="Configs.TryNotToCry.MaximumDistance"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.TryNotToCry))]
    internal static class TryNotToCryPatch
    {
        private static readonly int TntcLayer = LayerMask.GetMask("Default", "Door", "Glass");

        private static bool Prefix(Scp096 __instance)
        {
            if (!Plugin.Instance.Config.TryNotToCry.AnySurface)
                return true;

            if (!Physics.Raycast(__instance.Hub.PlayerCameraReference.position, __instance.Hub.PlayerCameraReference.forward, out var hitInfo, Plugin.Instance.Config.TryNotToCry.MaximumDistance, TntcLayer))
                return false;

            DoorVariant door = hitInfo.collider.gameObject.GetComponentInParent<DoorVariant>();
            if (door && !door.IsConsideredOpen() && !Scp096._takenDoors.ContainsKey(__instance.Hub))
                Scp096._takenDoors.Add(__instance.Hub, door);

            __instance.PlayerState = Scp096PlayerState.TryNotToCry;
            return false;
        }

        private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> newInstructions = ListPool<CodeInstruction>.Shared.Rent(instructions);

            int index = newInstructions.FindIndex(instruction => instruction.opcode == OpCodes.Ldc_R4);

            newInstructions.RemoveAt(index);

            newInstructions.InsertRange(index, new[]
            {
                new CodeInstruction(OpCodes.Call, PropertyGetter(typeof(Plugin), nameof(Plugin.Instance))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Plugin), nameof(Plugin.Config))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Config), nameof(Config.TryNotToCry))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Configs.TryNotToCry), nameof(Configs.TryNotToCry.MaximumDistance))),
            });

            for (int z = 0; z < newInstructions.Count; z++)
                yield return newInstructions[z];

            ListPool<CodeInstruction>.Shared.Return(newInstructions);
        }
    }
}