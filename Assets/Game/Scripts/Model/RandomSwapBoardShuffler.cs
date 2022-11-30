using System;

public class RandomSwapBoardShuffler : IBoardShuffler
{
    private Random _random;

    public RandomSwapBoardShuffler()
    {
        _random = new Random();
    }

    public RandomSwapBoardShuffler(int seed)
    {
        _random = new Random(seed);
    }

    public void ShuffleBoard(Board board)
    {
        Tile tempTile;

        for(int i = 0; i < board.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < board.Tiles.GetLength(1); j++)
            {
                //Reset any connections
                board.Tiles[i, j].ConnectionId = -1;
                
                tempTile = board.Tiles[i, j];
                
                int randomRow = _random.Next(0, board.Tiles.GetLength(0));
                int randomColumn = _random.Next(0, board.Tiles.GetLength(1));

                //swap the tiles
                board.Tiles[i, j] = board.Tiles[randomRow, randomColumn];
                board.Tiles[i, j].Row = i;
                board.Tiles[i, j].Column = j;

                board.Tiles[randomRow, randomColumn] = tempTile;
                board.Tiles[randomRow, randomColumn].Row = randomRow;
                board.Tiles[randomRow, randomColumn].Column = randomColumn;

                board.Tiles[i, j].TileState = TileState.Shifted;
                board.Tiles[randomRow, randomColumn].TileState = TileState.Shifted;

            }
        }
    }
}
