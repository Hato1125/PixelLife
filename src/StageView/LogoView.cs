namespace PixelLife.View;

internal class LogoView : IView
{
    public LogoPixelView[] logoPixelViews = new LogoPixelView[50];

    public void Init()
    {
        for (int i = 0; i < logoPixelViews.Length; i++)
            logoPixelViews[i] = new();
    }

    public void Update()
    {
    }

    public void Draw()
    {
        for (int i = 0; i < logoPixelViews.Length; i++)
            logoPixelViews[i].Draw();

        DX.GetGraphSize(AppMain.Graph.TitleGraphs[TitleGraph.Logo], out int w, out int h);
        DX.DrawGraph(
            (AppInfo.CliantSize.Width - w) / 2,
            (AppInfo.CliantSize.Height - h) / 2,
            AppMain.Graph.TitleGraphs[TitleGraph.Logo],
            DX.TRUE
        );
    }
}