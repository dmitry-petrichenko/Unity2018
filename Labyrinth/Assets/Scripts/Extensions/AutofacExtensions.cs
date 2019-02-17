using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Builder;

namespace Scripts.Extensions
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
               disposable.Disposed += ()=> scopeTree.Dispose();
               return (TImplementer)disposable;
           });
           return registrationBuilder;
       }
    }

    public class ScopeTree : IScopeTree
    {
        private List<ScopeTree> _children = new List<ScopeTree>();
        private ILifetimeScope _scope;
        private bool _isDisposed;

        public ScopeTree(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public IScopeTree CreateChild(Action<ContainerBuilder> configurationAction = null)
        {
            var scope = configurationAction != null
                ? _scope.BeginLifetimeScope(configurationAction)
                : _scope.BeginLifetimeScope();
            var child = new ScopeTree(scope);
            _children.Add(child);

            var updater = new ContainerBuilder();
            updater.RegisterInstance(child).As<IScopeTree>();
            updater.Update(child.Scope.ComponentRegistry);

            return child;
        }

        public void Dispose()
        {
            if(_isDisposed)
                return;

            _children.ForEach(c=>c.Dispose());
            _children = null;
            _isDisposed = true;
            _scope.Dispose();
            _scope = null;
        }

        public ILifetimeScope Scope => _scope;
    }

    public interface IScopeTree
    {
        IScopeTree CreateChild(Action<ContainerBuilder> configurationAction = null);
        void Dispose();
        ILifetimeScope Scope { get; }
    }
}
