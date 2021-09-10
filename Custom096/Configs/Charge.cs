// -----------------------------------------------------------------------
// <copyright file="Charge.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Settings related to Scp096's charge.
    /// </summary>
    public class Charge
    {
        /// <summary>
        /// Gets or sets a value indicating whether Scp096 killing a user during their charge will end it.
        /// </summary>
        [Description("Whether Scp096 killing a user during their charge will end it.")]
        public bool KillEndsCharge { get; set; } = false;

        /// <summary>
        /// Gets or sets how much damage Scp096 will deal to a target.
        /// </summary>
        [Description("How much damage Scp096 will deal to a target.")]
        public float Damage { get; set; } = 9696f;

        /// <summary>
        /// Gets or sets how much damage Scp096 will deal to a non-target.
        /// </summary>
        [Description("How much damage Scp096 will deal to a non-target.")]
        public float NonTargetDamage { get; set; } = 35f;

        /// <summary>
        /// Gets or sets the duration, in seconds, of the charge.
        /// </summary>
        [Description("The duration, in seconds, of the charge.")]
        public float Duration { get; set; } = 0.8f;

        /// <summary>
        /// Gets or sets the cooldown, in seconds, between charges.
        /// </summary>
        [Description("The cooldown, in seconds, between charges.")]
        public float Cooldown { get; set; } = 7f;

        /// <summary>
        /// Gets or sets the range that Scp096 can hit targets while charging.
        /// </summary>
        public float HitboxSize { get; set; } = 1.5f;
    }
}