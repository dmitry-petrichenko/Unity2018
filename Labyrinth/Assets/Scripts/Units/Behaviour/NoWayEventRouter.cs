using System;
using Scripts.Units.StateInfo;

namespace Scripts.Units
{
    public class NoWayEventRouter : INoWayEventRouter
    {
        private readonly ISubMoveController _subMoveController;
        private readonly IUnitStateInfo _unitStateInfo;
        
        public NoWayEventRouter(
            ISubMoveController subMoveController,
            IUnitStateInfo unitStateInfo)
        {
            _subMoveController = subMoveController;
            _unitStateInfo = unitStateInfo;
            
            Initialize();
        }

        private void Initialize()
        {
            _subMoveController.NoWayToPointHandler += NoWayToPointEventHandler;
        }

        private void NoWayToPointEventHandler(IntVector2 position)
        {
            if (_unitStateInfo.IsAttacking)
            {
                if (NoWayToAttackPointHandler != null)
                {
                    NoWayToAttackPointHandler(position);
                }
            }
            else
            {
                if (NoWayToPointHandler != null)
                {
                    NoWayToPointHandler(position);
                }
            }
        }

        public event Action<IntVector2> NoWayToAttackPointHandler;
        public event Action<IntVector2> NoWayToPointHandler;
    }
}