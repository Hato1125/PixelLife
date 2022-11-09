using PixelLife.Logic;
using PixelLife.View;

namespace PixelLife;

internal class Title_Logo : SceneBase
{
    private readonly LogoLogic logoLogic = new();
    private readonly LogoView logoView = new();

    public override void Init()
    {
        logoLogic.Init();
        logoView.Init();
    }

    public override void Update()
    {
        logoLogic.Update();
        for(int i = 0; i < logoLogic.logoPixels.Length; i++)
        {
            logoView.logoPixelViews[i].X = 710 + (int)logoLogic.logoPixels[i].X;
            logoView.logoPixelViews[i].Y = 540 + (int)logoLogic.logoPixels[i].Y;
            logoView.logoPixelViews[i].Xorigin =(int)logoLogic.logoPixels[i].Xorigin;
            logoView.logoPixelViews[i].Opacity = (int)logoLogic.logoPixels[i].Opacity;
        }
    }

    public override void Render()
    {
        logoView.Draw();
    }

    public override void Finish()
    {
    }
}