using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;

namespace Scripts.Units
{
    public class OneUnitController : OneUnitServicesContainer, IOneUnitController, IDisposable
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MovePathComplete;
        public event Action MoveTileStart;
        public event Action MoveTileComplete;

        private MoveController _moveController;
        private IUnitsTable _unitsTable;
        private readonly IEventDispatcher _eventDispatcher;
        private readonly IBaseMovingController _baseMovingController;

        public OneUnitController(IOneUnitServices services) : base(services)
        {
            _unitsTable = services.UnitsTable;
            _moveController = services.MoveController;
            _eventDispatcher = services.EventDispatcher;
            _baseMovingController = services.BaseMovingController;
        }

        protected void Initialize()
        {
            base.Initialize();
            SubscribeOnEvents();
           
            // Initialize behaviour
            _moveController.Initialize(this);
            _unitsTable.AddUnit(this);
        }
        
        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_PATH_COMPLETE, MoveCompleteHandler);
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_START, MoveTileStartHandler);
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_COMPLETE, MoveTileCompleteHandler);
        }
        
        private void UnsubscribeFromEvents()
        {
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_PATH_COMPLETE, new Action(MoveCompleteHandler));
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_START, new Action(MoveTileStartHandler));
            _eventDispatcher.RemoveEventListener(UnitEvents.MOVE_TILE_COMPLETE, new Action(MoveTileCompleteHandler));
        }

        private void MoveTileCompleteHandler()
        {
            MoveTileComplete?.Invoke();
        }

        private void MoveTileStartHandler()
        {
            UpdatePosition();
        }

        protected virtual void UpdatePosition()
        {
            if (PositionChanged != null)
                PositionChanged(Position);
        }

        private void MoveCompleteHandler()
        {
            MovePathComplete?.Invoke();
        }

        public void SetOnPosition(IntVector2 position)
        {
            _moveController.SetOnPosition(position);
        }

        public IntVector2 Position => MotionController.Position;

        public void MoveTo(IntVector2 position)
        {
            _moveController.MoveTo(position);
        }

        public void Wait()
        {
            _baseMovingController.Wait();
        }
        
        public void Wait(IntVector2 position)
        {
            _baseMovingController.Wait(position);
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}