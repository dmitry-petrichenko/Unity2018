namespace ZScripts.Map.Info
{
    public interface ITileView
    {
        IntVector2 ViewPosition { get; set; }
        int Type { get; set; }
        bool IsEmpty();
    }
}