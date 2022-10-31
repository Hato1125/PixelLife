global using System.Drawing;
global using DxLibDLL;

namespace PixelLife;

internal class AppMain
{
    public static readonly AppMain Instans = new();

    private AppMain()
    {

    }

    void Initializer()
    {
        DX.GetDefaultState(out int w, out int h, out int _, out int _, out int _, out int _, out int _, out int _, out int _, out int _);
        float sizeScale = w / AppInfo.CliantSize.Width;
        int width = (int)(AppInfo.CliantSize.Width * sizeScale);
        int height = (int)(AppInfo.CliantSize.Height * sizeScale);
        DX.SetOutApplicationLogValidFlag(DX.FALSE);
        DX.SetUseFPUPreserveFlag(DX.TRUE);
        DX.SetFontUseAdjustSizeFlag(DX.FALSE);
        DX.SetGraphMode(AppInfo.CliantSize.Width, AppInfo.CliantSize.Height, 32);
        DX.SetWindowSize(width, height);
        DX.ChangeWindowMode(DX.TRUE);
        DX.SetWindowText($"{AppInfo.AppName} v{AppInfo.AppVersion}");
        DX.SetMainWindowClassName(AppInfo.AppName);
        if (DX.DxLib_Init() == -1)
            throw new Exception("DxLibの初期化に失敗しました。");
    }

    void Update()
    {
    }

    void Render()
    {
    }

    void Uninitializer()
    {
        DX.DxLib_End();
    }

    public void Run()
    {
        Initializer();

        while(DX.ProcessMessage() != -1)
        {
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ClearDrawScreen();
            Update();
            Render();
            DX.ScreenFlip();
        }
        Uninitializer();
    }
}