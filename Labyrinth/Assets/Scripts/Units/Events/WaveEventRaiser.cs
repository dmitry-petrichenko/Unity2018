using System;
using System.Collections.Generic;
using Units.OneUnit;
using Units.OneUnit.StatesControllers;
using Units.OneUnit.StatesControllers.Base;

namespace Scripts.Units.Events
{
    public class WaveEventRaiser : Disposable, IWaveEventRiser
    {
        private Dictionary<IPositional, Action> _subscribers;
        
        private readonly IBaseActionController _baseActionController;

        public WaveEventRaiser(IBaseActionController baseActionController)
        {
            _baseActionController = baseActionController;
            _baseActionController.MoveTileStart += MoveTileStartHandler;
            
            _subscribers = new Dictionary<IPositional, Action>();
        }

        private void MoveTileStartHandler()
        {
            List<IntVector2> adjacentPoints;
            for (int i = 1; i < 5; i++)
            {
                adjacentPoints = _baseActionController.Position.GetAdjacentPoints(null, i);
                foreach (var subscriber in _subscribers)
                {
                    foreach (var point in adjacentPoints)
                    {
                        if (subscriber.Key.Position.Equals(point))
                        {
                            subscriber.Value.Invoke();
                        }
                    }  
                }
            }
        }

        public void AddPositionChangedHandler(Action handler, IPositional subscriber)
        {
            _subscribers.Add(subscriber, handler);
        }

        public void RemovePositionChangedHandler(Action handler, IPositional subscriber)
        {
            if (_subscribers.ContainsKey(subscriber))
                _subscribers.Remove(subscriber);
        }

        protected override void DisposeInternal()
        {
            _baseActionController.MoveTileStart -= MoveTileStartHandler;
            _subscribers.Clear();
            base.DisposeInternal();
        }
    }
}