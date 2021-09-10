// -----------------------------------------------------------------------
// <copyright file="UpdateTryNotToCry.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
    using HarmonyLib;
    using PlayableScps;

    /// <summary>
    /// Patches <see cref="Scp096.UpdateTryNotToCry"/> to prevent interference with the <see cref="Configs.TryNotToCry.AnySurface"/> config.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.UpdateTryNotToCry))]
    internal static class UpdateTryNotToCry
    {
        private static bool Prefix() => !Plugin.Instance.Config.TryNotToCry.AnySurface;
    }
}