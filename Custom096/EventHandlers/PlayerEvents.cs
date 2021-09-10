// -----------------------------------------------------------------------
// <copyright file="PlayerEvents.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.EventHandlers
{
    using Exiled.API.Features;
    using Exiled.Events.EventArgs;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Player"/>.
    /// </summary>
    public class PlayerEvents
    {
        private readonly Config config;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerEvents"/> class.
        /// </summary>
        /// <param name="config">An instance of the <see cref="Config"/> class.</param>
        public PlayerEvents(Config config) => this.config = config;

        /// <summary>
        /// Subscribes to all player events.
        /// </summary>
        public void Subscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole += OnChangingRole;
            Exiled.Events.Handlers.Player.Died += OnDied;
        }

        /// <summary>
        /// Unsubscribes from all player events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Player.ChangingRole -= OnChangingRole;
            Exiled.Events.Handlers.Player.Died -= OnDied;
        }

        private void OnChangingRole(ChangingRoleEventArgs ev)
        {
            foreach (var player in Player.List)
            {
                if (player.CurrentScp is PlayableScps.Scp096 scp)
                    scp._targets.Remove(ev.Player.ReferenceHub);
            }
        }

        private void OnDied(DiedEventArgs ev)
        {
            if (!(ev.Killer?.CurrentScp is PlayableScps.Scp096 scp096))
                return;

            scp096._chargeCooldown -= config.Rage.ChargeTimeOnKill;
            scp096.AddReset();
        }
    }
}