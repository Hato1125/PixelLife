using System.Numerics;

namespace PixelLife.Logic;

internal class LogoPixel
{
    private const int Y_MIN = 50;
    private const int Y_MAX = 200;
    private const int SPEED_MIN = 35;
    private const int SPEED_MAX = 50;
    private const int XORIGIN_MAX = 500;
    private const int COOLTIME_MAX = 7;
    private double _ctOpacity;
    private double _ctCoolTime;
    private double _coolTime;
    private double _yMax;
    private int _speed;
    private PixelDirection _xDirection;
    private PixelDirection _yDirection;
    public double X { get; private set; }
    public double Y { get; private set; }
    public double Xorigin { get; private set; }
    public double Opacity { get; private set; }

    public void Init()
    {
        X = 0;
        Y = 0;
        Xorigin = new Random().Next(0, XORIGIN_MAX);
        _coolTime = new Random().Next(0, COOLTIME_MAX);
        _yMax = new Random().Next(Y_MIN, Y_MAX);
        _speed = new Random().Next(SPEED_MIN, SPEED_MAX);
        _ctCoolTime = 0;

        int direRand_x = new Random().Next(0, 2);
        if (direRand_x == 0)
            _xDirection = PixelDirection.Plus;
        else
            _xDirection = PixelDirection.Minus;

        int direRand_y = new Random().Next(0, 2);
        if (direRand_y == 0)
            _yDirection = PixelDirection.Plus;
        else
            _yDirection = PixelDirection.Minus;
    }

    public void CalMove()
    {
        _ctCoolTime += AppMain.Time.TotalSeconds;

        if (_ctCoolTime <= _coolTime)
            return;

        if (Math.Abs(Y) >= _yMax)
        {
            Init();
            return;
        }

        double value = (Math.Sqrt(2) * _speed) * AppMain.Time.TotalSeconds;

        if (_xDirection == PixelDirection.Plus)
            X += value;
        else
            X -= value;

        if (_yDirection == PixelDirection.Plus)
            Y += value;
        else
            Y -= value;

            Console.WriteLine($"X:{X}, Y:{Y}");

        _ctOpacity = Y * (180.0 / _yMax);
        Opacity = Math.Sin(Math.Abs(_ctOpacity) * Math.PI / 180) * 255;
    }
}

internal enum PixelDirection
{
    Plus,
    Minus,
}