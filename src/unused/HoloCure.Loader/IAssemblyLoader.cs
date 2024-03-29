﻿using System.Collections.Generic;
using HoloCure.Registry;

namespace HoloCure.Loader
{
    /// <summary>
    ///     Handles resolving and loading mod assemblies.
    /// </summary>
    public interface IAssemblyLoader
    {
        /// <summary>
        ///     Adds an assembly prober to this loader.
        /// </summary>
        /// <param name="prober">The prober to add.</param>
        void AddProber(IAssemblyProber prober);

        IEnumerable<IModMetadata> ResolveMods();

        IEnumerable<IModMetadata> OrganizeMods(IAssemblyOrganizer organizer);

        void LoadMods();
    }
}