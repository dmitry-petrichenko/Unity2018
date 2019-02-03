using System;
using ID5D6AAC.Common.EventDispatcher;
using Scripts;
using Scripts.Units.Events;
using Units.OneUnit.Base.GameObject.Animation;
using Units.OneUnit.Base.GameObject.Health;
using Units.OneUnit.Base.GameObject.Motion;
using Units.OneUnit.Base.GameObject.Rotation;

namespace Units.OneUnit.Base.GameObject
{
    public class UnitGameObjectController : IUnitGameObjectController
    {
        private readonly IOneUnitRotationController _rotationController;
        private readonly IOneUnitAnimationController _animationController;
        private readonly IOneUnitMotionController _motionController;
        private readonly IOneUnitHealthController _oneUnitHealthController;
        private readonly IEventDispatcher _eventDispatcher;
        
        public IntVector2 Position => _motionController.Position;
        
        public UnitGameObjectController(            
            IOneUnitRotationController oneUnitRotationController,
            IOneUnitAnimationController oneUnitAnimationController,
            IOneUnitMotionController oneUnitMotionController,
            IOneUnitHealthController oneUnitHealthController,
            IEventDispatcher eventDispatcher
            )
        {
            _rotationController = oneUnitRotationController;
            _animationController = oneUnitAnimationController;
            _motionController = oneUnitMotionController;
            _oneUnitHealthController = oneUnitHealthController;
            _eventDispatcher = eventDispatcher;
            
            _motionController.MoveStart += StartMoveHandler;
            _motionController.MoveComplete += MoveStepCompleteHandler;
            _animationController.AttackComplete += AttackCompleteHandler;
        }

        public void MoveTo(IntVector2 position)
        {
            _rotationController.Rotate(_motionController.Position, position);
            _animationController.PlayWalkAnimation();
            _motionController.MoveToPosition(position);
        }

        public void Wait()
        {
            _animationController.PlayIdleAnimation();
        }
        
        public void Wait(IntVector2 position)
        {
            _rotationController.Rotate(_motionController.Position, position);
            _animationController.PlayIdleAnimation();
        }
        
        public void Attack(IntVector2 position)
        {
            _rotationController.Rotate(_motionController.Position, position);
            _animationController.PlayAttackAnimation();
        }
        
        public void Die()
        {
            _animationController.PlayDieAnimation();
        }
        
        public void SetHealthBarValue(float value)
        {
            _oneUnitHealthController.SetHealthBarValue(value);
        }

        public void SetOnPosition(IntVector2 position)
        {
            _motionController.SetOnPosition(position);
        }

        public event Action MoveComplete
        {
            add => _motionController.MoveComplete += value;
            remove => _motionController.MoveComplete -= value;
        }

        public bool IsMoving => _motionController.IsMoving;

        private void StartMoveHandler()
        {
            _eventDispatcher.DispatchEvent(UnitEventsTypes.MOVE_TILE_START);
        }
        
        private void MoveStepCompleteHandler()
        {
            _eventDispatcher.DispatchEvent(UnitEventsTypes.MOVE_TILE_COMPLETE);
        }
        
        private void AttackCompleteHandler()
        {
            _eventDispatcher.DispatchEvent(UnitEventsTypes.ATTACK_COMPLETE);
        }
    }
}