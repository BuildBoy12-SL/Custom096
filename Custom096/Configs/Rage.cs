// -----------------------------------------------------------------------
// <copyright file="Rage.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Settings relating to Scp096's enraged state.
    /// </summary>
    public class Rage
    {
        /// <summary>
        /// Gets or sets a value indicating whether tutorials will be forced to not enrage Scp096.
        /// </summary>
        [Description("Whether tutorials will be forced to not enrage Scp096.")]
        public bool TutorialsCanEnrage { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether damage will enrage Scp096.
        /// </summary>
        [Description("Whether damage will enrage Scp096.")]
        public bool EnrageOnDamage { get; set; } = true;

        /// <summary>
        /// Gets or sets how much rage time Scp096 will have by default.
        /// </summary>
        [Description("How much rage time Scp096 will have by default.")]
        public float DefaultRageTime { get; set; } = 7f;

        /// <summary>
        /// Gets or sets how much rage time Scp096 will gain per target gained.
        /// </summary>
        [Description("How much rage time Scp096 will gain per target gained.")]
        public float RageTimePerTarget { get; set; } = 3f;

        /// <summary>
        /// Gets or sets the maximum amount of time, in seconds, that can be added to Scp096's enraged state.
        /// </summary>
        [Description("The maximum amount of time Scp096 can be enraged.")]
        public float MaximumAddedRageTime { get; set; } = 15f;

        /// <summary>
        /// Gets or sets a value indicating whether the flashed and deafened effects received from flashbangs will be severely reduced while a Scp096 is enraged.
        /// </summary>
        [Description("Whether the flashed and deafened effects received from flashbangs will be severely reduced while a Scp096 is enraged.")]
        public bool DisableFlashing { get; set; } = true;

        /// <summary>
        /// Gets or sets how many seconds of the charge timer will be reduced on kill.
        /// </summary>
        [Description("How many seconds of the charge timer that will be reduced on kill.")]
        public float ChargeTimeOnKill { get; set; } = 0f;
    }
}