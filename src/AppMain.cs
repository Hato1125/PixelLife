global using System.Drawing;
global using System.Diagnostics;
global using DxLibDLL;
using PixelLife.Gui;

namespace PixelLife;

internal class AppMain
{
    public static readonly AppMain Instans = new();
    public Stopwatch w = new();
    public TimeSpan s;
    private int a;
    private Button? b;

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

        a = DX.LoadGraph(@"C:\Users\hatof\Pictures\1_pantone_collab_dark(Photo)(noise_scale)(Level3)(x1.000000).png");
        b = new(250, 45, "Segoe UI", 23, 7);
        b.Text = "テストボタン";
        b.EdgeSize = 0;
        b.EdgeColor = Color.FromArgb(200, 200, 200);
        b.IsBlur = true;
        b.SetIcon("🎮", "Segoe UI Emoji", 25, 4);
        b.IconPadding = (24, 7);
    }

    void Update()
    {
        b.X = 1650;
        b.Y = 600;
        b.Delta = s.TotalSeconds;
        b.Update();
    }

    void Render()
    {
        DX.DrawGraph(0, 0, a, DX.TRUE);
        b.Draw();
    }

    void Uninitializer()
    {
        b.Dispose();
        DX.DxLib_End();
    }

    public void Run()
    {
        Initializer();

        while (DX.ProcessMessage() != -1)
        {
            w.Restart();
            DX.SetDrawScreen(DX.DX_SCREEN_BACK);
            DX.ClearDrawScreen();
            Update();
            Render();
            DX.ScreenFlip();
            s = w.Elapsed;
        }
        Uninitializer();
    }
}