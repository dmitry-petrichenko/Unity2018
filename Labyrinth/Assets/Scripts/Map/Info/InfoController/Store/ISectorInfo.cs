namespace ZScripts.Map.Info
{
    public interface ISectorInfo
    {
        IntVector2 startPoint { get; set; }
        IntVector2 size { get; set; }
        IntVector2 index { get; set; }
    }
}