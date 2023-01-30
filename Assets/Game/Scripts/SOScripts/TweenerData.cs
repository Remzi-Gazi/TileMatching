using UnityEngine;

[CreateAssetMenu(fileName = "TweenerData", menuName = "Scriptable Objects/Tweener Data")]
public class TweenerData : ScriptableObject
{
    [HideInInspector] public Vector3[,] TilePlacementPositions;

    public Vector3 InitialTileSpawnPosition;
    public Vector3 TileReplacementHeight;
    public Vector3 BoardStartingPosition;
    public Vector2 TileSize;

    public float TilePlacementDelay;
    public float TilePlacementDuration;

    public GameRule GameRule;

#if UNITY_EDITOR
    private void OnValidate()
    {
        CalculateTilePositions();
    }

    private void CalculateTilePositions()
    {
        //DOTween.SetTweensCapacity(300, 150);
        TilePlacementPositions = new Vector3[GameRule.Rows, GameRule.Columns];
        for (int i = 0; i < GameRule.Rows; i++)
        {
            for (int j = 0; j < GameRule.Columns; j++)
            {
                TilePlacementPositions[i, j] = CalculateFinalTilePosition(i, j);
            }
        }
    }

    private Vector3 CalculateFinalTilePosition(int row, int column)
    {
        Vector3 placementPosition = BoardStartingPosition;

        int rowSize = GameRule.Rows;
        int columnSize = GameRule.Columns;

        //calculate the starting height of the rows
        placementPosition.y += TileSize.y / 2 + TileSize.y * (rowSize - 1 - row);

        //even number of columns
        if (columnSize % 2 == 0)
        {
            //calculate the starting position of the tiles in the case of even number of columns 
            placementPosition.x -= TileSize.x * (columnSize / 2 - 1) + TileSize.x / 2;
        }
        //odd number of columns
        else
        {
            //calculate the starting position of the tiles in the case of odd number of columns 
            placementPosition.x -= TileSize.x * (columnSize / 2 - 1);
        }
        placementPosition.x += TileSize.x * column;

        return placementPosition;
    }
#endif

}