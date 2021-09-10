// -----------------------------------------------------------------------
// <copyright file="Plugin.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096
{
    using System;
    using Custom096.EventHandlers;
    using Exiled.API.Features;
    using HarmonyLib;

    /// <summary>
    /// The main plugin class.
    /// </summary>
    public class Plugin : Plugin<Config>
    {
        private MapEvents mapEvents;
        private PlayerEvents playerEvents;
        private Scp096Events scp096Events;
        private Harmony harmony;

        /// <summary>
        /// Gets the only existing instance of the <see cref="Plugin"/> class.
        /// </summary>
        public static Plugin Instance { get; private set; }

        /// <inheritdoc />
        public override Version RequiredExiledVersion { get; } = new Version(3, 0, 0);

        /// <inheritdoc />
        public override void OnEnabled()
        {
            Instance = this;

            harmony = new Harmony($"build.custom096.{DateTime.UtcNow.Ticks}");
            harmony.PatchAll();

            mapEvents = new MapEvents(Config);
            playerEvents = new PlayerEvents(Config);
            scp096Events = new Scp096Events(Config);

            mapEvents.Subscribe();
            playerEvents.Subscribe();
            scp096Events.Subscribe();
            base.OnEnabled();
        }

        /// <inheritdoc />
        public override void OnDisabled()
        {
            mapEvents.Unsubscribe();
            playerEvents.Unsubscribe();
            scp096Events.Unsubscribe();

            mapEvents = null;
            playerEvents = null;
            scp096Events = null;

            harmony.UnpatchAll();
            harmony = null;

            Instance = null;
            base.OnDisabled();
        }
    }
}