public class Board
{
    private Tile[,] _tiles;

    public Board(GameRule gameRule, IColorSelector colorSelector)
    {
        _tiles = new Tile[gameRule.Rows, gameRule.Columns];
        
        for(int i = 0; i < gameRule.Rows; i++)
        {
            for(int j = 0; j < gameRule.Columns; j++)
            {
                int colorId = colorSelector.SelectColor(gameRule.ColorCount);
                _tiles[i, j] = new Tile(colorId, i,j);
            }
        }
    }

    public void ResetConnections()
    {
        for (int i = 0; i < _tiles.GetLength(0); i++)
        {
            for (int j = 0; j < _tiles.GetLength(1); j++)
            {
                _tiles[i, j].ConnectionId = -1;
            }
        }
    }

    public Tile[,] Tiles { get => _tiles; set => _tiles = value; }

    
}
