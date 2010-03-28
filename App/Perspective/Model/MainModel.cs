//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Perspective.Hosting;
using System.ComponentModel.Composition.Hosting;

namespace Perspective.Model
{
    /// <summary>
    /// Manages the data that represent the extensions.
    /// </summary>
    public class MainModel
    {
        [ImportMany(AllowRecomposition = true)]
        public IEnumerable<Extension> Extensions { get; set; }

        /// <summary>
        /// Initializes a new instance of MainModel.
        /// </summary>
        public MainModel()
        {
            Compose();
        }

        private void Compose()
        {
            var catalog = new DirectoryCatalog("Extensions");
            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);
        }
    }
}
