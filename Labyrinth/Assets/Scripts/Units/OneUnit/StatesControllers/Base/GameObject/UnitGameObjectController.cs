using System;
using Scripts;
using Units.OneUnit.StatesControllers.Base.GameObject.Animation;
using Units.OneUnit.StatesControllers.Base.GameObject.Health;
using Units.OneUnit.StatesControllers.Base.GameObject.Motion;
using Units.OneUnit.StatesControllers.Base.GameObject.Rotation;

namespace Units.OneUnit.StatesControllers.Base.GameObject
{
    public class UnitGameObjectController : Disposable, IUnitGameObjectController
    {
        private readonly IOneUnitRotationController _rotationController;
        private readonly IOneUnitAnimationController _animationController;
        private readonly IOneUnitMotionController _motionController;
        private readonly IOneUnitHealthController _oneUnitHealthController;

        public IntVector2 Position => _motionController.Position;

        public UnitGameObjectController(
            IOneUnitRotationController oneUnitRotationController,
            IOneUnitAnimationController oneUnitAnimationController,
            IOneUnitMotionController oneUnitMotionController,
            IOneUnitHealthController oneUnitHealthController
        )
        {
            _rotationController = oneUnitRotationController;
            _animationController = oneUnitAnimationController;
            _motionController = oneUnitMotionController;
            _oneUnitHealthController = oneUnitHealthController;
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
            var adjacentPoints = _motionController.Position.GetAdjacentPoints();
            
            if (!adjacentPoints.Contains(position))
                throw new Exception("Attacked possition isn't in unit range");
            
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

        public void SetHealthBarVisible(bool value)
        {
            _oneUnitHealthController.SetHealthBarVisible(value);
        }

        public event Action MoveTileComplete
        {
            add =>  _motionController.MoveComplete += value;
            remove =>  _motionController.MoveComplete -= value;
        }
        
        public event Action MoveTileStart
        {
            add =>  _motionController.MoveStart += value;
            remove =>  _motionController.MoveStart -= value;
        }

        public event Action AttackComplete
        {
            add => _animationController.AttackComplete += value;
            remove => _animationController.AttackComplete -= value;
        }
        
        public event Action DieComplete
        {
            add => _animationController.DieComplete += value;
            remove => _animationController.DieComplete -= value;
        }
         
        public void SetOnPosition(IntVector2 position)
        {
            _motionController.SetOnPosition(position);
        }

        public bool IsMoving => _motionController.IsMoving;
    }
}