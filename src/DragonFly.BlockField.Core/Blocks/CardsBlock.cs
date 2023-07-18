namespace DragonFly.BlockField;

/// <summary>
/// CardsBlock
/// </summary>
public class CardsBlock : Block
{
    public override string CssIcon => "fa-regular fa-id-card";

    /// <summary>
    /// Cards
    /// </summary>
    public IList<CardItem> Cards { get; set; } = new List<CardItem>();
}
