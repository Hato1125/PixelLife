namespace PixelLife.View;

internal class BackView : IView
{
    public void Init()
    {

    }

    public void Update()
    {

    }

    public void Draw()
    {
        DX.DrawGraph(0, 0, AppMain.Graph.TitleGraphs[TitleGraph.Back], DX.TRUE);
    }
}