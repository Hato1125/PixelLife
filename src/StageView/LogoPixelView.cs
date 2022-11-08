namespace PixelLife.View;

internal class LogoPixelView
{
    private const int WIDTH = 10;
    private const int HEIGHT = 10;
    
    public int X;
    public int Y;
    public int Opacity;
    public int Xorigin;

    public void Draw()
    {
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, Opacity);
        DrawPixel(X, Y);
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
    }

    void DrawPixel(int x, int y)
    {
        DX.DrawBox(
            x + Xorigin,
            y,
            x + Xorigin + WIDTH,
            y + HEIGHT,
            0xffffff,
            DX.TRUE
        );
    }
}