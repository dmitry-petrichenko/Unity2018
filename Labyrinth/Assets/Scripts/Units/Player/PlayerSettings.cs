using Scripts.Settings;
using Scripts.Units.Settings;
using Units.OneUnit.StatesControllers.Base.Settings;

namespace Units.Player
{
    public class PlayerSettings : UnitSettings
    {
        public PlayerSettings(ISettings settings)
        {
            Initialize(settings.UnitsResourcesLocation + "RedMage.json");
        }
    }
}