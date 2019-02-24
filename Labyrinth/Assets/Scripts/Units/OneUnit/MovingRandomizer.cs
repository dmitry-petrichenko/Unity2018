using System.Collections.Generic;
using Scripts;
using Scripts.Extensions;
using Units.PathFinder;

namespace Units.OneUnit
{
    public class MovingRandomizer : Disposable, IMovingRandomizer
    {
        private IGrid _grid;
        private List<IntVector2> _vacantPoints;
        private IntVector2 _unitPosition;
        private IntVector2 startPoint = new IntVector2(0, 0);
        private IntVector2 endPoint = new IntVector2(7, 7);

        public MovingRandomizer(IGrid grid)
        {
            _vacantPoints = new List<IntVector2>();
            _grid = grid;
            
            InitializeVacantPoints();
        }
          
        private IntVector2 GenerateVacantPoint(List<IntVector2> points)
        {
            int index = UnityEngine.Random.Range(0, points.Count - 1);

            return points[index];
        }
        
        public IntVector2 GetRandomPoint(IntVector2 unitPosition)
        {
            IntVector2 intVetctor2;
            
            _unitPosition = unitPosition;
            intVetctor2 = GenerateVacantPoint(_vacantPoints);
            while (intVetctor2.x == _unitPosition.x &&
                   intVetctor2.y == _unitPosition.y)
            {
                intVetctor2 = GenerateVacantPoint(_vacantPoints);
            }

            return intVetctor2;
        }
        
        private void InitializeVacantPoints()
        {
            IntVector2 intVector2;
            
            for (int i = startPoint.x; i < endPoint.x; i++)
            {
                for (int j = startPoint.y; j < endPoint.y; j++)
                {
                    intVector2 = new IntVector2(i, j);
                    if (_grid.GetCell(intVector2))
                    {
                        _vacantPoints.Add(intVector2);
                    }
                }
            }
        }
    }
}