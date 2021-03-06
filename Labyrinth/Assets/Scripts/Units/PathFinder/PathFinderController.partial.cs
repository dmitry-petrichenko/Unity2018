﻿using System.Collections.Generic;
using Scripts;

namespace Units.PathFinder
{
    public partial class PathFinderController
    {
        public List<IntVector2> GetPath(IntVector2 point, IntVector2 point2, int pathLength)
        {
            int localPathLength = 0;
            _occupiedIndexes = new List<IntVector2>();

            _destinationPoint = point2;
            _wayPoints = new List<IntVector2>();
            _openListF = new List<int>();

            if (IsInOccupiedIndexses(_destinationPoint))
            {
                return _wayPoints;
            }

            Vertex2D first = CreateVertex2D(point, null);
            AddInOpenList(first);

            while (localPathLength <= 1)
            {
                _currentVertex = GetMinorVertexByF();

                RemoveFromOpenList(_currentVertex);
                _closeList.Add(_currentVertex.Index);

                List<Vertex2D> neighbours = GetNeibhours(_currentVertex);

                foreach (var neighbour in neighbours)
                {
                    AddInOpenList(neighbour);
                }

                localPathLength++;
            }

            foreach (var vertex2D in _openList)
            {
                if (vertex2D.Index.x == _destinationPoint.x && vertex2D.Index.y == _destinationPoint.y)
                    SelectVertex(vertex2D);
            }

            _wayPoints.Reverse();

            _openList = new HashSet<Vertex2D>();
            _closeList = new HashSet<IntVector2>();
            _destinationPoint = new IntVector2(0, 0);
            _complete = false;

            if (_wayPoints.Count > 0)
            {
                _wayPoints.RemoveAt(0);
            }

            return _wayPoints;
        }
    }
}