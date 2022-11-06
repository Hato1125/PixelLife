namespace PixelLife.Model;

internal class TitleModel
{
    private double _fade;
    public bool FadeFlag;
    public double FadeValue;

    public void Init()
    {
        _fade = 0;
        FadeFlag = false;
        FadeValue = 0;
    }

    public void Fade()
    {
        if (!FadeFlag)
            return;

        _fade += AppMain.Time.TotalSeconds * 60;

        if (_fade > 90)
            _fade = 90;

        FadeValue = Math.Sin(_fade * Math.PI / 180) * 255;
    }
}