using Scripts.Units.Events;
using Scripts.Units.StateInfo;
using Scripts.Units.StateInfo.LivingStates;
using Units;
using Units.OneUnit;

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
            ILivingStateControllerExternal livingStateControllerExternal,
            IUnitEvents unitEvents,
            IUnitStateController stateInfo
            ) : base(unitsTable, unitEvents, livingStateControllerExternal, stateInfo)
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