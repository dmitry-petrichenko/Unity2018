using Scripts.Settings;

namespace Scripts.Units.Enemy
{
    public class EnemyController
    {
        public delegate EnemyController Factory();
        
        private IPeacefulBehaviour _peacefulBehaviour; 
        private IAgressiveBehaviour _agressiveBehaviour; 
        private ISettings _settings;
        private readonly IOneUnitController _oneUnitController;

        public EnemyController(
            IOneUnitController oneUnitController,
            IPeacefulBehaviour peacefulBehaviour,
            IAgressiveBehaviour agressiveBehaviour,
            ISettings settings
            )
        {
            _settings = settings;
            _peacefulBehaviour = peacefulBehaviour;
            _agressiveBehaviour = agressiveBehaviour;
            _oneUnitController = oneUnitController;
            
            Initialize();
        }
            
        void Initialize()
        {
            _oneUnitController.Initialize(_settings.UnitsResourcesLocation + "RedMage.json");
            
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