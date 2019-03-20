﻿using System;
using Fathcore.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fathcore.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for EncryptFactory.
    /// </summary>
    public static class EncryptFactoryServiceExtensions
    {
        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with default implementation type to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEncryptFactory(this IServiceCollection services)
        {
            services.AddEncryptFactory<EncryptFactory>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with an implementation type specified in TImplementation to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEncryptFactory<TImplementation>(this IServiceCollection services)
            where TImplementation : IEncryptFactory
        {
            services.AddEncryptFactory(typeof(TImplementation));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with an implementation type specified in implementationType to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddEncryptFactory(this IServiceCollection services, Type implementationType)
        {
            var serviceType = typeof(IEncryptFactory);
            if (!serviceType.IsAssignableFrom(implementationType) || !implementationType.IsClass)
                throw new InvalidOperationException($"The {nameof(implementationType)} must be concrete class and implements {nameof(IEncryptFactory)}.");

            services.AddSingleton(implementationType);
            services.AddSingleton(serviceType, provider => provider.GetRequiredService(implementationType));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with default implementation type to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddEncryptFactory(this IServiceCollection services)
        {
            services.TryAddEncryptFactory<IEncryptFactory>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with an implementation type specified in TImplementation to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddEncryptFactory<TImplementation>(this IServiceCollection services)
            where TImplementation : class, IEncryptFactory
        {
            services.TryAddEncryptFactory(typeof(TImplementation));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IEncryptFactory"/> service with an implementation type specified in implementationType to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddEncryptFactory(this IServiceCollection services, Type implementationType)
        {
            var serviceType = typeof(IEncryptFactory);
            if (!serviceType.IsAssignableFrom(implementationType) || !implementationType.IsClass)
                throw new InvalidOperationException($"The {nameof(implementationType)} must be concrete class and implements {nameof(IEncryptFactory)}.");

            services.TryAddSingleton(implementationType);
            services.TryAddSingleton(serviceType, provider => provider.GetRequiredService(implementationType));

            return services;
        }
    }
}
