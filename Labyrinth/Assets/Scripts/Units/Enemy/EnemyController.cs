using Scripts.Settings;
using Scripts.Units.Settings;

namespace Scripts.Units.Enemy
{
    public class EnemyController : OneUnitController
    {
        public delegate EnemyController Factory();
        
        private IPeacefulBehaviour _peacefulBehaviour; 
        private IAgressiveBehaviour _agressiveBehaviour; 
        private IUnitSettings _unitSettings; 
        private ISettings _settings;

        public EnemyController(IOneUnitServices services) : base(services)
        {
            _settings = services.Settings;
            _unitSettings = services.UnitSettings;
            _peacefulBehaviour = services.PeacefulBehaviour;
            _agressiveBehaviour = services.AgressiveBehaviour;
            
            Initialize();
        }
            
        void Initialize()
        {
            UnitSettings = _unitSettings;
            UnitSettings.Initialize(_settings.UnitsResourcesLocation + "SpiderBlack01.json");
            base.Initialize();
            
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