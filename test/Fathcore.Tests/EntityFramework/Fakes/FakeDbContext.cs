﻿using System;
using System.Collections.Generic;
using Fathcore.EntityFramework.Builders;
using Microsoft.EntityFrameworkCore;

namespace Fathcore.EntityFramework.Fakes
{
    internal class FakeDbContext : BaseDbContext
    {
        public FakeDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var types = new List<Type>()
            {
                typeof(AuthorEntityMapping),
                typeof(BookEntityMapping),
                typeof(TitleEntityMapping),
                typeof(StringQueryTypeMapping)
            };

            foreach (var type in types)
            {
                var configuration = (IMappingConfiguration)Activator.CreateInstance(type);
                configuration.ApplyConfiguration(modelBuilder);
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
