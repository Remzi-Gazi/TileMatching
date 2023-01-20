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

    public DebugColorSelector(int seed, bool repeatSelectedColor, int repeatCount)
    {
        _random = new Random(seed);

        _repeatSelectedColor = repeatSelectedColor;
        _repeatCount = repeatCount;

        _returnedColorCounter = 0;
        _selectedColor = -1;
    }

    public int SelectColor(int colorCount)
    {
        if (_repeatSelectedColor)
        {
            //first selection
            if (_selectedColor == -1)
            {
                _selectedColor = _random.Next(0, colorCount);
            }


            if (_returnedColorCounter < _repeatCount)
            {
                _returnedColorCounter++;
                return _selectedColor;
            }
            else
            {
                
                _returnedColorCounter = 1;
                //return other than last selected color
                //_selectedColor = (_selectedColor + _random.Next(1, gameRule.ColorCount - 1)) % gameRule.ColorCount;
                _selectedColor = _random.Next(0, colorCount);
                return _selectedColor;
            }
        }
        else
        {
            return _random.Next(0, colorCount);
        }
        
    }
}
