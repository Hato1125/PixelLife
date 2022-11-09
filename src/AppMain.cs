global using System.Drawing;
global using System.Diagnostics;
global using DxLibDLL;

namespace PixelLife;

internal class AppMain
{
    private Stopwatch stopwatch = new();

    public static readonly AppMain Instans = new();
    public static TimeSpan Time = TimeSpan.Zero;
    public static readonly SceneManeger Scene = new();
    public static readonly GraphLoad Graph = new();

    private AppMain()
    {

    }

    void Initializer()
    {
        DX.SetOutApplicationLogValidFlag(DX.FALSE);
        DX.GetDefaultState(out int w, out int h, out int _, out int _, out int _, out int _, out int _, out int _, out int _, out int _);
        float sizeScale = w / AppInfo.CliantSize.Width;
        int width = (int)(AppInfo.CliantSize.Width * sizeScale);
        int height = (int)(AppInfo.CliantSize.Height * sizeScale);
        DX.SetUseFPUPreserveFlag(DX.TRUE);
        DX.SetFontUseAdjustSizeFlag(DX.FALSE);
        DX.SetGraphMode(AppInfo.CliantSize.Width, AppInfo.CliantSize.Height, 32);
        DX.SetWindowSize(width, height);
        DX.ChangeWindowMode(DX.TRUE);
        DX.SetWindowText($"{AppInfo.AppName} v{AppInfo.AppVersion}");
        DX.SetMainWindowClassName(AppInfo.AppName);
        if (DX.DxLib_Init() == -1)
            throw new Exception("DxLibの初期化に失敗しました。");
        DX.CreateMaskScreen();
        Graph.Load();

        Scene.AddScene("Title", new Title());

        Scene.ChangeScene("Title");
    }

    void Update()
    {
        Scene.Init();
        Scene.Update();
    }

    void Render()
    {
        Scene.Render();
    }

    void Uninitializer()
    {
        DX.DxLib_End();
    }

    public void Run()
    {
        Initializer();

        while (DX.ProcessMessage() != -1)
        {
            stopwatch.Restart();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ClearDrawScreen();
            Update();
            Render();
            DX.ScreenFlip();
            DX.SetWindowText(DX.GetFPS().ToString("#0"));
            Time = stopwatch.Elapsed;
        }
        Uninitializer();
    }
}