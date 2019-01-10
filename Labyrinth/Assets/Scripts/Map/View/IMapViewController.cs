using System;

namespace Scripts.Map.View
{
    public interface IMapViewController
    {
        void InitializePlane(IntVector2 position);
        void InitializeCube(IntVector2 position);
        void InitializeEmpty(IntVector2 position);
        void InitializeSquare(IntVector2 position);
        void DestroyTile(IntVector2 position);
        event Action<IntVector2> TileClicked;
        event Action<IntVector2> RightClicked;
    }
}