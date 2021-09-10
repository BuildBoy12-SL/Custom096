// -----------------------------------------------------------------------
// <copyright file="Config.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096
{
    using System.ComponentModel;
    using Custom096.Configs;
    using Exiled.API.Interfaces;

    /// <inheritdoc cref="IConfig"/>
    public class Config : IConfig
    {
        /// <inheritdoc/>
        public bool IsEnabled { get; set; } = true;

        /// <summary>
        /// Gets or sets all related settings for Scp096's charging state.
        /// </summary>
        [Description("All related settings for Scp096's charging state.")]
        public Charge Charge { get; set; } = new Charge();

        /// <summary>
        /// Gets or sets all health related settings for Scp096.
        /// </summary>
        [Description("All health related settings for Scp096.")]
        public Health Health { get; set; } = new Health();

        /// <summary>
        /// Gets or sets all related settings for Scp096's enraged state.
        /// </summary>
        [Description("All related settings for Scp096's enraged state.")]
        public Rage Rage { get; set; } = new Rage();

        /// <summary>
        /// Gets or sets all related settings for Scp096's swinging state.
        /// </summary>
        [Description("All related settings for Scp096's swinging state.")]
        public Swing Swing { get; set; } = new Swing();

        /// <summary>
        /// Gets or sets all related settings for Scp096's TNTC state.
        /// </summary>
        [Description("All related settings for Scp096's TNTC state.")]
        public TryNotToCry TryNotToCry { get; set; } = new TryNotToCry();
    }
}