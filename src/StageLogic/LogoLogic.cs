namespace PixelLife.Logic;

internal class LogoLogic : ILogic
{
    public LogoPixel[] logoPixels = new LogoPixel[50];

    public void Init()
    {
        for (int i = 0; i < logoPixels.Length; i++)
        {
            logoPixels[i] = new();
            logoPixels[i].Init();
        }
    }

    public void Update()
    {
        for (int i = 0; i < logoPixels.Length; i++)
            logoPixels[i].CalMove();
    }
}