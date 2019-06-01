using System;
using System.Collections.Generic;
using Units.OneUnit.StatesControllers;
using Units.OneUnit.StatesControllers.Base;

namespace Scripts.Units.Events
{
    public class WaveEventRaiser : Disposable, IWaveEventRiser
    {
        private Dictionary<IPositional, Action> _subscribers;
        
        private readonly IBaseActionController _baseActionController;

        private bool _activated;

        public WaveEventRaiser(IBaseActionController baseActionController)
        {
            _baseActionController = baseActionController;
            _subscribers = new Dictionary<IPositional, Action>();
            UpdateActivation();
        }

        private void EventStartHandler()
        {
            List<IntVector2> adjacentPoints;
            List<Action> handlersToInvoke = new List<Action>();
            
            for (int i = 1; i < 5; i++)
            {
                adjacentPoints = _baseActionController.Position.GetAdjacentPoints(null, i);
                foreach (var subscriber in _subscribers)
                {
                    foreach (var point in adjacentPoints)
                    {
                        if (subscriber.Key.Position.Equals(point))
                        {
                            handlersToInvoke.Add(subscriber.Value);
                        }
                    }  
                }
            }

            foreach (var hadler in handlersToInvoke)
            {
                hadler.Invoke();
            }
            handlersToInvoke.Clear();
        }

        public void AddStateChangedHandler(Action handler, IPositional subscriber)
        {
            _subscribers.Add(subscriber, handler);
            UpdateActivation();
        }

        public void RemovePositionChangedHandler(Action handler, IPositional subscriber)
        {
            if (_subscribers.ContainsKey(subscriber))
                _subscribers.Remove(subscriber);
            
            UpdateActivation();
        }
        
        private void UpdateActivation()
        {
            if (_subscribers.Count > 0)
                Activate();
            else
                Deactivate();
        }

        private void Activate()
        {
            if (_activated)
                return;
            
            _baseActionController.MoveTileStart += EventStartHandler;
            _baseActionController.DieComplete += EventStartHandler;
            _activated = true;
        }
        
        private void Deactivate()
        {
            _baseActionController.MoveTileStart -= EventStartHandler;
            _baseActionController.DieComplete -= EventStartHandler;
            _activated = false;
        }
        
        protected override void DisposeInternal()
        {
            Deactivate();
            _subscribers.Clear();
            _subscribers = null;
            base.DisposeInternal();
        }
    }
}