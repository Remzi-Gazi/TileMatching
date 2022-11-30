using System.Collections.Generic;
using UnityEngine;

public class BlastHandler
{
    public void BlastTiles(Board board ,List<Tile> blastedTiles)
    {
        for (int i = 0; i < blastedTiles.Count; i++)
        {
            blastedTiles[i].TileState = TileState.Blasted;
            board.Tiles[blastedTiles[i].Row, blastedTiles[i].Column] = null;
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

    public void ShiftTilesAfterBlast(Board board, List<Tile> blastedTiles, HashSet<int> uniqueColumns)
    {
        //start searching on blasted columns for empty locations from the bottom of the board
        foreach (int column in uniqueColumns)
        {
            for (int j = board.Tiles.GetLength(0) - 1; j >= 0; j--)
            {
                if (board.Tiles[j, column] == null)
                {
                    //Find the first non empty tile and swap it
                    for (int k = j; k >= 0; k--)
                    {
                        if (board.Tiles[k, column] != null)
                        {
                            board.Tiles[j, column] = board.Tiles[k, column];
                            board.Tiles[k, column] = null;
                            board.Tiles[j, column].Row = j;
                            board.Tiles[j, column].TileState = TileState.Shifted;
                            break;
                        }
                    }
                }
            }
        }
    }

    public void RespawnBlastedTiles(Board board, List<Tile> blastedTiles, GameRule gameRule, IColorSelector colorSelector)
    {
        //search for empty locations starting from top of the board
        int tileCount = 0;
        for (int j = 0; j < board.Tiles.GetLength(1); j++)
        {
            for (int i = 0; i < board.Tiles.GetLength(0); i++)
            {
                if (board.Tiles[i, j] != null)
                {
                    break;
                }

                //if empty pick one blasted tile and reuse and reset values of it
                Tile respawnedTile = blastedTiles[tileCount];
                tileCount++;

                //reset values
                respawnedTile.Row = i;
                respawnedTile.Column = j;
                respawnedTile.ColorId = colorSelector.SelectColor(gameRule);
                board.Tiles[i, j] = respawnedTile;
                board.Tiles[i, j].TileState = TileState.Respawned;

            }
        }
    }
}
