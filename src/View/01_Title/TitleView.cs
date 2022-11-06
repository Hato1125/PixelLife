namespace PixelLife.View;

internal class TitleView
{
    public void FadeDraw(int value)
    {
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, value);
        DX.DrawBox(0, 0, AppInfo.CliantSize.Width, AppInfo.CliantSize.Height, 0xffffff, DX.TRUE);
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
    }
}