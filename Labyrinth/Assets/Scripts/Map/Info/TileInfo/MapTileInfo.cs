namespace Scripts.Map.Info
{
    public class MapTileInfo : IMapTileInfo
    {
        public int Type { get; set; }
        public bool IsEmpty()
        {
            switch (Type)
            {
                case MapTileType.Square :
                {
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        public IntVector2 ViewPosition { get; set; }
        public IntVector2 Index { get; set; }

        public void Initialize(int Type, IntVector2 ViewPosition, IntVector2 Index)
        {
            this.Index = Index;
            this.Type = Type;
            this.ViewPosition = ViewPosition;
        }
    }
}