namespace PixelLife;

/// <summary>
/// アプリケーションの情報を定義するクラス
/// </summary>
internal class AppInfo
{
    /// <summary>
    /// アプリケーション名
    /// </summary>
    public static readonly string AppName = "PixelLife";

    /// <summary>
    /// バージョン
    /// </summary>
    public static readonly string AppVersion = "0.0.0";

    /// <summary>
    /// アプリケーションのクライアントサイズ
    /// </summary>
    public static readonly (int Width, int Height) CliantSize = (1920, 1080);

    /// <summary>
    /// アプリケーションのWindowサイズを取得、または設定する
    /// </summary>
    /// <value>WindowSize</value>
    public static (int Width, int Height) WindowSize
    {
        get
        {
            DX.GetWindowSize(out int w, out int h);
            return (w, h);
        }
        set
        {
            DX.SetWindowSize(value.Width, value.Height);
        }
    }
}