using System;
using Autofac;
using Autofac.Builder;
using Tree;

namespace Scripts.Extensions2
{
    public static class AutofacExtensions
    {
       public static IRegistrationBuilder<TImplementer, SimpleActivatorData, SingleRegistrationStyle> CreateScopeForType<TImplementer>(
           this ContainerBuilder builder, Action<ContainerBuilder> configurationAction = null)
           where TImplementer : MyDisposable
       {
           var registrationBuilder = builder.Register(context =>
           {
               IScopeTree rootsScopeTree = context.Resolve<IScopeTree>();
               var scopeTree = rootsScopeTree.CreateChild(configurationAction);
               scopeTree.Scope.ComponentRegistry.Register(RegistrationBuilder.ForType<TImplementer>().CreateRegistration());

               var disposable = scopeTree.Scope.Resolve<TImplementer>() as MyDisposable;
               disposable.Disposed += () => scopeTree.Dispose();
               return (TImplementer)disposable;
           });
           return registrationBuilder;
       }
    }
}
