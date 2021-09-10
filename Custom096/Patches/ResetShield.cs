// -----------------------------------------------------------------------
// <copyright file="ResetShield.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
#pragma warning disable SA1313
    using Custom096.Configs;
    using HarmonyLib;
    using PlayableScps;
    using UnityEngine;

    /// <summary>
    /// Patches and overrides <see cref="Scp096.ResetShield"/> to aid in the implementation of <see cref="Health.DefaultAhp"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.ResetShield))]
    internal static class ResetShield
    {
        private static bool Prefix(Scp096 __instance)
        {
            __instance.CurMaxShield = Plugin.Instance.Config.Health.DefaultAhp;
            __instance.ShieldAmount = Mathf.Min(Plugin.Instance.Config.Health.DefaultAhp, __instance.Hub.playerStats.ArtificialHealth);
            return false;
        }
    }
}