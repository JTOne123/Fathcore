﻿using System;
using System.Linq;
using Fathcore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Fathcore.EntityFramework.AuditTrail.Extensions
{
    public class ServiceCollectionExtensionsTest
    {
        private IServiceCollection ServiceDescriptors { get; } = new ServiceCollection();

        [Fact]
        public void Should_Add_Audit_Handler_To_Service_By_Generic()
        {
            ServiceDescriptors.AddAuditHandler<AuditHandler>();

            var byServiceType = ServiceDescriptors.First(p => p.ServiceType == typeof(IAuditHandler));
            var byImplementedType = ServiceDescriptors.First(p => p.ServiceType == typeof(AuditHandler));

            Assert.Equal(ServiceLifetime.Scoped, byServiceType.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, byImplementedType.Lifetime);
        }

        [Fact]
        public void Should_Add_Audit_Handler_To_Service_By_Type()
        {
            ServiceDescriptors.AddAuditHandler(typeof(AuditHandler));

            var byServiceType = ServiceDescriptors.First(p => p.ServiceType == typeof(IAuditHandler));
            var byImplementedType = ServiceDescriptors.First(p => p.ServiceType == typeof(AuditHandler));

            Assert.Equal(ServiceLifetime.Scoped, byServiceType.Lifetime);
            Assert.Equal(ServiceLifetime.Scoped, byImplementedType.Lifetime);
        }

        [Fact]
        public void Should_Not_Add_Audit_Handler_To_Service_By_Type()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceDescriptors.AddAuditHandler(typeof(IAuditHandler)));
        }

        [Fact]
        public void Should_Not_Add_Audit_Handler_To_Service_By_Type_2()
        {
            Assert.Throws<InvalidOperationException>(() => ServiceDescriptors.AddAuditHandler(typeof(ServiceCollectionExtensionsTest)));
        }
    }
}
