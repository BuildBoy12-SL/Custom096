// -----------------------------------------------------------------------
// <copyright file="PreWindupPatch.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Patches
{
#pragma warning disable SA1313
    using System;
    using Custom096.Configs;
    using HarmonyLib;
    using Mirror;
    using PlayableScps;

    /// <summary>
    /// Patches <see cref="Scp096.PreWindup"/> to implement <see cref="Rage.StripWindupTimeDelay"/>.
    /// </summary>
    [HarmonyPatch(typeof(Scp096), nameof(Scp096.PreWindup))]
    internal static class PreWindupPatch
    {
        private static bool Prefix(Scp096 __instance, float delay = 0.0f)
        {
            if (!NetworkServer.active)
                throw new InvalidOperationException("Called PreWindup from client.");

            if (Plugin.Instance.Config.Rage.StripWindupTimeDelay)
                delay = 0.1f;

            __instance._preWindupTime = delay;
            return false;
        }
    }
}