using System.Collections.Generic;
using Scripts;
using Scripts.Units;
using Scripts.Units.Behaviour.UnitActions;
using Units.OneUnit.Base;

namespace Units.OneUnit
{
    public class AttackController : IAttackController
    {
        private IOneUnitController _unitController;
        private UnitBehaviourGenerator _unitBehaviourGenerator;
        private AttackAction.Factory _actionFactory;
        private OvertakeOccupatedPositionController _overtakeOccupatedPositionController;
        private IBaseActionController _baseActionController;

        public AttackController(
            IBaseActionController baseActionController,
            UnitBehaviourGenerator unitBehaviourGenerator,
            AttackAction.Factory actionFactory,
            OvertakeOccupatedPositionController overtakeOccupatedPositionController
            )
        {
            _actionFactory = actionFactory;
            _unitBehaviourGenerator = unitBehaviourGenerator;
            _overtakeOccupatedPositionController = overtakeOccupatedPositionController;
            _baseActionController = baseActionController;
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

        public void TakeDamage(int value)
        {
            _baseActionController.TakeDamage(value);
        }
    }
}