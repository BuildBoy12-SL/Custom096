// -----------------------------------------------------------------------
// <copyright file="ParseChargeCooldown.cs" company="Build">
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
    /// Patches and overrides <see cref="Scp096.ParseChargeCooldown"/> to implement <see cref="Charge.BaseCooldown"/> and <see cref="Charge.HitTargetReward"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.ParseChargeCooldown))]
    internal static class ParseChargeCooldown
    {
        private static bool Prefix(Scp096 __instance, ref float __result)
        {
            __result = Plugin.Instance.Config.Charge.BaseCooldown;
            if (__instance._chargeKilled)
                __result -= Plugin.Instance.Config.Charge.HitTargetReward;

            return false;
        }
    }
}