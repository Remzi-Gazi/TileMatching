public class Tile
{
    private int _row;
    private int _column;
    private int _colorId;
    private int _connectionId;

    private bool _blasted;
    private bool _shifted;
   
    public Tile(int colorId, int row, int column)
    {
        _row = row;
        _column = column;
        _colorId = colorId;
        _connectionId = -1;//not connected
        _blasted = true;
        _shifted = false;
    }

    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }
    public int ColorId { get => _colorId; set => _colorId = value; }
    public int ConnectionId { get => _connectionId; set => _connectionId = value; }
    public bool Blasted { get => _blasted; set => _blasted = value; }
    public bool Shifted { get => _shifted; set => _shifted = value; }

    
}
