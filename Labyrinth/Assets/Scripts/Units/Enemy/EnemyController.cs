using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units;
using Units.OneUnit;
using Units.OneUnit.Info;
using Units.OneUnit.State1E;

namespace Scripts.Units.Enemy
{
    public class EnemyController : OneUnitController
    {
        public delegate EnemyController Factory();
        
        private IPeacefulBehaviour _peacefulBehaviour; 
        private IAgressiveBehaviour _agressiveBehaviour; 

        public EnemyController(
            IPeacefulBehaviour peacefulBehaviour,
            IAgressiveBehaviour agressiveBehaviour,
            IUnitsTable unitsTable,
            IStateControllerExternal stateController,
            ILifeController lifeController,
            IUnitEvents unitEvents,
            IUnitInfoExternal unitInfoExternal
            ) : base(unitsTable, unitEvents, stateController, lifeController, unitInfoExternal)
        {
            _peacefulBehaviour = peacefulBehaviour;
            _agressiveBehaviour = agressiveBehaviour;
            
            Initialize();
        }
            
        void Initialize()
        {
            _peacefulBehaviour.Initialize(this);
            _agressiveBehaviour.Initialize(this);
        }

        public void Animate()
        {
            _peacefulBehaviour.Initialize(this);
            _peacefulBehaviour.Start();
        }

        public void Attack(IOneUnitController oneUnitController)
        {
            _agressiveBehaviour.Start(oneUnitController);
        }
    }
}