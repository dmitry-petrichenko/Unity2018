using Scripts.Settings;
using Scripts.Units.Settings;

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