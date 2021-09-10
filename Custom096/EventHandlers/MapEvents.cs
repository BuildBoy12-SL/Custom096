// -----------------------------------------------------------------------
// <copyright file="MapEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.EventHandlers
{
    using System.Linq;
    using Exiled.Events.EventArgs;
    using PlayableScps;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Map"/>.
    /// </summary>
    public class MapEvents
    {
        private readonly Config config;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapEvents"/> class.
        /// </summary>
        /// <param name="config">An instance of the <see cref="Config"/> class.</param>
        public MapEvents(Config config) => this.config = config;

        /// <summary>
        /// Subscribes to all map events.
        /// </summary>
        public void Subscribe()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade += OnExplodingGrenade;
        }

        /// <summary>
        /// Unsubscribes from all map events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Map.ExplodingGrenade -= OnExplodingGrenade;
        }

        private void OnExplodingGrenade(ExplodingGrenadeEventArgs ev)
        {
            if (ev.IsFrag || !config.Rage.DisableFlashing)
                return;

            foreach (var target in ev.TargetsToAffect.ToList())
            {
                if (target.CurrentScp is Scp096 scp096 && scp096.Enraged)
                    ev.TargetsToAffect.Remove(target);
            }
        }
    }
}