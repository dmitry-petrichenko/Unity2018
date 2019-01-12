using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts.Units.Events;

namespace Scripts.Units
{
    public class OneUnitController : OneUnitServicesContainer, IOneUnitController, IDisposable
    {
        public event Action<IntVector2> PositionChanged;
        public event Action MoveToComplete;
        public event Action MoveOneStepStart;
        public event Action MoveTileComplete;

        private MoveController _moveController;
        private IUnitsTable _unitsTable;
        private readonly IEventDispatcher _eventDispatcher;

        public OneUnitController(IOneUnitServices services) : base(services)
        {
            _unitsTable = services.UnitsTable;
            _moveController = services.MoveController;
            _eventDispatcher = services.EventDispatcher;
        }

        protected void Initialize()
        {
            
            base.Initialize();
            SubscribeOnEvents();
           
            // Initialize behaviour
            _moveController.Initialize(this);
            _moveController.MoveToComplete += MoveCompleteHandler;
            _unitsTable.AddUnit(this);
        }
        
        private void SubscribeOnEvents()
        {
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_START, MoveTileStartHandler);
            _eventDispatcher.AddEventListener(UnitEvents.MOVE_TILE_COMPLETE, MoveTileCompleteHandler);
        }
        
        private void UnsubscribeFromEvents()
        {
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
            if (MoveToComplete != null)
            {
                MoveToComplete();
            }
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
            AnimationController.PlayIdleAnimation();
        }
        
        public void Wait(IntVector2 position)
        {
            RotationController.Rotate(Position, position);
            AnimationController.PlayIdleAnimation();
        }

        public void Dispose()
        {
            UnsubscribeFromEvents();
        }
    }
}