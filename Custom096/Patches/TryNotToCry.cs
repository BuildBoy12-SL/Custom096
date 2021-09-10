// -----------------------------------------------------------------------
// <copyright file="TryNotToCry.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
#pragma warning disable SA1313
    using HarmonyLib;
    using Interactables.Interobjects.DoorUtils;
    using PlayableScps;
    using UnityEngine;

    /// <summary>
    /// Patches <see cref="Scp096.TryNotToCry"/> to implement <see cref="Configs.TryNotToCry.AnySurface"/> and <see cref="Configs.TryNotToCry.MaximumDistance"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.TryNotToCry))]
    internal static class TryNotToCry
    {
        private static int TntcLayer { get; } = LayerMask.GetMask("Default", "Door", "Glass");

        private static bool Prefix(Scp096 __instance)
        {
            if (!Plugin.Instance.Config.TryNotToCry.AnySurface)
                return true;

            if (!Physics.Raycast(__instance.Hub.PlayerCameraReference.position, __instance.Hub.PlayerCameraReference.forward, out var hitInfo, Plugin.Instance.Config.TryNotToCry.MaximumDistance, TntcLayer))
                return false;

            DoorVariant door = hitInfo.collider.gameObject.GetComponentInParent<DoorVariant>();
            if (door && !door.IsConsideredOpen() && !Scp096._takenDoors.ContainsKey(__instance.Hub.gameObject))
                Scp096._takenDoors.Add(__instance.Hub.gameObject, door);

            __instance.PlayerState = Scp096PlayerState.TryNotToCry;
            return false;
        }
    }
}