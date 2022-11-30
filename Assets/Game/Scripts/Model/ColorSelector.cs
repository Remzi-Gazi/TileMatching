using System;

public class ColorSelector : IColorSelector
{
    private Random _random;
    public ColorSelector()
    {
        _random = new Random();
    }

    public ColorSelector(int seed)
    {
        _random = new Random(seed);
    }
    
    public int SelectColor(GameRule gameRule)
    {
        return _random.Next(0, gameRule.ColorCount);
    }
}
