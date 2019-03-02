using Units.OneUnit.Base;

namespace Scripts.Units.StateInfo.BaseState.States
{
    public class AttackState : IState
    {
        private readonly IBaseActionControllerInternal _baseActionController;
        private readonly IStateControllerMutator _stateController;
        
        public AttackState(
            IBaseActionControllerInternal baseActionController,
            IStateControllerMutator stateController)
        {
            _baseActionController = baseActionController;
            _stateController = stateController;
        }

        public void RaiseNoWayToDestination(IntVector2 position)
        {
            _baseActionController.RaiseNoWayToAttackDestination(position);
        }

        public void RaiseNextTileOccupied(IntVector2 position)
        {
            _baseActionController.RaiseNextTileOccupied(position);
        }

        public void RaiseMovePathComplete()
        {
            _baseActionController.RaiseMovePathComplete();
        }

        public void SetAttackState()
        {
        }

        public void SetPlacidState()
        {
            _stateController.SetState(_stateController.GetPlacidState());
        }
    }
}