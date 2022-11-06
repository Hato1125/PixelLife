using PixelLife.Gui;

namespace PixelLife.View;

internal class UiView
{
    private Button[] _buttons = new Button[3];
    private readonly string[] _btnTexts = { "Play", "Setting", "Exit" };
    private readonly string[] _btnIcons = { "🎮", "⚙️", "👋" };
    public GuiAlignment alignment = new();

    public void Init()
    {
        alignment.Interval = 10;
        alignment.AlignmentType = AlignmentType.Vertical;
        alignment.X = (AppInfo.CliantSize.Width - 270) / 2;
        alignment.Y = 700;
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i] = new(280, 55, "Segoe UI", 24, 5);
            _buttons[i].BackColor = Color.FromArgb(10, 10, 10);
            _buttons[i].EdgeColor = Color.FromArgb(0, 0, 0);
            _buttons[i].ForeColor = Color.FromArgb(255, 255, 255);
            _buttons[i].BlurOpacity = 20;
            _buttons[i].EdgeOpacity = 50;
            _buttons[i].Text = _btnTexts[i];
            _buttons[i].SetIcon(_btnIcons[i], "Segoe UI Emoji", 25, 5);
            alignment.Controls.Add(_buttons[i]);
        }
    }

    public void Update()
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].Delta = AppMain.Time.TotalSeconds;

        alignment.Update();
    }

    public void Draw()
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].Draw();
    }
}