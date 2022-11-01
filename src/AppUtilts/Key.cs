namespace PixelLife;

internal class Key
{
    private readonly byte[] buffer = new byte[256];
    private readonly sbyte[] value = new sbyte[256];

    /// <summary>
    /// キーの文字を取得する (大文字)
    /// </summary>
    public readonly Dictionary<int, string> KeyString = new()
    {
        {DX.KEY_INPUT_A, "A"},
        {DX.KEY_INPUT_B, "B"},
        {DX.KEY_INPUT_C, "C"},
        {DX.KEY_INPUT_D, "D"},
        {DX.KEY_INPUT_E, "E"},
        {DX.KEY_INPUT_F, "F"},
        {DX.KEY_INPUT_G, "G"},
        {DX.KEY_INPUT_H, "H"},
        {DX.KEY_INPUT_I, "I"},
        {DX.KEY_INPUT_J, "J"},
        {DX.KEY_INPUT_K, "K"},
        {DX.KEY_INPUT_L, "L"},
        {DX.KEY_INPUT_M, "M"},
        {DX.KEY_INPUT_N, "N"},
        {DX.KEY_INPUT_O, "O"},
        {DX.KEY_INPUT_P, "P"},
        {DX.KEY_INPUT_Q, "Q"},
        {DX.KEY_INPUT_R, "R"},
        {DX.KEY_INPUT_S, "S"},
        {DX.KEY_INPUT_T, "T"},
        {DX.KEY_INPUT_U, "U"},
        {DX.KEY_INPUT_V, "V"},
        {DX.KEY_INPUT_W, "W"},
        {DX.KEY_INPUT_X, "X"},
        {DX.KEY_INPUT_Y, "Y"},
        {DX.KEY_INPUT_Z, "Z"},
        {DX.KEY_INPUT_BACK, "Back"},
        {DX.KEY_INPUT_TAB, "Tab"},
        {DX.KEY_INPUT_RETURN, "Return"},
        {DX.KEY_INPUT_LSHIFT, "LeftShift"},
        {DX.KEY_INPUT_RSHIFT, "RightShift"},
        {DX.KEY_INPUT_LCONTROL, "LeftControl"},
        {DX.KEY_INPUT_RCONTROL, "RightControl"},
        {DX.KEY_INPUT_ESCAPE, "Escape"},
        {DX.KEY_INPUT_SPACE, "Space"},
        {DX.KEY_INPUT_PGUP, "PageUp"},
        {DX.KEY_INPUT_PGDN, "PageDown"},
        {DX.KEY_INPUT_END, "End"},
        {DX.KEY_INPUT_HOME, "Home"},
        {DX.KEY_INPUT_LEFT, "Left"},
        {DX.KEY_INPUT_RIGHT, "Right"},
        {DX.KEY_INPUT_UP, "Up"},
        {DX.KEY_INPUT_DOWN, "Down"},
        {DX.KEY_INPUT_INSERT, "Insert"},
        {DX.KEY_INPUT_DELETE, "Delete"},
        {DX.KEY_INPUT_MINUS, "Minus"},
        {DX.KEY_INPUT_YEN, "Yen"},
        {DX.KEY_INPUT_PREVTRACK, "Prevtrack"},
        {DX.KEY_INPUT_PERIOD, "Period"},
        {DX.KEY_INPUT_SLASH, "Slash"},
        {DX.KEY_INPUT_LALT, "LeftAlt"},
        {DX.KEY_INPUT_RALT, "RightAlt"},
        {DX.KEY_INPUT_SCROLL, "Scroll"},
        {DX.KEY_INPUT_SEMICOLON, "Semicolon"},
        {DX.KEY_INPUT_COLON, "Colon"},
        {DX.KEY_INPUT_LBRACKET, "LeftBracket"},
        {DX.KEY_INPUT_RBRACKET, "RightBracket"},
        {DX.KEY_INPUT_AT, "At"},
        {DX.KEY_INPUT_BACKSLASH, "BackSlash"},
        {DX.KEY_INPUT_COMMA, "Comma"},
        {DX.KEY_INPUT_KANJI, "Kanji"},
        {DX.KEY_INPUT_CONVERT, "Convert"},
        {DX.KEY_INPUT_NOCONVERT, "NoConvert"},
        {DX.KEY_INPUT_KANA, "Kana"},
        {DX.KEY_INPUT_APPS, "Apps"},
        {DX.KEY_INPUT_CAPSLOCK, "CapsLook"},
        {DX.KEY_INPUT_SYSRQ, "Sysrq"},
        {DX.KEY_INPUT_PAUSE, "Pause"},
        {DX.KEY_INPUT_LWIN, "LeftWin"},
        {DX.KEY_INPUT_RWIN, "RightWin"},
        {DX.KEY_INPUT_NUMLOCK, "NumLook"},
        {DX.KEY_INPUT_NUMPAD0, "Num0"},
        {DX.KEY_INPUT_NUMPAD1, "Num1"},
        {DX.KEY_INPUT_NUMPAD2, "Num2"},
        {DX.KEY_INPUT_NUMPAD3, "Num3"},
        {DX.KEY_INPUT_NUMPAD4, "Num4"},
        {DX.KEY_INPUT_NUMPAD5, "Num5"},
        {DX.KEY_INPUT_NUMPAD6, "Num6"},
        {DX.KEY_INPUT_NUMPAD7, "Num7"},
        {DX.KEY_INPUT_NUMPAD8, "Num8"},
        {DX.KEY_INPUT_NUMPAD9, "Num9"},
        {DX.KEY_INPUT_ADD, "Add"},
        {DX.KEY_INPUT_SUBTRACT, "SubTract"},
        {DX.KEY_INPUT_DECIMAL, "Decimal"},
        {DX.KEY_INPUT_DIVIDE, "Divide"},
        {DX.KEY_INPUT_NUMPADENTER, "NumPadenter"},
        {DX.KEY_INPUT_F1, "F1"},
        {DX.KEY_INPUT_F2, "F2"},
        {DX.KEY_INPUT_F3, "F3"},
        {DX.KEY_INPUT_F4, "F4"},
        {DX.KEY_INPUT_F5, "F5"},
        {DX.KEY_INPUT_F6, "F6"},
        {DX.KEY_INPUT_F7, "F7"},
        {DX.KEY_INPUT_F8, "F8"},
        {DX.KEY_INPUT_F9, "F9"},
        {DX.KEY_INPUT_F10, "F10"},
        {DX.KEY_INPUT_F11, "F11"},
        {DX.KEY_INPUT_F12, "F12"},
        {DX.KEY_INPUT_0, "0"},
        {DX.KEY_INPUT_1, "1"},
        {DX.KEY_INPUT_2, "2"},
        {DX.KEY_INPUT_3, "3"},
        {DX.KEY_INPUT_4, "4"},
        {DX.KEY_INPUT_5, "5"},
        {DX.KEY_INPUT_6, "6"},
        {DX.KEY_INPUT_7, "7"},
        {DX.KEY_INPUT_8, "8"},
        {DX.KEY_INPUT_9, "9"},
    };

    /// <summary>
    /// 更新
    /// </summary>
    public void Update()
    {
        DX.GetHitKeyStateAll(buffer);

        for (int i = 0; i < buffer.Length; i++)
        {
            if (buffer[i] == 1)
            {
                if (!IsPush(i))
                    value[i] = 1;
                else
                    value[i] = 2;
            }
            else
            {
                if (IsPush(i))
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
    public bool IsPush(int keyCode)
        => value[keyCode] > 0;

    /// <summary>
    /// キーを押した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    public bool IsPushed(int keyCode)
        => value[keyCode] == 1;

    /// <summary>
    /// キーを離した瞬間を取得する
    /// </summary>
    /// <param name="keyCode">キーコード</param>
    public bool IsUped(int keyCode)
        => value[keyCode] == -1;
}