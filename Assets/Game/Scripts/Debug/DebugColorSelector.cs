using System;

[Serializable]
public class DebugColorSelector : IColorSelector
{
    private bool _repeatSelectedColor;
    private int _repeatCount;

    private int _returnedColorCounter;
    
    private Random _random;

    private int _selectedColor;

    public DebugColorSelector(bool repeatSelectedColor, int repeatCount)
    {
        _random = new Random();

        _repeatSelectedColor = repeatSelectedColor;
        _repeatCount = repeatCount;

        _returnedColorCounter = 0;
        _selectedColor = -1;
    }

    public DebugColorSelector()
    {
        _random = new Random();

        _repeatSelectedColor = false;
    }

    public DebugColorSelector(int seed, bool repeatSelectedColor, int repeatCount)
    {
        _random = new Random(seed);

        _repeatSelectedColor = repeatSelectedColor;
        _repeatCount = repeatCount;

        _returnedColorCounter = 0;
        _selectedColor = -1;
    }

    public int SelectColor(GameRule gameRule)
    {
        if (_repeatSelectedColor)
        {
            //first selection
            if (_selectedColor == -1)
            {
                _selectedColor = _random.Next(0, gameRule.ColorCount);
            }


            if (_returnedColorCounter < _repeatCount)
            {
                _returnedColorCounter++;
                return _selectedColor;
            }
            else
            {
                //return other than last selected color
                _returnedColorCounter = 1;
                //_selectedColor = (_selectedColor + _random.Next(1, gameRule.ColorCount - 1)) % gameRule.ColorCount;
                _selectedColor = _random.Next(0, gameRule.ColorCount);
                return _selectedColor;
            }
        }
        else
        {
            return _random.Next(0, gameRule.ColorCount);
        }
        
    }
}
