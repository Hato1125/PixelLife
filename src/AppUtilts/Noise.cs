namespace PixelLife;

internal class Noise
{
    /// <summary>
    /// モノクロのノイズ画像を生成する
    /// </summary>
    /// <param name="gHandle">グラフィックハンドル</param>
    /// <param name="level">ノイズレベル</param>
    /// <param name="opacity">透明度</param>
    public void CreateNoise(ref int gHandle, byte level, int opacity = 255)
    {
        if (gHandle == 0 || gHandle == -1)
            return;

        DX.GetGraphSize(gHandle, out int w, out int h);
        int softImage = DX.MakeARGB8ColorSoftImage(w, h);

        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                int color = DX.GetRand(level);
                DX.DrawPixelSoftImage(softImage, x, y, color, color, color, opacity);
            }

        gHandle = DX.CreateGraphFromSoftImage(softImage);
    }

    /// <summary>
    /// カラーのノイズ画像を生成する
    /// </summary>
    /// <param name="gHandle">グラフィックハンドル</param>
    /// <param name="level">ノイズレベル</param>
    /// <param name="opacity">透明度</param>
    public void CreateColorNoise(ref int gHandle, byte level, int opacity = 255)
    {
        if (gHandle == 0 || gHandle == -1)
            return;

        DX.GetGraphSize(gHandle, out int w, out int h);
        int softImage = DX.MakeARGB8ColorSoftImage(w, h);

        for (int x = 0; x < w; x++)
            for (int y = 0; y < h; y++)
            {
                int colorR = DX.GetRand(level);
                int colorG = DX.GetRand(level);
                int colorB = DX.GetRand(level);
                DX.DrawPixelSoftImage(softImage, x, y, colorR, colorG, colorB, opacity);
            }

        gHandle = DX.CreateGraphFromSoftImage(softImage);
    }
}