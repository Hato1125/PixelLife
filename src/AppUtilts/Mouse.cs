namespace PixelLife;

internal class Mouse
{
    private readonly sbyte[] value = new sbyte[7];

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        for (int i = 0; i < value.Length; i++)
        {
            if (DX.GetMouseInput() == (int)GetKeyCode(i))
            {
                if (!IsPush(GetKeyCode(i)))
                    value[i] = 1;
                else
                    value[i] = 2;
            }
            else
            {
                if (IsPush(GetKeyCode(i)))
                    value[i] = -1;
                else
                    value[i] = 0;
            }
        }
    }

    /// <summary>
    /// キーを押している間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    public bool IsPush(MouseKey keyCode)
        => value[GetKeyCodeIndex(keyCode)] > 0;

    /// <summary>
    /// キーを押した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    public bool IsPushed(MouseKey keyCode)
        => value[GetKeyCodeIndex(keyCode)] == 1;

    /// <summary>
    /// キーを離した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    public bool IsUped(MouseKey keyCode)
        => value[GetKeyCodeIndex(keyCode)] == -1;

    private int GetKeyCodeIndex(MouseKey keyCode)
    {
        switch (keyCode)
        {
            case MouseKey.Left:
                return 0;

            case MouseKey.Right:
                return 1;

            case MouseKey.Input_4:
                return 2;

            case MouseKey.Input_5:
                return 3;

            case MouseKey.Input_6:
                return 4;

            case MouseKey.Input_7:
                return 5;

            case MouseKey.Input_8:
                return 6;

            default:
                return 0;
        }
    }

    private MouseKey GetKeyCode(int index)
    {
        switch (index)
        {
            case 0:
                return MouseKey.Left;

            case 1:
                return MouseKey.Right;

            case 2:
                return MouseKey.Input_4;

            case 3:
                return MouseKey.Input_5;

            case 4:
                return MouseKey.Input_6;

            case 5:
                return MouseKey.Input_7;

            case 6:
                return MouseKey.Input_8;

            default:
                return MouseKey.Left;
        }
    }
}

/// <summary>
/// マウスのキーコード
/// </summary>
public enum MouseKey
{
    Left = DX.MOUSE_INPUT_LEFT,
    Right = DX.MOUSE_INPUT_RIGHT,
    Input_4 = DX.MOUSE_INPUT_4,
    Input_5 = DX.MOUSE_INPUT_5,
    Input_6 = DX.MOUSE_INPUT_6,
    Input_7 = DX.MOUSE_INPUT_7,
    Input_8 = DX.MOUSE_INPUT_8,
}