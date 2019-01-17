using Scripts;

namespace Units.PathFinder
{
    public interface IGrid
    {
        bool GetCell(IntVector2 index);
    }
}