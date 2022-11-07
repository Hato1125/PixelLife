namespace PixelLife.Logic;

internal class TitleLogic : ILogic
{
    private double _fadeCounter;
    private string[] _sceneName = new string[3] { "Select", "Setting", "Exit" };
    public int FadeOpacity { get; private set; }
    public bool IsFade { get; set; }

    public void Init()
    {
        _fadeCounter = 0;
        FadeOpacity = 0;
        IsFade = false;
    }

    public void FadeCountTick()
    {
        if (!IsFade || _fadeCounter >= 90)
            return;

        _fadeCounter += AppMain.Time.TotalSeconds * 100;

        if (_fadeCounter > 90)
            _fadeCounter = 90;

        FadeOpacity = (int)(Math.Sin(_fadeCounter * Math.PI / 180) * 255);
    }

    public void DrawFade()
    {
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, FadeOpacity);
        DX.DrawBox(0, 0, AppInfo.CliantSize.Width, AppInfo.CliantSize.Height, 0xffffff, DX.TRUE);
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
    }

    public void ChangeScene(int index)
    {
        if (FadeOpacity < 255)
            return;

        Console.WriteLine("Change");
        // NOTE: まだTitle以外実装していないのでコメントアウト
        //AppMain.Scene.ChangeScene(_sceneName[index]);
    }
}