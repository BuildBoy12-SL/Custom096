// -----------------------------------------------------------------------
// <copyright file="SetupShieldPatch.cs" company="Build">
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

    /// <summary>
    /// Patches <see cref="Scp096.SetupShield"/> to implement <see cref="Health.DefaultAhp"/> and <see cref="Health.AhpMultiplier"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.SetupShield))]
    internal static class SetupShieldPatch
    {
        private static bool Prefix(Scp096 __instance)
        {
            Health health = Plugin.Instance.Config.Health;
            __instance.Shield.Limit = health.DefaultAhp;
            __instance.Shield.DecayRate = -health.RechargeRate;
            __instance.ShieldAmount = health.DefaultAhp;
            return false;
        }
    }
}