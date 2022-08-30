#region License

// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

#endregion

using HoloCure.Framework.Services;
using HoloCure.NET.Desktop.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HoloCure.NET.Desktop
{
    public class HoloCureGameDesktop : HoloCureGame
    {
        protected override void ConfigureStartUpServices(IServiceCollection collection)
        {
            base.ConfigureStartUpServices(collection);

            collection.AddSingleton<IStartUpService, WindowConfigurationService>();
        }
    }
}
