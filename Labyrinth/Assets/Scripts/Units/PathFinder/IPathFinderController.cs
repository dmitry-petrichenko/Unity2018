using System.Collections.Generic;
using Scripts;

namespace Units.PathFinder
{
    public interface IPathFinderController
    {
        List<IntVector2> GetPath(IntVector2 point, IntVector2 point2, List<IntVector2> occupiedPossitions);
        List<IntVector2> GetPath(IntVector2 point, IntVector2 point2, int pathLength);
    }
}