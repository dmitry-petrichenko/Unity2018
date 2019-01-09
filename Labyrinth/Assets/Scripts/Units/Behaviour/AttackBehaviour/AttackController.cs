using System.Collections.Generic;
using Scripts.Units.Behaviour.UnitActions;
using Scripts.Units.Rotation;

namespace Scripts.Units
{
    public class AttackController
    {
        private IOneUnitController _unitController;
        private UnitBehaviourGenerator _unitBehaviourGenerator;
        private IOneUnitRotationController _oneUnitRotationController;
        private AttackAction.Factory _actionFactory;
        private OvertakeOccupatedPositionController _overtakeOccupatedPositionController;

        public AttackController(
            UnitBehaviourGenerator unitBehaviourGenerator,
            AttackAction.Factory actionFactory,
            IOneUnitRotationController oneUnitRotationController,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController
            )
        {
            _actionFactory = actionFactory;
            _unitBehaviourGenerator = unitBehaviourGenerator;
            _oneUnitRotationController = oneUnitRotationController;
            _overtakeOccupatedPositionController = overtakeOccupatedPositionController;
        }

        public void Initialize(IOneUnitController unitController)
        {
            _unitController = unitController;
            _overtakeOccupatedPositionController.Initialize(_unitController);
        }

        public void Cancel()
        {
            _unitBehaviourGenerator.Stop();
        }
        
        public void Attack(IntVector2 position)
        {
            _oneUnitRotationController.Rotate(_unitController.Position, position);
            
            List<IUnitAction> actions = new List<IUnitAction>();
            
            AttackAction action;
            action = _actionFactory.Invoke();
            actions.Add(action);
            
            _unitBehaviourGenerator.Initialize(_unitController, actions);
            _unitBehaviourGenerator.Start();
        }
    }
}