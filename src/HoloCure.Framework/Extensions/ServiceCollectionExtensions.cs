#region License

// Copyright (c) Tomat. Licensed under the MIT License.
// See the LICENSE file in the repository root for full license text.

#endregion

using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace HoloCure.Framework.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMultiSingleton<TService, [DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)] TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            return services.AddSingleton(typeof(TService), typeof(TImplementation));
        }
    }
}
