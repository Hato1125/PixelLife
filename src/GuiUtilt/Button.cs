using System.Drawing;

namespace PixelLife.Gui;

internal class Button : Control, IDisposable
{
    private const int blurMargin = 20;
    private const int HoverAnime_Max = 25;
    private const int ClickAnime_Max = 50;

    private int _font;
    private int _mask;
    private int _back;
    private int _icon;
    private int _fontSize;
    private int _fontWeight;
    private double _ctHover;
    private double _ctClick;
    private bool _isGraph;
    private string _iconStr;
    private string _fontName;

    /// <summary>
    ///  背景の透明度
    /// </summary>
    public int BlurOpacity { get; set; }

    /// <summary>
    /// 画像アイコンの余白
    /// </summary>
    public (int Left, int Top) IconPadding { get; set; }

    /// <summary>
    /// ボタンの角の半径
    /// </summary>
    public float Radius { get; set; }

    /// <summary>
    /// 縁取りのサイズ
    /// </summary>
    public float EdgeSize { get; set; }

    /// <summary>
    /// デルタ
    /// </summary>
    public double Delta { get; set; }

    /// <summary>
    /// テキスト
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// 背景をぼかすか否か
    /// </summary>
    public bool IsBlur { get; set; }

    /// <summary>
    /// 背景色
    /// </summary>
    public Color BackColor { get; set; }

    /// <summary>
    /// 文字色
    /// </summary>
    public Color ForeColor { get; set; }

    /// <summary>
    /// 縁取りの色
    /// </summary>
    public Color EdgeColor { get; set; }

    /// <summary>
    /// ボタンを初期化する
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    /// <param name="fontName">フォント名</param>
    /// <param name="fontSize">フォントサイズ</param>
    /// <param name="fontWeight">フォントの太さ</param>
    public Button(int width, int height, string fontName, int fontSize, int fontWeight)
    : base(width, height)
    {
        this._fontSize = fontSize;
        this._fontWeight = fontWeight;
        this._fontName = fontName;
        this._iconStr = string.Empty;
        this.Text = string.Empty;
        this.Radius = 10.0f;
        this.EdgeSize = 2.0f;
        this.BlurOpacity = 150;
        BackColor = Color.FromArgb(28, 28, 30);
        ForeColor = Color.FromArgb(255, 255, 255);
        EdgeColor = Color.FromArgb(158, 157, 159);
        Build(width, height, fontName, fontSize, fontWeight);
    }

    /// <summary>
    /// アイコンをセットする
    /// </summary>
    /// <param name="filePath">グラフィックのパス</param>
    public void SetIcon(string filePath)
    {
        if (_icon != -1)
            DX.DeleteGraph(_icon);

        _icon = DX.LoadGraph(filePath);
        _isGraph = true;
    }

    /// <summary>
    /// アイコンをセットする
    /// </summary>
    /// <param name="iconStr">アイコン文字</param>
    /// <param name="fontName">フォント名</param>
    /// <param name="fontSize">フォントサイズ</param>
    /// <param name="fontWeight">フォントの太さ</param>
    public void SetIcon(string iconStr, string fontName, int fontSize, int fontWeight)
    {
        if (_icon != -1)
            DX.DeleteFontToHandle(_icon);

        _icon = DX.CreateFontToHandle(fontName, fontSize, fontWeight, DX.DX_FONTTYPE_ANTIALIASING_8X8);
        this._iconStr = iconStr;
        _isGraph = false;
    }

    /// <summary>
    /// ボタンをセイセする
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    /// <param name="fontName">フォント名</param>
    /// <param name="fontSize">フォントサイズ</param>
    /// <param name="fontWeight">フォントの太さ</param>
    public void Build(int width, int height, string fontName, int fontSize, int fontWeight)
    {
        base.Build(width, height);

        if (_font != -1)
            DX.DeleteFontToHandle(_font);

        if (_mask != -1)
            DX.DeleteGraph(_mask);

        if (_back != -1)
            DX.DeleteGraph(_back);

        _font = DX.CreateFontToHandle(fontName, fontSize, fontWeight, DX.DX_FONTTYPE_ANTIALIASING_8X8);
        _mask = DX.MakeScreen(width, height, DX.TRUE);
        _back = DX.MakeScreen(width, height + blurMargin, DX.TRUE);
        DX.SetMaskScreenGraph(_mask);
    }

    /// <summary>
    /// 更新をする
    /// </summary>
    /// <param name="pointX">相対X座標</param>
    /// <param name="pointY">相対Y座標</param>
    public override void Update(int pointX = 0, int pointY = 0)
    {
        ReSizeAction = () => { ReSize(); };

        base.Update(pointX, pointY);

        HoverAnime();
        ClickAnime();
    }

    /// <summary>
    /// 描画をする
    /// </summary>
    public override void Draw()
    {
        DrawAction = () =>
        {
            DrawBack();
            DrawText();
            DrawIcon();
        };

        if (IsBlur)
            DX.GetDrawScreenGraph(X, Y, X + Width, Y + Height + blurMargin, _back);

        base.Draw();
    }

    /// <summary>
    /// ボタンを解放する
    /// </summary>
    public override void Dispose()
    {
        base.Dispose();

        if (_font != -1)
            DX.DeleteFontToHandle(_font);

        if (_mask != -1)
            DX.DeleteGraph(_mask);

        if (_back != -1)
            DX.DeleteGraph(_back);

        if (_icon != -1)
        {
            if (_isGraph)
                DX.DeleteGraph(_icon);
            else
                DX.DeleteFontToHandle(_icon);
        }
    }

    // リサイズイベント
    void ReSize()
    {
        Build(Width, Height, _fontName, _fontSize, _fontWeight);
    }

    // 背景をレンダリングする
    void DrawBack()
    {
        uint backColor = DX.GetColor(BackColor.R, BackColor.G, BackColor.B);
        int nowScreen = DX.GetDrawScreen();
        DX.SetMaskReverseEffectFlag(DX.TRUE);
        DX.SetDrawScreen(_mask);
        DX.ClearDrawScreen();
        DX.DrawRoundRectAA(0, 0, Width, Height, Radius, Radius, 255, 0x000000, DX.TRUE);
        DX.SetDrawScreen(nowScreen);

        DX.SetUseMaskScreenFlag(DX.TRUE);

        if (IsBlur)
        {
            DX.GraphFilter(_back, DX.DX_GRAPH_FILTER_GAUSS, 64, 8000);
            DX.DrawGraph(0, 0, _back, DX.TRUE);
        }

        DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, IsBlur ? BlurOpacity : 255);

        DX.DrawBox(0, 0, Width, Height, backColor, DX.TRUE);

        if (EdgeSize > 0)
            DrawEdge();

        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);

        DrawFade();

        DX.SetUseMaskScreenFlag(DX.FALSE);

        DX.SetMaskReverseEffectFlag(DX.FALSE);
    }

    // フェードをレンダリングする
    void DrawFade()
    {
        double fade = Math.Sin(_ctHover * Math.PI / 180) * HoverAnime_Max;
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_ALPHA, (int)fade);
        DX.DrawBox(0, 0, Width, Height, 0x000000, DX.TRUE);
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
    }

    // 縁取りをレンダリングする
    void DrawEdge()
    {
        uint edgeColor = DX.GetColor(EdgeColor.R, EdgeColor.G, EdgeColor.B);
        DX.DrawRoundRectAA(0, 0, Width, Height, Radius + EdgeSize / 2, Radius + EdgeSize / 2, 255, edgeColor, DX.FALSE, EdgeSize * 2);
    }

    // 文字をレンダリングする
    void DrawText()
    {
        if (Text == string.Empty)
            return;

        uint foreColor = DX.GetColor(ForeColor.R, ForeColor.G, ForeColor.B);
        int width = DX.GetDrawStringWidthToHandle(Text, Text.Length, _font);
        int height = DX.GetFontSizeToHandle(_font);

        DX.DrawStringFToHandle(
            (this.Width - width) / 2,
            (this.Height - height) / 2,
            Text,
            foreColor,
            _font
        );
    }

    // アイコンをレンダリングする
    void DrawIcon()
    {
        if (_icon == 0 || _icon == -1)
            return;

        if (_isGraph)
        {
            DX.GetGraphSize(_icon, out int _, out int h);
            float scale = (float)(Height - IconPadding.Top * 2) / (float)h;

            DX.DrawRotaGraph2F(IconPadding.Left, IconPadding.Top, 0, 0, scale, 0.0d, _icon, DX.TRUE);
        }
        else
        {
            uint foreColor = DX.GetColor(ForeColor.R, ForeColor.G, ForeColor.B);
            int height = DX.GetFontSizeToHandle(_icon);
            float centerHeight = (float)(this.Height - height) / 2.0f;
            DX.DrawStringFToHandle(centerHeight, centerHeight, _iconStr, foreColor, _icon);
        }
    }

    // ホバー時のアニメ
    void HoverAnime()
    {
        if (IsHovering())
        {
            _ctHover += Delta * 900;

            if (_ctHover > 90)
                _ctHover = 90;
        }
        else
        {
            _ctHover -= Delta * 900;

            if (_ctHover < 0)
                _ctHover = 0;
        }
    }

    // クリック時のアニメ
    void ClickAnime()
    {
        if (IsClick())
        {
            _ctClick += Delta * 700;

            if (_ctClick > 90)
                _ctClick = 90;
        }
        else
        {
            _ctClick -= Delta * 700;

            if (_ctClick < 0)
                _ctClick = 0;
        }

        double fade = Math.Sin(_ctClick * Math.PI / 180) * ClickAnime_Max;
        Opacity = 255 - (int)fade;
    }
}