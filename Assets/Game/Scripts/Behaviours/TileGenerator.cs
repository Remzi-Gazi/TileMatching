using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject _emptyTile;

    public TileController[,] GenerateTiles(Board board, GameRule gameRule, TileData tileData, ITileMatching tileMatching, Tweener tweener)
    {
        TileController[,] tileControllers = new TileController[gameRule.Rows, gameRule.Columns];

        for (int i = 0; i < gameRule.Rows; i++)
        {
            for (int j = 0; j < gameRule.Columns; j++)
            {
                tileControllers[i, j] = Instantiate(_emptyTile).GetComponent<TileController>();
                tileControllers[i, j].Tile = board.Tiles[i, j];
                tileControllers[i, j].Tweener = tweener;
                tileControllers[i, j].UpdateSprite(tileMatching, gameRule, tileData);
                tileControllers[i, j].UpdateTile(i*gameRule.Columns+j);
            }
        }

        return tileControllers;
    }
}