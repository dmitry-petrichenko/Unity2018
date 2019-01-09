using System;
using System.Collections.Generic;
using Scripts.GameLoop;

namespace Scripts.ActionDistributor
{
    public class HeavyActionDistributor : IHeavyActionDistributor
    {
        private Queue<Action> _actionsQueue;
        private bool _wasAddedAtThisFrame;

        private readonly IGameLoopController _gameLoopController;
        
        public HeavyActionDistributor(IGameLoopController gameLoopController)
        {
            _gameLoopController = gameLoopController;
            _actionsQueue = new Queue<Action>();

            _gameLoopController.Updated += UpdateHandler;
        }

        private void UpdateHandler()
        {
            if (!_wasAddedAtThisFrame)
            {
                if (HasActions())
                {
                    _actionsQueue.Dequeue().Invoke();
                }
            }

            _wasAddedAtThisFrame = false;
        }

        public void InvokeDistributed(Action action)
        {
            if (_wasAddedAtThisFrame)
            {
                _actionsQueue.Enqueue(action);
            }
            else
            {
                action.Invoke();
                _wasAddedAtThisFrame = true;
            }
        }

        public bool HasActions()
        {
            return _actionsQueue.Count > 0;
        }
    }
}