namespace PixelLife;

internal class GraphLoad
{
    #region PathHelper
    private readonly string TX = $"{AppContext.BaseDirectory}Data\\Resource\\";
    private readonly string TITLE = "01_Title\\";
    #endregion

    public readonly Dictionary<TitleGraph, int> TitleGraphs = new();

    public void Load()
    {
        TitleGraphs.Add(TitleGraph.Logo, DX.LoadGraph($"{TX}{TITLE}Logo.png"));
    }
}

public enum TitleGraph
{
    Logo
}