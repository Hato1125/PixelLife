using PixelLife.Logic;
using PixelLife.View;

namespace PixelLife;

internal class Title : SceneBase
{
    public Title()
    {
        Child.Add(new Title_Back());
        Child.Add(new Title_Logo());
        Child.Add(new Title_Ui());
    }

    public override void Init()
    {
        base.Init();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Render()
    {
        base.Render();
    }

    public override void Finish()
    {
        base.Finish();
    }
}