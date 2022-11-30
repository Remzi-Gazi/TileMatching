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

    public Tile(int row, int column)
    {
        _row = row;
        _column = column;
        _colorId = 0;//default color
        _connectionId = -1;
        _tileState = TileState.Instantiated;
    }

    public Tile(Tile tile)
    {
        _row = tile.Row;
        _column = tile.Column;
        _colorId = tile.ColorId;
        _connectionId = tile._connectionId;
        _tileState = TileState.Instantiated;
    }

    public int Row { get => _row; set => _row = value; }
    public int Column { get => _column; set => _column = value; }
    public int ColorId { get => _colorId; set => _colorId = value; }
    public int ConnectionId { get => _connectionId; set => _connectionId = value; }
    public TileState TileState { get => _tileState; set => _tileState = value; }
}
