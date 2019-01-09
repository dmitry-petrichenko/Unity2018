using System.Collections.Generic;
using Autofac;
using Autofac.Builder;
using Autofac.Core;

namespace Scripts.AutofacExtensions
{
    public class ContextRegistrator
    {
        private IComponentContext _componentContext;
        List<IBuilderHolder> _builderHolders;
        
        public ContextRegistrator(IComponentContext componentContext)
        {
            _builderHolders = new List<IBuilderHolder>();
            _componentContext = componentContext;
        }

        public IRegistrationBuilder<TImplementer, ConcreteReflectionActivatorData, SingleRegistrationStyle> RegisterType<TImplementer>()
        {
            var registrationBuilder = RegistrationBuilder.ForType<TImplementer>();
            _builderHolders.Add(new BuilderHolder<TImplementer>(registrationBuilder));

            return registrationBuilder;
        }

        public void CreateRegistrations()
        {
            foreach (var builderHolder in _builderHolders)
            {
                var registration = builderHolder.CreateRegistration();
                _componentContext.ComponentRegistry.Register(registration);
            }
        }

        public class BuilderHolder<T> : IBuilderHolder
        {
            private IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> _builder;
            public BuilderHolder(IRegistrationBuilder<T, ConcreteReflectionActivatorData, SingleRegistrationStyle> builder)
            {
                _builder = builder;
            }

            public IComponentRegistration CreateRegistration()
            {
                return _builder.CreateRegistration();
            }
        }
        
        public interface IBuilderHolder
        {
            IComponentRegistration CreateRegistration();
        }
 
    }
}