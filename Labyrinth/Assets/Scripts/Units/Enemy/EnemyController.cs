using Units.OneUnit;

namespace Scripts.Units.Enemy
{
    public class EnemyController
    {
        public delegate EnemyController Factory();
        
        private IPeacefulBehaviour _peacefulBehaviour; 
        private IAgressiveBehaviour _agressiveBehaviour; 
        private readonly IOneUnitController _oneUnitController;

        public EnemyController(
            IOneUnitController oneUnitController,
            IPeacefulBehaviour peacefulBehaviour,
            IAgressiveBehaviour agressiveBehaviour
            )
        {
            _peacefulBehaviour = peacefulBehaviour;
            _agressiveBehaviour = agressiveBehaviour;
            _oneUnitController = oneUnitController;
            
            Initialize();
        }
            
        void Initialize()
        {
            _peacefulBehaviour.Initialize(_oneUnitController);
            _agressiveBehaviour.Initialize(_oneUnitController);
        }

        public void SetOnPosition(IntVector2 position)
        {
            _oneUnitController.SetOnPosition(position);
        }

        public void Animate()
        {
            _peacefulBehaviour.Initialize(_oneUnitController);
            _peacefulBehaviour.Start();
        }

        public void Attack(IOneUnitController oneUnitController)
        {
            _agressiveBehaviour.Start(oneUnitController);
        }
    }
}