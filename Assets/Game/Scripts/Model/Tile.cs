public class Tile
{
    private TileState _tileState;

    private int _row;
    private int _column;
    private int _colorId;
    private int _connectionId;
   
    public Tile(int colorId, int row, int column)
    {
        _row = row;
        _column = column;
        _colorId = colorId;
        _connectionId = -1;//not connected
        _tileState = TileState.Instantiated;
    }

    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }
    public int ColorId { get => _colorId; set => _colorId = value; }
    public int ConnectionId { get => _connectionId; set => _connectionId = value; }
    public TileState TileState { get => _tileState; set => _tileState = value; }
}
