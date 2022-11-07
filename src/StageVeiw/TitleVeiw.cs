using PixelLife.Gui;

namespace PixelLife.Veiw;

internal class TitleVeiw : IVeiw
{
    private const int GUI_WIDTH = 295;
    private const int GUI_HEIGHT = 65;

    private Button[] _buttons = new Button[3];
    private string[] _texts = new string[3] { "Play!", "Setting", "Exit" };
    private string[] _icons = new string[3] { "🎮", "⚙️", "👋" };
    public GuiAlignment Alignment = new();

    public void Init()
    {
        Alignment.AlignmentType = AlignmentType.Vertical;
        Alignment.Interval = 10;
        Alignment.X = (AppInfo.CliantSize.Width - GUI_WIDTH) / 2;
        Alignment.Y = 700;
        for (int i = 0; i < _buttons.Length; i++)
        {
            _buttons[i] = new(GUI_WIDTH, GUI_HEIGHT, "Segoe UI", 29, 5);
            _buttons[i].BackColor = Color.FromArgb(10, 10, 10);
            _buttons[i].EdgeColor = Color.FromArgb(0, 0, 0);
            _buttons[i].ForeColor = Color.FromArgb(255, 255, 255);
            _buttons[i].Text = _texts[i];
            _buttons[i].BlurOpacity = 20;
            _buttons[i].EdgeOpacity = 50;
            _buttons[i].SetIcon(_icons[i], "Segoe UI Emoji", 29, 5);
            Alignment.Controls.Add(_buttons[i]);
        }
    }

    public void Update()
    {
        for (int i = 0; i < _buttons.Length; i++)
            _buttons[i].Delta = AppMain.Time.TotalSeconds;
        Alignment.Update();
    }

    public void Draw()
    {
        Alignment.Draw();
    }
}