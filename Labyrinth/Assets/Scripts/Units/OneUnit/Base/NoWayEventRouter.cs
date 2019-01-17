using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Scripts.Units.StateInfo;

namespace Units.OneUnit.Base
{
    public class NoWayEventRouter : INoWayEventRouter, IDisposable
    {
        private readonly IUnitStateInfo _unitStateInfo;
        private readonly IEventDispatcher _eventDispatcher;
        
        public event Action<IntVector2> NoWayToAttackPointHandler;
        public event Action<IntVector2> NoWayToPointHandler;
        
        public NoWayEventRouter(
            IUnitStateInfo unitStateInfo,
            IEventDispatcher eventDispatcher)
        {
            _unitStateInfo = unitStateInfo;
            _eventDispatcher = eventDispatcher;
            
            Initialize();
        }

        private void Initialize()
        {
            _eventDispatcher.AddEventListener<IntVector2>(UnitEventsTypes.NO_WAY_TO_TILE, NoWayToPointEventHandler);
        }

        private void NoWayToPointEventHandler(IntVector2 position)
        {
            if (_unitStateInfo.IsAttacking)
            {
                NoWayToAttackPointHandler?.Invoke(position);
            }
            else
            {
                NoWayToPointHandler?.Invoke(position);
            }
        }
        
        public void Dispose()
        {
            _eventDispatcher.RemoveEventListener(UnitEventsTypes.NO_WAY_TO_TILE, new Action<IntVector2>(NoWayToPointEventHandler));
        }
    }
}