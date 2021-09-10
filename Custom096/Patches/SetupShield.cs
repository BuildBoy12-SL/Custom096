// -----------------------------------------------------------------------
// <copyright file="SetupShield.cs" company="Build">
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
    internal static class SetupShield
    {
        private static bool Prefix(Scp096 __instance)
        {
            __instance._prevArtificialHpDelay = __instance.Hub.playerStats.ArtificialHpDecay;
            __instance._prevArtificialHpRatio = __instance.Hub.playerStats.ArtificialNormalRatio;
            __instance._prevMaxArtificialHp = __instance.Hub.playerStats.MaxArtificialHealth;

            Health health = Plugin.Instance.Config.Health;
            __instance.Hub.playerStats.NetworkMaxArtificialHealth = health.DefaultAhp;
            __instance.ShieldAmount = health.DefaultAhp;
            __instance.Hub.playerStats.NetworkArtificialNormalRatio = health.AhpMultiplier;
            return false;
        }
    }
}