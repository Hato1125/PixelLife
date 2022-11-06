using PixelLife.Model;
using PixelLife.View;

namespace PixelLife.Presenter;

internal class TitleUi : SceneBase
{
    private readonly UiModel uiModel = new();
    private readonly UiView uiView = new();
    private int _clickIndex;

    public override void Init()
    {
        _clickIndex = 0;
        uiView.Init();

        base.Init();
    }

    public override void Update()
    {
        uiView.Update();

        for (int i = 0; i < uiView.alignment.Controls.Count(); i++)
            if (uiView.alignment.Controls[i].IsClicked())
            {
                Title.titleModel.FadeFlag = true;
                _clickIndex = i;
            }

        //if (Title.titleModel.FadeValue > 90)
            //uiModel.ChangeScene(_clickIndex);

        base.Update();
    }

    public override void Render()
    {
        uiView.Draw();

        base.Render();
    }

    public override void Finish()
    {
        base.Finish();
    }
}