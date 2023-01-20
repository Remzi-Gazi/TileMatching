using System.Collections.Generic;

public class BlastHandler
{
    public void BlastTiles(Tile[,] tiles, List<Tile> blastedTiles)
    {
        for (int i = 0; i < blastedTiles.Count; i++)
        {
            blastedTiles[i].TileState = TileState.Blasted;
            tiles[blastedTiles[i].Row, blastedTiles[i].Column] = null;
        }
    }

    public HashSet<int> FindBlastedColumns(List<Tile> blastedTiles)
    {
        HashSet<int> uniqueColumns = new HashSet<int>();

        for (int i = 0; i < blastedTiles.Count; i++)
        {
            uniqueColumns.Add(blastedTiles[i].Column);
        }

        return uniqueColumns;
    }

    public void ShiftTilesAfterBlast(Tile[,] tiles, HashSet<int> uniqueColumns)
    {
        //start searching on blasted columns for empty locations from the bottom of the board
        foreach (int column in  uniqueColumns)
        {
            for (int j = tiles.GetLength(0) - 1; j >= 0; j--)
            {
                if (tiles[j, column] == null)
                {
                    //Find the first non empty tile and swap it
                    for (int k = j; k >= 0; k--)
                    {
                        if (tiles[k, column] != null)
                        {
                            tiles[j, column] = tiles[k, column];
                            tiles[k, column] = null;
                            tiles[j, column].Row = j;
                            tiles[j, column].TileState = TileState.Shifted;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void RespawnBlastedTiles(Tile[,] tiles, List<Tile> blastedTiles, int colorCount, IColorSelector colorSelector)
    {
        //search for empty locations starting from top of the board
        int tileCount = 0;
        for (int j = 0; j < tiles.GetLength(1); j++)
        {
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                if (tiles[i, j] != null)
                {
                    break;
                }

                //if empty pick one blasted tile and reuse and reset values of it
                Tile respawnedTile = blastedTiles[tileCount];
                tileCount++;

                //reset values
                respawnedTile.Row = i;
                respawnedTile.Column = j;
                respawnedTile.ColorId = colorSelector.SelectColor(colorCount);
                tiles[i, j] = respawnedTile;
                tiles[i, j].TileState = TileState.Respawned;

            }
        }
    }
}
