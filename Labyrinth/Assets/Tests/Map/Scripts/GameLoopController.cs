using System;
using System.Collections;
using Scripts.GameLoop;
using UnityEngine;

namespace Tests.Map.Scripts
{
    public class GameLoopController : IGameLoopController
    {
        private MonoBehaviour _monoBehaviour;
        
        public GameLoopController(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        public event Action Updated;
        public void DelayStart(Action action, float time)
        {
            _monoBehaviour.StartCoroutine(WaitAndDoAction(action, time));
        }

        private GameInstaller _gameController;

        public void Initialize(GameInstaller gameController)
        {
            _gameController = gameController;
            _gameController.Updated += UpdateHandler;
        }

        private void UpdateHandler()
        {
            if (Updated != null)
                Updated();
        }
        
        IEnumerator WaitAndDoAction(Action action, float waitTime) {
            yield return new WaitForSeconds(waitTime);
            action.Invoke();
        }
    }
}