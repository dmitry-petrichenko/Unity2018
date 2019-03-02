namespace Scripts.Units.StateInfo.BaseState
{
    public interface IState
    {
        void RaiseNoWayToDestination(IntVector2 position);
        void RaiseNextTileOccupied(IntVector2 position);
        void RaiseMovePathComplete();
        
        void SetAttackState();
        void SetPlacidState();
    }
}