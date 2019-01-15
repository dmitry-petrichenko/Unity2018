using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;
using Scripts.Units.Rotation;
using Scripts.Units.Settings;
using Scripts.Units.StateInfo;
using Units;

namespace Scripts.Units
{
    public class OneUnitController : IOneUnitController, IDisposable
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileComplete;

        private MoveController _moveController;
        private IUnitsTable _unitsTable;
        private readonly IEventDispatcher _eventDispatcher;
        private IUnitStateInfo _unitStateInfo;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IEventDispatcher eventDispatcher,
            IUnitStateInfo unitStateInfo
            )
        {
            _unitStateInfo = unitStateInfo;
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _eventDispatcher = eventDispatcher;
            
            SubscribeOnEvents();
            _moveController.Initialize(this);
            _unitsTable.AddUnit(this);
        }

        public IUnitStateInfo UnitStateInfo => _unitStateInfo;
        public IntVector2 Position => _moveController.Position;

        public void SetOnPosition(IntVector2 position) => _moveController.SetOnPosition(position);
        
        public void MoveTo(IntVector2 position) => _moveController.MoveTo(position);

        public void Wait() => _moveController.Wait();
        
        public void Wait(IntVector2 position) => _moveController.Wait(position);

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_PATH_COMPLETE, MovePathCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_START, MoveTileStartHandler);
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_COMPLETE, MoveTileCompleteHandler);
        }
        
        private void UnsubscribeFromEvents()
        {
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_PATH_COMPLETE, new Action(MovePathCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_START, new Action(MoveTileStartHandler));
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_COMPLETE, new Action(MoveTileCompleteHandler));
        }

        private void MoveTileStartHandler() => UpdatePosition();
        
        private void MoveTileCompleteHandler() => MoveTileComplete?.Invoke();

        private void MovePathCompleteHandler() => MovePathComplete?.Invoke();
        
        public void UpdatePosition() => PositionChanged?.Invoke(Position);
    }
}