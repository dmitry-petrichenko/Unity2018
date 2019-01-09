using System;

namespace Scripts.Map.Info
{
    public partial class MapSectorController
    {     

        private void UpdateAdjacents(IntVector2 position)
        {
            IntVector2 progression = GetPositionVisibleProgression(position, _currentSector);
            AddNewSectors(progression);
        }

        private void AddNewSectors(IntVector2 progression)
        {
            if (progression.x == 0 && progression.y == 0)
            {
                return;
            }
            else if (Math.Abs(progression.x) > 0 && Math.Abs(progression.y) > 0)
            {
                AddNewSector(progression);
                AddNewSectorByDimention(progression, true);
                AddNewSectorByDimention(progression, false);
            }
            else
            {
                AddNewSector(progression);
            }
        }

        private void AddNewSector(IntVector2 progression)
        {
            IntVector2 index = new IntVector2(_currentSector.index.x + progression.x, _currentSector.index.y + progression.y);
            _sectorLifecycleController.AddActiveSector(index);
        }

        private void AddNewSectorByDimention(IntVector2 progression, bool xDimention)
        {
            if (xDimention)
            {
                AddNewSector(new IntVector2(progression.x, 0));
            }
            else
            {
                AddNewSector(new IntVector2(0, progression.y));
            }
        }

        private IntVector2 GetPositionVisibleRange(IntVector2 position, bool xDimention)
        {
            int startValue, endValue;
          
            if (xDimention)
            {
                startValue = position.x - _settings.ActiveAreaSize / 2;
                endValue = position.x + _settings.ActiveAreaSize / 2;
                return new IntVector2(startValue, endValue);
            }
            else
            {
                startValue = position.y - _settings.ActiveAreaSize / 2;
                endValue = position.y + _settings.ActiveAreaSize / 2;
                return new IntVector2(startValue, endValue);
            }
        }

        private int GetVisibleDimantionProgression(IntVector2 sectorRange, IntVector2 visibleRange)
        {
            if (sectorRange.x > visibleRange.x)
            {
                return -1;
            }
            else if (sectorRange.y < visibleRange.y)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        
        private IntVector2 GetPositionVisibleProgression(IntVector2 position, ISectorInfo sectorInfo)
        {
            int xProgression, yProgression;
            xProgression = GetVisibleDimantionProgression(GetSectorRange(sectorInfo, true), GetPositionVisibleRange(position, true));
            yProgression = GetVisibleDimantionProgression(GetSectorRange(sectorInfo, false), GetPositionVisibleRange(position, false));

            return new IntVector2(xProgression, yProgression);
        }
    }
}