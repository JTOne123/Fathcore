﻿using System;
using Fathcore.EntityFramework.AuditTrail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Fathcore.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for AuditHandler.
    /// </summary>
    public static class AuditHandlerServicesExtensions
    {
        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with default implementation type to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddAuditHandler(this IServiceCollection services)
        {
            services.AddAuditHandler<AuditHandler>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with an implementation type specified in TImplementation to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddAuditHandler<TImplementation>(this IServiceCollection services)
            where TImplementation : class, IAuditHandler
        {
            services.AddAuditHandler(typeof(TImplementation));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with an implementation type specified in implementationType to the specified <see cref="IServiceCollection"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddAuditHandler(this IServiceCollection services, Type implementationType)
        {
            var serviceType = typeof(IAuditHandler);
            if (!serviceType.IsAssignableFrom(implementationType) || !implementationType.IsClass)
                throw new InvalidOperationException($"The {nameof(implementationType)} must be concrete class and implements {nameof(IAuditHandler)}.");

            services.AddHttpContextAccessor();
            services.AddScoped(implementationType);
            services.AddScoped(serviceType, provider => provider.GetRequiredService(implementationType));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with default implementation type to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddAuditHandler(this IServiceCollection services)
        {
            services.TryAddAuditHandler<AuditHandler>();

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with an implementation type specified in TImplementation to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <typeparam name="TImplementation">The type of the implementation to use.</typeparam>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddAuditHandler<TImplementation>(this IServiceCollection services)
            where TImplementation : class, IAuditHandler
        {
            services.TryAddAuditHandler(typeof(TImplementation));

            return services;
        }

        /// <summary>
        /// Adds an <see cref="IAuditHandler"/> service with an implementation type specified in implementationType to the specified <see cref="IServiceCollection"/> if the service type hasn't already been registered.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the service to.</param>
        /// <param name="implementationType">The implementation type of the service.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection TryAddAuditHandler(this IServiceCollection services, Type implementationType)
        {
            var serviceType = typeof(IAuditHandler);
            if (!serviceType.IsAssignableFrom(implementationType) || !implementationType.IsClass)
                throw new InvalidOperationException($"The {nameof(implementationType)} must be concrete class and implements {nameof(IAuditHandler)}.");

            services.AddHttpContextAccessor();
            services.TryAddScoped(implementationType);
            services.TryAddScoped(serviceType, provider => provider.GetRequiredService(implementationType));

            return services;
        }
    }
}