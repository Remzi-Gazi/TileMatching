using System;

public class ColorSelector : IColorSelector
{
    private Random _random;

    public ColorSelector()
    {
        _random = new Random();
    }

    public int SelectColor(int colorCount)
    {
        return _random.Next(0, colorCount);
    }
}
