using System.Collections.Generic;
using Scripts;
using Scripts.Units;
using Scripts.Units.Behaviour.UnitActions;

namespace Units.OneUnit
{
    public class AttackController
    {
        private IOneUnitController _unitController;
        private UnitBehaviourGenerator _unitBehaviourGenerator;
        private AttackAction.Factory _actionFactory;
        private OvertakeOccupatedPositionController _overtakeOccupatedPositionController;

        public AttackController(
            UnitBehaviourGenerator unitBehaviourGenerator,
            AttackAction.Factory actionFactory,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController
            )
        {
            _actionFactory = actionFactory;
            _unitBehaviourGenerator = unitBehaviourGenerator;
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
            List<IUnitAction> actions = new List<IUnitAction>();
            
            AttackAction action;
            action = _actionFactory.Invoke();
            action.Initialize(position);
            actions.Add(action);
            
            _unitBehaviourGenerator.Initialize(_unitController, actions);
            _unitBehaviourGenerator.Start();
        }
    }
}