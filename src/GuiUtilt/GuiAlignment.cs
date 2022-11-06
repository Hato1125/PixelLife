namespace PixelLife.Gui;

internal class GuiAlignment : IDisposable
{
    /// <summary>
    /// X座標
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// Y座標
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Guiの間隔
    /// </summary>
    public int Interval { get; set; }

    /// <summary>
    /// 整列の種類
    /// </summary>
    public AlignmentType AlignmentType { get; set; }

    /// <summary>
    /// コントロールリスト
    /// </summary>
    public List<Control> Controls { get; set; }

    /// <summary>
    /// 初期化
    /// </summary>
    public GuiAlignment()
    {
        Controls = new();
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        int posX = 0;
        int posY = 0;

        for (int i = 0; i < Controls.Count(); i++)
        {
            switch (AlignmentType)
            {
                case AlignmentType.Beside:
                    posX += Controls[i].Width + Interval;
                    posY = 0;
                    break;

                case AlignmentType.Vertical:
                    posX = 0;
                    posY += Controls[i].Height + Interval;
                    break;
            }

            (Controls[i].X, Controls[i].Y) = (posX + X, posY + Y);
            Controls[i].Update();
        }
    }

    /// <summary>
    /// 描画
    /// </summary>
    public void Draw()
    {
        foreach (var item in Controls)
            item.Draw();
    }

    /// <summary>
    /// 解放
    /// </summary>
    public void Dispose()
    {
        foreach (var item in Controls)
            item.Dispose();
        Controls.Clear();
    }
}

public enum AlignmentType
{
    Beside,
    Vertical,
}