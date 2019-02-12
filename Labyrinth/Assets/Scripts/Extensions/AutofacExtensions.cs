using System;
using Autofac;
using Autofac.Builder;

namespace Scripts.Extensions2
{
    public static class AutofacExtensions
    {
       public static IRegistrationBuilder<TImplementer, SimpleActivatorData, SingleRegistrationStyle> CreateScopeForType<TImplementer>(
               this ContainerBuilder builder, Action<ContainerBuilder> configurationAction)
       {
           var rb = builder.Register(context =>
           {
               ILifetimeScope scope = context.Resolve<ILifetimeScope>().BeginLifetimeScope(configurationAction);
               scope.ComponentRegistry.Register(RegistrationBuilder.ForType<TImplementer>().CreateRegistration());
               return scope.Resolve<TImplementer>();
           });
           return rb;
       }
    }
}