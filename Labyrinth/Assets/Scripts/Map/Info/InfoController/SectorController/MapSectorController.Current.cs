using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace ZScripts.Map.Info
{
    public partial class MapSectorController
    {     
        private ISectorInfo _currentSector;
        
        private void InitializeCurrentSector()
        {
            IntVector2 index = new IntVector2(0, 0);

            _currentSector = _mapInfoStoreController.UploadSectorInfo(index);
            if (_currentSector == null)
            {
                throw new SystemException();
            }
            _sectorLifecycleController.AddActiveSector(_currentSector.index);
            _sectorLifecycleController.UpdateSectors();

        }

        private void UpdateCurrentSector(IntVector2 position)
        {
            _currentSector =  GetSectorOfPosition(position, _currentSector);
            _sectorLifecycleController.AddActiveSector(_currentSector.index);
        }

        private ISectorInfo GetSectorOfPosition(IntVector2 position, ISectorInfo sectorInfo)
        {
            if (IsPositionInSector(sectorInfo, position))
            {
                return sectorInfo;
            }
            else
            {
                IntVector2 progression;
                progression = GetPositionSectorProgression(position, sectorInfo);
                IntVector2 newPosition = new IntVector2(sectorInfo.index.x + progression.x, sectorInfo.index.y + progression.y);
                sectorInfo = GetCashedSectorInfo(newPosition);
                return GetSectorOfPosition(position, sectorInfo);
            }
        }

        private ISectorInfo GetCashedSectorInfo(IntVector2 index)
        {
            if (_sectorLifecycleController.LoadedSectorInfos.ContainsKey(index))
            {
                return _sectorLifecycleController.LoadedSectorInfos[index];
            }
            else
            {
                return _mapInfoStoreController.UploadSectorInfo(index); 
            }
        }

        private bool IsPositionInSector(ISectorInfo sectorInfo, IntVector2 position)
        {
            IntVector2 result = GetPositionSectorProgression(position, sectorInfo);
            if (result.x == 0 && result.y == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private IntVector2 GetPositionSectorProgression(IntVector2 position, ISectorInfo sectorInfo)
        {
            int xProgression, yProgression;
            xProgression = GetSectorDimantionProgression(GetSectorRange(sectorInfo, true), position.x);
            yProgression = GetSectorDimantionProgression(GetSectorRange(sectorInfo, false), position.y);

            return new IntVector2(xProgression, yProgression);
        }
        
        private IntVector2 GetSectorRange(ISectorInfo sectorInfo, bool xDimention)
        {
            int startValue, endValue;
          
            if (xDimention)
            {
                startValue = sectorInfo.startPoint.x;
                endValue = sectorInfo.startPoint.x + sectorInfo.size.x - 1;
                return new IntVector2(startValue, endValue);
            }
            else
            {
                startValue = sectorInfo.startPoint.y;
                endValue = sectorInfo.startPoint.y + sectorInfo.size.y - 1;
                return new IntVector2(startValue, endValue);
            }
        }

        private int GetSectorDimantionProgression(IntVector2 range, int playerPosition)
        {
            if (playerPosition > range.y)
            {
                return 1;
            }
            else if (playerPosition < range.x)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

    }
}