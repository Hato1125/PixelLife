namespace PixelLife.Model;

internal class UiModel
{
    public readonly string[] SceneName = {
        "Select", "Setting", "Exit"
    };

    public void ChangeScene(int index)
    {
        AppMain.Scene.ChangeScene(SceneName[index]);
    }
}