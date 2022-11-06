using PixelLife.Model;
using PixelLife.View;

namespace PixelLife.Presenter;

internal class Title : SceneBase
{
    public static readonly TitleModel titleModel = new();
    public static readonly TitleView titleView = new();

    public Title()
    {
        Child.Add(new TitleUi());
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Update()
    {
        titleModel.Fade();

        base.Update();
    }

    public override void Render()
    {
        base.Render();

        titleView.FadeDraw((int)titleModel.FadeValue);
    }

    public override void Finish()
    {
        base.Finish();
    }
}