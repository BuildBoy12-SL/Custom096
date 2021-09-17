// -----------------------------------------------------------------------
// <copyright file="Health.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Settings related to Scp096's health and hume shield.
    /// </summary>
    public class Health
    {
        /// <summary>
        /// Gets or sets how much ahp Scp096 should start with.
        /// </summary>
        [Description("How much ahp Scp096 should start with.")]
        public int DefaultAhp { get; set; } = 1000;

        /// <summary>
        /// Gets or sets the amount of hume to be gained for each target.
        /// </summary>
        [Description("The amount of hume to be gained for each target.")]
        public int AhpPerTarget { get; set; } = 0;

        /// <summary>
        /// Gets or sets the maximum hume that Scp096 can have at one time.
        /// </summary>
        [Description("The maximum hume that Scp096 can have at one time.")]
        public int MaximumAhp { get; set; } = 2000;

        /// <summary>
        /// Gets or sets a multiplier of how strong ahp is compared to hp.
        /// </summary>
        [Description("Multiplier of how strong AHP is compared to HP. 1 makes them identical, 2 makes 1 ahp = 2hp, etc.")]
        public float AhpMultiplier { get; set; } = 1f;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, it takes for Scp096 to start regenerating hume after being shot.
        /// </summary>
        [Description("The amount of time, in seconds, it takes for Scp096 to start regenerating hume after being shot.")]
        public float RechargeDelayAfterDamage { get; set; } = 25f;

        /// <summary>
        /// Gets or sets how much hume shield will be regenerated per second.
        /// </summary>
        [Description("How much hume shield will be regenerated per second.")]
        public float RechargeRate { get; set; } = 40f;
    }
}