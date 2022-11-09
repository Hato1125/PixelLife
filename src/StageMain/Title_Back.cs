using PixelLife.Logic;
using PixelLife.View;

namespace PixelLife;

internal class Title_Back : SceneBase
{
    private readonly BackView backView = new();

    public override void Init()
    {
    }

    public override void Update()
    {
    }

    public override void Render()
    {
        backView.Draw();
    }

    public override void Finish()
    {
    }
}