namespace PixelLife;

internal class GraphLoad
{
    #region PathHelper
    private readonly string TX = $"{AppContext.BaseDirectory}Data\\Resource\\";
    private readonly string TITLE = "title\\";
    #endregion

    public readonly Dictionary<TitleGraph, int> TitleGraphs = new();

    public void Load()
    {
        TitleGraphs.Add(TitleGraph.Logo, DX.LoadGraph($"{TX}{TITLE}Logo.png"));
        TitleGraphs.Add(TitleGraph.Back, DX.LoadGraph($"{TX}{TITLE}Back.png"));
    }
}

public enum TitleGraph
{
    Logo,
    Back,
}