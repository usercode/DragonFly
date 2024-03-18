namespace DragonFly;

public class CardItem
{
    /// <summary>
    /// AssetId
    /// </summary>
    public Guid? AssetId { get; set; }

    /// <summary>
    /// Title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// Blocks
    /// </summary>
    public IList<Block> Blocks { get; set; } = new List<Block>();

    /// <summary>
    /// Header
    /// </summary>
    public string? Header { get; set; }

    /// <summary>
    /// Footer
    /// </summary>
    public string? Footer { get; set; }

    /// <summary>
    /// BackgroundColor
    /// </summary>
    public ColorType? BackgroundColor { get; set; }

    /// <summary>
    /// BorderColor
    /// </summary>
    public ColorType? BorderColor { get; set; }
}

