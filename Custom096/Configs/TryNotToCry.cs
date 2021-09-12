// -----------------------------------------------------------------------
// <copyright file="TryNotToCry.cs" company="Build">
// Copyright (c) Build. All rights reserved.
// Licensed under the CC BY-SA 3.0 license.
// </copyright>
// -----------------------------------------------------------------------

namespace Custom096.Configs
{
    using System.ComponentModel;

    /// <summary>
    /// Settings related to Scp096's "Try Not To Cry" state.
    /// </summary>
    public class TryNotToCry
    {
        /// <summary>
        /// Gets or sets a value indicating whether Scp096 can use TNTC on any surface.
        /// </summary>
        [Description("Whether Scp096 can use TNTC on any surface.")]
        public bool AnySurface { get; set; } = false;

        /// <summary>
        /// Gets or sets the maximum distance that Scp096 can TNTC against a surface.
        /// </summary>
        [Description("The maximum distance that Scp096 can TNTC against a surface. Requires AnySurface to be enabled.")]
        public float MaximumDistance { get; set; } = 1f;
    }
}