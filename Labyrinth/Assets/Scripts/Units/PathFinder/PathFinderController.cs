using System;
using System.Collections.Generic;
using System.Linq;
using Scripts;
using Scripts.Extensions;

namespace Units.PathFinder
{
    public partial class PathFinderController : MyDisposable, IPathFinderController
    {
        private IGrid _grid;
        private List<IntVector2> _closeList;
        private List<Vertex2D> _openList;
        private List<IntVector2> _wayPoints;
        private IntVector2 _destinationPoint;
        private bool _complete;
        private Vertex2D _currentVertex;
        private List<IntVector2> _occupiedIndexes;

        private List<int> _openListF;
        private Dictionary<Vertex2D, int> _openListDict;


        public PathFinderController(IGrid grid)
        {
            _grid = grid;
            Initialize();
        }

        public void Initialize()
        {
            _closeList = new List<IntVector2>();
            _openList = new List<Vertex2D>();
            _openListF = new List<int>();
            _openListDict = new Dictionary<Vertex2D, int>();
        }

        public event Action<IntVector2> NoWayToDestinationPoint;
        public event Action<IntVector2> DestinationPointIsNotEmpty;

        public List<IntVector2> GetPath(IntVector2 point, IntVector2 point2, List<IntVector2> occupiedIndexes = null)
        {
            if (occupiedIndexes != null)
            {
                _occupiedIndexes = occupiedIndexes;
            }
            else
            {
                _occupiedIndexes = new List<IntVector2>();
            }

            _destinationPoint = point2;
            _wayPoints = new List<IntVector2>();
            _openListF = new List<int>();
            _openListDict = new Dictionary<Vertex2D, int>();

            if (IsInOccupiedIndexses(_destinationPoint))
            {
                DestinationPointIsNotEmpty?.Invoke(_destinationPoint);
                return _wayPoints;
            }

            if (IsPointBusy(_destinationPoint))
            {
                DestinationPointIsNotEmpty?.Invoke(_destinationPoint);
                return _wayPoints;
            }

            Vertex2D first = CreateVertex2D(point, null);
            if (first == null)
            {
                throw new Exception("PathFinderController: first point in path is not empty");
            }
            AddInOpenList(first);

            while (!_complete)
            {
                _currentVertex = GetMinorVertexByF(_openList);

                RemoveFromOpenList(_currentVertex);
                _closeList.Add(_currentVertex.Index);

                List<Vertex2D> neighbours = GetNeibhours(_currentVertex);

                foreach (var neighbour in neighbours)
                {
                    AddInOpenList(neighbour);
                }

                if (_openList.Count == 0)
                {
                    _complete = true;
                }
            }

            foreach (var vertex2D in _openList)
            {
                if (vertex2D.Index.x == _destinationPoint.x && vertex2D.Index.y == _destinationPoint.y)
                    SelectVertex(vertex2D);
            }

            if (_wayPoints.Count == 0)
            {
                NoWayToDestinationPoint?.Invoke(_destinationPoint);
                Reset();
                return _wayPoints;
            }

            Reset();
            _wayPoints.Reverse();
            _wayPoints.RemoveAt(0);

            return _wayPoints;
        }

        private void Reset()
        {
            _openList = new List<Vertex2D>();
            _closeList = new List<IntVector2>();
            _destinationPoint = new IntVector2(0, 0);
            _complete = false;
        }

        private void SelectVertex(Vertex2D vertex2D)
        {
            _wayPoints.Add(vertex2D.Index);
            if (vertex2D.Parent != null)
                SelectVertex(vertex2D.Parent);
        }

        private void AddInOpenList(Vertex2D vertex)
        {
            if (vertex.Index.x == _destinationPoint.x && vertex.Index.y == _destinationPoint.y)
            {
                _complete = true;
            }

            foreach (var vertex2D in _openList)
            {
                if (vertex2D.Index.x == vertex.Index.x && vertex2D.Index.y == vertex.Index.y)
                {
                    return;
                }
            }

            _openListF.Add(vertex.F);
            _openListDict.Add(vertex, vertex.F);
            _openList.Add(vertex);
        }

        private void RemoveFromOpenList(Vertex2D vertex)
        {
            _openList.Remove(vertex);
            _openListF.Remove(vertex.F);
            _openListDict.Remove(vertex);
        }

        private Vertex2D GetMinorVertexByF(List<Vertex2D> list)
        {
            _openListF.Sort();
            return _openListDict.FirstOrDefault(x => x.Value == _openListF[0]).Key;

            /*
            List<Vertex2D> sorted = list.OrderBy(v => v.F).ToList();
            return sorted[0];
            */
        }

        private List<Vertex2D> GetNeibhours(Vertex2D vertex)
        {
            List<Vertex2D> neighbours = new List<Vertex2D>();
            Vertex2D neighbour;

            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x + 1, vertex.Index.y), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x + 1, vertex.Index.y + 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x, vertex.Index.y + 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x + 1, vertex.Index.y - 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x, vertex.Index.y - 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x - 1, vertex.Index.y - 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x - 1, vertex.Index.y), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }
            neighbour = CreateVertex2D(new IntVector2(vertex.Index.x - 1, vertex.Index.y + 1), vertex);
            if (neighbour != null) { neighbours.Add(neighbour); }

            return neighbours;
        }

        private Vertex2D CreateVertex2D(IntVector2 index, Vertex2D parent)
        {
            Vertex2D vertex2D = null;

            if (!IsPointBusy(index) && !IsInCloseList(index) && !IsInOccupiedIndexses(index))
            {
                vertex2D = new Vertex2D();
                vertex2D.Initialize(index, parent, _destinationPoint);
            }

            return vertex2D;
        }

        private bool IsPointBusy(IntVector2 index)
        {
            return !_grid.GetCell(index);
        }

        private bool IsInOccupiedIndexses(IntVector2 Index)
        {
            return _occupiedIndexes.Contains(Index);
        }

        private bool IsInCloseList(IntVector2 Index)
        {
            return _closeList.Contains(Index);
        }
    }
}