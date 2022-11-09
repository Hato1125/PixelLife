using PixelLife.Logic;
using PixelLife.View;

namespace PixelLife;

internal class Title_Ui : SceneBase
{
    private readonly TitleLogic titleLogic = new();
    private readonly TitleView titleVeiw = new();
    private int _sceneIndex;

    public override void Init()
    {
        _sceneIndex = 0;
        titleLogic.Init();
        titleVeiw.Init();
    }

    public override void Update()
    {
        titleVeiw.Update();

        for (int i = 0; i < titleVeiw.Alignment.Controls.Count(); i++)
            if (titleVeiw.Alignment.Controls[i].IsClicked())
            {
                _sceneIndex = i;
                titleLogic.IsFade = true;
            }

        titleLogic.FadeCountTick();
        titleLogic.ChangeScene(_sceneIndex);
    }

    public override void Render()
    {
        titleVeiw.Draw();
        titleLogic.DrawFade();
    }

    public override void Finish()
    {
    }
}