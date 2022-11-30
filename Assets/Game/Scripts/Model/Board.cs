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
                int colorId = colorSelector.SelectColor(gameRule);
                _tiles[i, j] = new Tile(colorId, i,j);
            }
        }
    }

    public Tile[,] Tiles { get => _tiles; set => _tiles = value; }

}
