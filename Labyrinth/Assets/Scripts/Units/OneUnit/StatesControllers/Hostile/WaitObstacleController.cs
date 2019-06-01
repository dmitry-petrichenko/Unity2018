using System;
using System.Collections.Generic;
using Scripts;
using Units.OccupatedMap;
using Units.OneUnit.StatesControllers.Base;
using UnityEngine;

namespace Units.OneUnit.StatesControllers.Hostile
{
    public class WaitObstacleController : IWaitObstacleController
    {
        public event Action OstacleStateChanged;
        
        private readonly IOccupatedPossitionsMap _occupatedPossitionsMap;
        private readonly IBaseActionController _baseActionController;
        
        private IntVector2 _attackPosition;
        private List<IOneUnitController> _subscribedUnits;
        
        public WaitObstacleController(IOccupatedPossitionsMap occupatedPossitionsMap, 
            IBaseActionController baseActionController)
        {
            _occupatedPossitionsMap = occupatedPossitionsMap;
            _baseActionController = baseActionController;
            _subscribedUnits = new List<IOneUnitController>();
        }

        public void Wait(IntVector2 position)
        {
            var units = GetUnitsInRange(_attackPosition);
            SubscribeOnUnits(units);
            var unit = GetNearestUnit(_baseActionController.Position, units);
            _baseActionController.WaitPosition(unit.Position);
        }
        
        public void Cancel()
        {
            UnsubscribeFromUnits();
            _attackPosition = IntVector2Constant.UNASSIGNET;
        }

        public void SetAttackPosition(IntVector2 position)
        {
            _attackPosition = position;
        }

        private IOneUnitController GetNearestUnit(IntVector2 position, List<IOneUnitController> units)
        {
            IOneUnitController unit = null;
           
            var adjacentPoints = position.GetAdjacentPoints();
            units.ForEach(u =>
            {
                if (adjacentPoints.Contains(u.Position))
                {
                    unit = u;
                }
            });

            return unit;
        }

        private void UnsubscribeFromUnits()
        {
            _subscribedUnits.ForEach(u =>
            {
                u.UnitEvents.RemovePositionChangedHandler(OstacleStateChangedHandler, _baseActionController);
            });
            _subscribedUnits.Clear();
        }
        
        private void SubscribeOnUnits(List<IOneUnitController> units)
        {
            units.ForEach(u =>
            {
                u.UnitEvents.AddPositionChangedHandler(OstacleStateChangedHandler, _baseActionController);
                _subscribedUnits.Add(u);
            });
        }

        private List<IOneUnitController> GetUnitsInRange(IntVector2 position)
        {
            var unitsInRange = new List<IOneUnitController>();
            var pointsInRange = GetPointsInRange(position);
            
            pointsInRange.ForEach(p =>
            {
                var unit = _occupatedPossitionsMap.GetUnitOnPosition(p);
                if (!(unit is UnitStub))
                {
                    unitsInRange.Add(unit);
                } 
            });
            
            return unitsInRange;
        }
        
        private List<IntVector2> GetPointsInRange(IntVector2 position)
        {
            List<IntVector2> adjacentPoints;
            List<IntVector2> pointsInRange = new List<IntVector2>();
            
            for (int i = 1; i < 5; i++)
            {
                adjacentPoints = position.GetAdjacentPoints(null, i);
                foreach (var point in adjacentPoints)
                {
                    pointsInRange.Add(point);
                }  
            }
            return pointsInRange;
        }

        private void OstacleStateChangedHandler()
        {
            OstacleStateChanged?.Invoke();
        }
    }
}