using System;

namespace Scripts.Units.UnitActions
{
    public class MoveToPositionAction : IUnitAction
    {
        public delegate MoveToPositionAction Factory();
        
        private IOneUnitController _oneUnitController;
        private IMovingRandomizer _movingRandomizer;
        
        public MoveToPositionAction(IMovingRandomizer movingRandomizer)
        {
            _movingRandomizer = movingRandomizer;
        }

        public void Start()
        {
            _oneUnitController.UnitEvents.MovePathComplete += MoveCompleteHandler;
            IntVector2 position = _movingRandomizer.GetRandomPoint(_oneUnitController.Position);
            _oneUnitController.MoveTo(position);
        }

        private void MoveCompleteHandler()
        {
            if (OnComplete != null)
            {
                OnComplete();
            }
        }

        public void Stop()
        {
            
        }

        public void Destroy()
        {
            
        }

        public void Initialize(IOneUnitController oneUnitController)
        {
            _oneUnitController = oneUnitController;
        }

        public event Action OnComplete;
    }
}

