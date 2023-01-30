using System.Collections.Generic;

public class BlastHandler
{
    private IColorSelector _colorSelector;

    public BlastHandler(IColorSelector colorSelector)
    {
        _colorSelector = colorSelector;
    }

    public void BlastTiles(Tile[,] tiles, List<Tile> blastedTiles)
    {
        for (int i = 0; i < blastedTiles.Count; i++)
        {
            tiles[blastedTiles[i].Row, blastedTiles[i].Column] = null;
        }
    }

    public void ShiftTiles(Tile[,] tiles)
    {
        //start searching on blasted columns for empty locations from the bottom of the board
        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            int swapRow = -1;
            for (int j = tiles.GetLength(0) - 1; j >= 0; j--)
            {
                if (swapRow != -1 && tiles[j, i] != null)
                {
                    tiles[swapRow, i] = tiles[j, i];
                    tiles[swapRow, i].Row = swapRow;
                    tiles[swapRow, i].Shifted = true;
                    tiles[j, i] = null;
                    swapRow--;
                }
                else if (swapRow == -1 && tiles[j, i] == null)
                {
                    swapRow = j;
                }
            }
        }
    }

    public void RespawnBlastedTiles(Tile[,] tiles, List<Tile> blastedTiles)
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
                respawnedTile.ColorId = _colorSelector.SelectColor();
                tiles[i, j] = respawnedTile;
                tiles[i, j].Blasted = true;
                tiles[i, j].Shifted = true;
            }
        }
    }
}