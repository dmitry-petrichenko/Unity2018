using Scripts.Settings;
using Units.OneUnit.StatesControllers.Base.Settings;

namespace Scripts.Units.Settings
{
    public class EnemySettings : UnitSettings
    {
        public EnemySettings(ISettings settings)
        {
            //Initialize(settings.UnitsResourcesLocation + "SpiderBlack01.json");
            Initialize(settings.UnitsResourcesLocation + "RedMage.json");
            TotalHealth = 6;
        }
    }
}