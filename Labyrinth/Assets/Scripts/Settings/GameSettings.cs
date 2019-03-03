using UnityEngine;

namespace Scripts.Settings
{
    public class GameSettings : ISettings
    {
        
        public int MapSectionSize { get; private set; }
        public int ActiveAreaSize { get; protected set; }
        public IntVector2 InitializePosition { get; private set; }
        public string MapsResourcesLocation { get; private set; }
        public string UnitsResourcesLocation { get; private set; }

        private const string TEST_MAP_PATH = "TestMap_01_OneBrick/";

        public GameSettings()
        {
            Initialize();
        }

        public void Initialize()
        {
            MapSectionSize = 2;
            ActiveAreaSize = 14;
            InitializePosition = new IntVector2(0, 0);
            MapsResourcesLocation = Application.dataPath + "/Resources/Maps/" + TEST_MAP_PATH;
            UnitsResourcesLocation = Application.dataPath + "/Resources/Units/Settings/";
        }
        
    }
}