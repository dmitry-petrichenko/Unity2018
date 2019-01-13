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
        private readonly IBaseMovingController _baseMovingController;
        private readonly IOneUnitServicesContainer _oneUnitServicesContainer;

        public OneUnitController(
            IUnitsTable unitsTable,
            MoveController moveController,
            IEventDispatcher eventDispatcher,
            IBaseMovingController baseMovingController,
            IOneUnitServicesContainer oneUnitServicesContainer
            )
        {
            _unitsTable = unitsTable;
            _moveController = moveController;            
            _eventDispatcher = eventDispatcher;
            _baseMovingController = baseMovingController;
            _oneUnitServicesContainer = oneUnitServicesContainer;
        }

        public void Initialize(string settingsPath)
        {
            _oneUnitServicesContainer.Initialize(settingsPath);

            SubscribeOnEvents();
           
            // Initialize behaviour
            _moveController.Initialize(this);
            _unitsTable.AddUnit(this);
        }
        
        public IUnitStateInfo UnitStateInfo { get; }
        public IOneUnitMotionController MotionController => _oneUnitServicesContainer.MotionController;
        public IOneUnitAnimationController AnimationController => _oneUnitServicesContainer.AnimationController;
        public IOneUnitRotationController RotationController => _oneUnitServicesContainer.RotationController;
        public IUnitSettings UnitSettings => _oneUnitServicesContainer.UnitSettings;
        public IntVector2 Position => MotionController.Position;

        public void SetOnPosition(IntVector2 position) => _baseMovingController.SetOnPosition(position);
        
        public void MoveTo(IntVector2 position) => _baseMovingController.MoveTo(position);

        public void Wait() => _baseMovingController.Wait();
        
        public void Wait(IntVector2 position) => _baseMovingController.Wait(position);

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