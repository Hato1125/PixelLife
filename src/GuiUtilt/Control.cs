namespace PixelLife.Gui;

internal class Control : IDisposable
{
    private const int DEFAULT_WIDTH = 220;
    private const int DEFAULT_HEIGHT = 30;

    private int _screen;
    private int _back;
    private int _widthBuffer;
    private int _heightBuffer;
    private int _relative_x;
    private int _relative_y;
    private bool _isJudge;
    private Mouse _mouse;

    /// <summary>
    /// コントロールのX座標
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// コントロールのY座標
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// コントロールの横幅
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// コントロールの高さ
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// コントロールの透明度
    /// </summary>
    public int Opacity { get; set; }

    /// <summary>
    /// Redの輝度
    /// </summary>
    public int Bright_R { get; set; }

    /// <summary>
    /// Greenの輝度
    /// </summary>
    public int Bright_G { get; set; }

    /// <summary>
    /// Blueの輝度
    /// </summary>
    public int Bright_B { get; set; }

    /// <summary>
    /// コントロールの描画時のブレンドモード
    /// </summary>
    public ControlBlend BlendMode { get; set; }

    /// <summary>
    /// 描画用アクション
    /// </summary>
    public Action? DrawAction { get; set; }

    /// <summary>
    /// サイズ変更時に呼ばれるリサイズアクション
    /// </summary>
    protected Action? ReSizeAction { get; set; }

    /// <summary>
    /// 子コントロールリスト
    /// </summary>
    public List<Control> Child { get; private set; }

    /// <summary>
    /// コントロールの初期化をする
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    public Control(int width, int height)
    {
        if (width <= 0)
            width = DEFAULT_WIDTH;

        if (height <= 0)
            height = DEFAULT_HEIGHT;

        _widthBuffer = 0;
        _heightBuffer = 0;
        Opacity = 255;
        Bright_R = 255;
        Bright_G = 255;
        Bright_B = 255;
        _isJudge = true;
        BlendMode = ControlBlend.None;
        Child = new();
        _mouse = new();

        Build(width, height);
    }

    /// <summary>
    /// コントロールの生成をする
    /// </summary>
    /// <param name="width">横幅</param>
    /// <param name="height">高さ</param>
    public void Build(int width, int height)
    {
        if (_screen != 0 && _screen != -1)
            DX.DeleteGraph(_screen);

        if (_back != 0 && _back != -1)
            DX.DeleteGraph(_back);

        this.Width = width;
        this.Height = height;
        _screen = DX.MakeScreen(this.Width, this.Height, DX.TRUE);
        _back = DX.MakeScreen(this.Width, this.Height, DX.TRUE);
    }

    /// <summary>
    /// 更新をする
    /// </summary>
    /// <param name="pointX">相対X座標</param>
    /// <param name="pointY">相対Y座標</param>
    public virtual void Update(int pointX = 0, int pointY = 0)
    {
        if (ReSizeAction != null)
            ReSize(ReSizeAction);

        _mouse.Update();

        if (pointX == 0 && pointY == 0)
        {
            _relative_x = _mouse.X - X;
            _relative_y = _mouse.Y - Y;
        }
        else
        {
            _relative_x = _mouse.X - pointX;
            _relative_y = _mouse.Y - pointY;
        }

        if (Child.Count() > 0)
        {
            foreach (var item in Child)
            {
                item.Update(item.X + X, item.Y + Y);
                _isJudge = item.IsHovering() ? false : true;
            }
        }
    }

    /// <summary>
    /// 描画をする
    /// </summary>
    public virtual void Draw()
    {
        int nowScreen = DX.GetDrawScreen();
        DX.SetDrawScreen(_screen);
        DX.ClearDrawScreen();

        if (DrawAction != null)
            DrawAction();

        foreach (var item in Child)
            item.Draw();

        DX.SetDrawScreen(nowScreen);

        DX.GetDrawScreenGraph(X, Y, X + Width, Y + Height, _back);
        DX.DrawGraph(X, Y, _back, DX.TRUE);
        DX.SetDrawBright(Bright_R, Bright_G, Bright_B);
        DX.SetDrawBlendMode(GetBlendMode(BlendMode), Opacity);
        DX.DrawGraph(X, Y, _screen, DX.TRUE);
        DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0);
        DX.SetDrawBright(255, 255, 255);
    }

    /// <summary>
    /// コントロールの解放をする
    /// </summary>
    public virtual void Dispose()
    {
        if (_screen != -1)
            DX.DeleteGraph(_screen);
        _screen = 0;

        foreach (var item in Child)
            item.Dispose();

        Child.Clear();
    }

    /// <summary>
    /// ホバーしたかを取得する
    /// </summary>
    public bool IsHovering()
    {
        if (_relative_x >= 0 && _relative_x <= Width
        && _relative_y >= 0 && _relative_y <= Height)
            return true;
        else
            return false;
    }

    /// <summary>
    /// クリックしている間を取得する
    /// </summary>
    public bool IsClick()
        => _isJudge && IsHovering() && _mouse.IsPush(MouseKey.Left);

    /// <summary>
    /// クリックした瞬間を取得する
    /// </summary>
    public bool IsClicked()
        => _isJudge && IsHovering() && _mouse.IsPushed(MouseKey.Left);

    /// <summary>
    /// 離した瞬間を取得する
    /// </summary>
    public bool IsClickUp()
        => _isJudge && IsHovering() && _mouse.IsUped(MouseKey.Left);

    // コントロールのリサイズを管理する
    protected void ReSize(Action reSizeAction)
    {
        if (Width != _widthBuffer || Height != _heightBuffer)
        {
            reSizeAction();
            _widthBuffer = Width;
            _heightBuffer = Height;
        }
    }

    // ブレンドモードを取得する
    int GetBlendMode(ControlBlend blend)
    {
        switch (blend)
        {
            case ControlBlend.Add:
                return DX.DX_BLENDMODE_ADD;

            case ControlBlend.Sub:
                return DX.DX_BLENDMODE_NOBLEND;

            case ControlBlend.None:
                return DX.DX_BLENDMODE_ALPHA;

            default:
                return DX.DX_BLENDMODE_ALPHA;
        }
    }

    /// <summary>
    /// ブレンドモード
    /// </summary>
    public enum ControlBlend
    {
        /// <summary>
        /// 加算合成
        /// </summary>
        Add,

        /// <summary>
        /// 減産合成
        /// </summary>
        Sub,

        /// <summary>
        /// なし
        /// </summary>
        None,
    }
}