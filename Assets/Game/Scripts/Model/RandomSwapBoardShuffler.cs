using System;

public class RandomSwapBoardShuffler : IBoardShuffler
{
    private Random _random;

    public RandomSwapBoardShuffler()
    {
        _random = new Random();
    }

    public void ShuffleBoard(Tile[,] tiles)
    {
        Tile tempTile;

        for(int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                //Reset any connections
                tiles[i, j].ConnectionId = -1;
                
                tempTile = tiles[i, j];
                
                int randomRow = _random.Next(0, tiles.GetLength(0));
                int randomColumn = _random.Next(0, tiles.GetLength(1));

                //swap the tiles
                tiles[i, j] = tiles[randomRow, randomColumn];
                tiles[i, j].Row = i;
                tiles[i, j].Column = j;

                tiles[randomRow, randomColumn] = tempTile;
                tiles[randomRow, randomColumn].Row = randomRow;
                tiles[randomRow, randomColumn].Column = randomColumn;

                tiles[i, j].TileState = TileState.Shifted;
                tiles[randomRow, randomColumn].TileState = TileState.Shifted;

            }
        }
    }
}
