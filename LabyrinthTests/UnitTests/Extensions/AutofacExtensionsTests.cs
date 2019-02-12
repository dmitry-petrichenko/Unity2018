using System;
using System.Collections.Generic;
using Autofac;
using Scripts;
using Scripts.Extensions2;
using Telerik.JustMock;
using Tree;
using Xunit;

namespace UnitTests.Extensions
{
    public class AutofacExtensionsTests
    {   
        private List<string> _createdTypes;
        private List<string> _disposedTypes;
        
        public AutofacExtensionsTests()
        {
        }

        [Fact]
        public void ShouldCorrectDisposeScopeObjects()
        {
            // Arrange
            _createdTypes = new List<string>();
            _disposedTypes = new List<string>();

            TypeEventDispatcher.TypeCreationEvent += s => _createdTypes.Add(s);
            TypeEventDispatcher.TypeDisposeEvent += s => _disposedTypes.Add(s);

            var builder = new ContainerBuilder();
            builder.RegisterType<ScopeTree>().As<IScopeTree>();
            
            builder.CreateScopeForType<TestDependency1A>(ConfigurationAction);
            void ConfigurationAction(ContainerBuilder containerBuilder)
            {
                containerBuilder.RegisterType<TestDependency2A>();
            }
            var container = builder.Build();

            // Act
            var testDependency1A = container.Resolve<TestDependency1A>();
            testDependency1A.Dispose();

            // Assert 
            Assert.Equal(3, _createdTypes.Count);
            Assert.Equal(3, _disposedTypes.Count);
        }
    }
    
    public class TestDependency1A : TestDisposable
    {
        public TestDependency1A(TestDependency2A testDependency2A, TestDependency2A testDependency2A2)
        {
        }
    }
    
    public class TestDependency2A : TestDisposable
    {
    }
    
    public class TestDisposable : MyDisposable
    {
        public TestDisposable()
        {
            TypeEventDispatcher.SendTypeCreationEvent(GetType().FullName);
        }

        protected override void DisposeInternal()
        {
            TypeEventDispatcher.SendTypeDisposeEvent(GetType().FullName);
        }
    }

    public static class TypeEventDispatcher
    {
        public static event Action<string> TypeCreationEvent; 
        public static event Action<string> TypeDisposeEvent; 
        
        public static void SendTypeCreationEvent(string value)
        {
            TypeCreationEvent?.Invoke(value);
        }
        
        public static void SendTypeDisposeEvent(string value)
        {
            TypeDisposeEvent?.Invoke(value);
        }
    }
}