using System;
using System.Collections.Generic;
using Autofac;

namespace Tree
{
    public class ScopeTree : IScopeTree
    {
        public IScopeTree INSTANCE { get; private set; }

        private List<ScopeTree> _children = new List<ScopeTree>();
        private ILifetimeScope _scope;
        private bool _isDisposed;

        private string _test = "";

        public ScopeTree(ILifetimeScope scope, string test = "F")
        {
            _scope = scope;

            _test = test;
        }

        public IScopeTree CreateChild(Action<ContainerBuilder> configurationAction = null)
        {
            _configurationAction = configurationAction;
            Console.WriteLine("Create scope" + _test);
            _child = new ScopeTree(_scope.BeginLifetimeScope(_test, configurationAction1), _test + "_I");
            _children.Add(_child);


            var updater = new ContainerBuilder();
            updater.RegisterInstance(_child).As<IScopeTree>();
            updater.Update(_child.Scope.ComponentRegistry);

            return _child;
        }

        private ScopeTree _child;
        private Action<ContainerBuilder> _configurationAction;
        private void configurationAction1(ContainerBuilder builder)
        {
            _configurationAction?.Invoke(builder);
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _children.ForEach(c=>c.Dispose());
                _isDisposed = true;
                Console.WriteLine("------");
                _scope.Dispose();
                Console.WriteLine("Dispose scope" + _scope.Tag);
            }
        }

        public ILifetimeScope Scope => _scope;
    }

    public interface IScopeTree
    {
        IScopeTree CreateChild(Action<ContainerBuilder> configurationAction = null);
        void Dispose();
        ILifetimeScope Scope { get; }

        IScopeTree INSTANCE {  get; }
    }
}