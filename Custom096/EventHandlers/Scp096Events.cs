// -----------------------------------------------------------------------
// <copyright file="Scp096Events.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.EventHandlers
{
    using Exiled.Events.EventArgs;
    using PlayableScps;
    using UnityEngine;

    /// <summary>
    /// Handles events derived from <see cref="Exiled.Events.Handlers.Scp096"/>.
    /// </summary>
    public class Scp096Events
    {
        private readonly Config config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Scp096Events"/> class.
        /// </summary>
        /// <param name="config">An instance of the <see cref="Config"/> class.</param>
        public Scp096Events(Config config) => this.config = config;

        /// <summary>
        /// Subscribes to all scp096 events.
        /// </summary>
        public void Subscribe()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget += OnAddingTarget;
            Exiled.Events.Handlers.Scp096.ChargingPlayer += OnChargingPlayer;
        }

        /// <summary>
        /// Unsubscribes from all scp096 events.
        /// </summary>
        public void Unsubscribe()
        {
            Exiled.Events.Handlers.Scp096.AddingTarget -= OnAddingTarget;
            Exiled.Events.Handlers.Scp096.ChargingPlayer -= OnChargingPlayer;
        }

        private void OnAddingTarget(AddingTargetEventArgs ev)
        {
            if (ev.Target.SessionVariables.ContainsKey("IsNPC"))
            {
                ev.IsAllowed = false;
                return;
            }

            if (ev.Target.Role == RoleType.Tutorial && !config.Rage.TutorialsCanEnrage)
            {
                ev.IsAllowed = false;
                return;
            }

            ev.Scp096.MaxArtificialHealth += Mathf.Clamp(config.Health.AhpPerTarget, 0, config.Health.MaximumAhp - ev.Scp096.MaxArtificialHealth);
            ev.Scp096.ArtificialHealth += (ushort)Mathf.Clamp(config.Health.AhpPerTarget, 0, ev.Scp096.MaxArtificialHealth - ev.Scp096.ArtificialHealth);
        }

        private void OnChargingPlayer(ChargingPlayerEventArgs ev)
        {
            ev.Damage = ev.IsTarget ? config.Charge.Damage : config.Charge.NonTargetDamage;
            ev.EndCharge = config.Charge.KillEndsCharge;
        }
    }
}