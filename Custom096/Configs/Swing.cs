// -----------------------------------------------------------------------
// <copyright file="Swing.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Settings related to Scp096's main attack.
    /// </summary>
    public class Swing
    {
        /// <summary>
        /// Gets or sets the amount of time, in seconds, between each swing.
        /// </summary>
        [Description("The amount of time, in seconds, between each swing.")]
        public float AttackCooldown { get; set; } = 0.1f;

        /// <summary>
        /// Gets or sets the amount of time, in seconds, that Scp096's swing registers hits.
        /// </summary>
        [Description("The amount of time, in seconds, that Scp096's swing registers hits.")]
        public float AttackDuration { get; set; } = 0.5f;
    }
}