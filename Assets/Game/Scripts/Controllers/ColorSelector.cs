using System;

public class ColorSelector : IColorSelector
{
    private Random _random;
    private int _colorCount;

    public ColorSelector(int colorCount)
    {
        _random = new Random();
        _colorCount = colorCount;
    }

    public int SelectColor()
    {
        return _random.Next(0, _colorCount);
    }

    public int ColorCount { get => _colorCount; set => _colorCount = value; }
}
