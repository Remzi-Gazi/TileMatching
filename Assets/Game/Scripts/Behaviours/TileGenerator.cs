using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    [SerializeField] private GameObject _emptyTile;

    public TileView[,] GenerateTiles(GameRule gameRule)
    {
        TileView[,] tileViews = new TileView[gameRule.Rows, gameRule.Columns];

        for (int i = 0; i < gameRule.Rows; i++)
        {
            for (int j = 0; j < gameRule.Columns; j++)
            {
                tileViews[i, j] = Instantiate(_emptyTile).GetComponent<TileView>();
            }
        }
        return tileViews;
    }
}