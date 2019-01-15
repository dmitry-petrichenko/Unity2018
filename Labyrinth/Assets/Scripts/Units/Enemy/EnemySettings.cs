using Scripts.Settings;

namespace Scripts.Units.Settings
{
    public class EnemySettings : UnitSettings
    {
        public EnemySettings(ISettings settings)
        {
            Initialize(settings.UnitsResourcesLocation + "SpiderBlack01.json");
        }
    }
}