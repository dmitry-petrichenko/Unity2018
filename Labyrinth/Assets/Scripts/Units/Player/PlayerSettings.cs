using Scripts.Settings;
using Scripts.Units.Settings;

namespace Scripts.Units.Player
{
    public class PlayerSettings : UnitSettings
    {
        public PlayerSettings(ISettings settings)
        {
            Initialize(settings.UnitsResourcesLocation + "RedMage.json");
        }
    }
}