using System;
using System.Collections.Generic;
using Autofac;
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
        public void ShouldCorrectCreateDisposeScopeLevel0Objects()
        {
            // Arrange
            _createdTypes = new List<string>();
            _disposedTypes = new List<string>();

            TypeEventDispatcher.TypeCreationEvent += s => _createdTypes.Add(s);
            TypeEventDispatcher.TypeDisposeEvent += s => _disposedTypes.Add(s);

            var builder = CreateBuilder();
            
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
        
        [Theory]
        [InlineData("B", 4)]
        [InlineData("C", 3)]
        [InlineData("D", 2)]
        [InlineData("E", 1)]
        public void ShouldCorrectDisposeDeendencyHierarhy(string childInHierarhy, int disposedCount)
        {
            // Arrange
            _disposedTypes = new List<string>();
            var instancesMap = new Dictionary<string, BaseDependency2>();
            
            TypeEventDispatcher.TypeDisposeEvent += s => _disposedTypes.Add(s);

            var builder = CreateBuilder();
            builder.CreateScopeForType<TestDependency2B>(ConfigurationAction1).SingleInstance();
            
            void ConfigurationAction1(ContainerBuilder containerBuilder)
            {
                containerBuilder.CreateScopeForType<TestDependency2C>(ConfigurationAction2).SingleInstance();
            }
            
            void ConfigurationAction2(ContainerBuilder containerBuilder)
            {
                containerBuilder.CreateScopeForType<TestDependency2D>(ConfigurationAction3).SingleInstance();
            }
            
            void ConfigurationAction3(ContainerBuilder containerBuilder)
            {
                containerBuilder.CreateScopeForType<TestDependency2E>().SingleInstance();
            }
            
            var container = builder.Build();
            
            instancesMap.Add("B", container.Resolve<TestDependency2B>());
            instancesMap.Add("C", container.Resolve<TestDependency2B>().GetChild());
            instancesMap.Add("D", container.Resolve<TestDependency2B>().GetChild().GetChild());
            instancesMap.Add("E", container.Resolve<TestDependency2B>().GetChild().GetChild().GetChild());

            // Act
            instancesMap[childInHierarhy].Dispose();

            // Assert 
            Assert.Equal(disposedCount, _disposedTypes.Count);
        }

        private ContainerBuilder CreateBuilder()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ScopeTree>().As<IScopeTree>();
            return builder;
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
    
    public class TestDependency2B : BaseDependency2
    {
        public TestDependency2B(TestDependency2C testDependency2C) : base(testDependency2C)
        {
        }
    }
    
    public class TestDependency2C : BaseDependency2
    {
        public TestDependency2C(TestDependency2D testDependency2D) : base(testDependency2D)
        {
        }
    }
    
    public class TestDependency2D : BaseDependency2
    {
        public TestDependency2D(TestDependency2E testDependency2E) : base(testDependency2E)
        {
        }
    }
    
    public class TestDependency2E : BaseDependency2
    {
        public TestDependency2E() : base(null)
        {
        }
    }

    public class BaseDependency2 : TestDisposable
    {
        private BaseDependency2 _baseDependency2;
        public BaseDependency2(BaseDependency2 testDependency2B)
        {
            _baseDependency2 = testDependency2B;
        }

        public BaseDependency2 GetChild()
        {
            return _baseDependency2;
        }
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