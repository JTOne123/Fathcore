﻿using System;
using System.Linq;
using Fathcore.Infrastructure;
using Xunit;

namespace Fathcore.Tests
{
    public class EngineTest : TestBase
    {
        [Fact]
        public void Engine_IsCreated_ByDefault()
        {
            Singleton<IEngine>.Instance = null;

            Assert.Null(Singleton<IEngine>.Instance);
            Assert.Same(Engine.Current, Singleton<IEngine>.Instance);
            Assert.NotNull(Singleton<IEngine>.Instance);
        }

        [Fact]
        public void Engine_CreateAnInstance_ByItSelf()
        {
            var engine = Engine.Create();

            Assert.Same(engine, Singleton<IEngine>.Instance);
            Assert.Same(engine, BaseSingleton.AllSingletons[typeof(IEngine)]);
            Assert.Same(engine, Engine.Current);
            Assert.Same(Singleton<IEngine>.Instance, Engine.Current);
        }

        [Fact]
        public void Engine_ShouldReplace_AnInstance()
        {
            var typeFinder = new TypeFinder();
            var engine = Engine.Create();
            var newEngine = (IEngine)Activator.CreateInstance(typeFinder.FindClassesOfType<IEngine>().First());

            Assert.Same(engine, Engine.Current);
            Assert.NotSame(newEngine, Engine.Current);

            Engine.Replace(newEngine);

            Assert.Same(newEngine, Engine.Current);
        }
    }
}