public class Board
{
    private Tile[,] _tiles;
    private GameRule _gameRule;

    public Board(GameRule gameRule, IColorSelector colorSelector)
    {
        _gameRule = gameRule;
        _tiles = new Tile[_gameRule.Rows, _gameRule.Columns];
        
        for(int i = 0; i < _gameRule.Rows; i++)
        {
            for(int j = 0; j < _gameRule.Columns; j++)
            {
                int colorId = colorSelector.SelectColor(_gameRule);
                _tiles[i, j] = new Tile(colorId, i,j);
            }
        }
    }

    public void ResetConnections()
    {
        for (int i = 0; i < _gameRule.Rows; i++)
        {
            for (int j = 0; j < _gameRule.Columns; j++)
            {
                _tiles[i, j].ConnectionId = -1;
            }
        }
    }

    public Tile[,] Tiles { get => _tiles; set => _tiles = value; }

    
}
