using ZScripts.Settings;
using ZScripts.Units.Settings;

namespace ZScripts.Units.Player
{
    public class PlayerController : OneUnitController, IPlayerController
    {
        private ISettings _settings;
        private IGameEvents _gameEvents;
        private IUnitSettings _unitSettings;

        protected override void UpdatePosition()
        {
            base.UpdatePosition();
            _gameEvents.TriggerPlayerPositionChanged(Position);
        }
        
        public PlayerController(IOneUnitServices services) : base(services)
        {
            _settings = services.Settings;
            _unitSettings = services.UnitSettings;
            _gameEvents = services.GameEvents;
            
            Initialize();
        }
        
        private void Initialize()
        {
            UnitSettings = _unitSettings;
            UnitSettings.Initialize(_settings.UnitsResourcesLocation + "RedMage.json");
            base.Initialize();

            MoveToComplete += Wait;
            SetOnPosition(new IntVector2(1, 1));
        }

        public void Attack(IntVector2 position)
        {
        }
    }
}