using UnityEngine;
using DG.Tweening;

//commented out we only need one asset
//[CreateAssetMenu]
public class Tweener : MonoBehaviour
{
    public GameRule GameRule;
    public TileData TileData;

    public Vector3 InitialTileSpawnPosition;

    public Vector3 TileReplacementHeight;

    public Vector3 BoardStartingPosition;

    public float TilePlacementDelay;

    public float TilePlacementDuration;

    //[HideInInspector]
    public Vector3[,] TilePlacementPositions;

    public void Awake()
    {
        DOTween.SetTweensCapacity(241,121);
        TilePlacementPositions = new Vector3[GameRule.Rows,GameRule.Columns];
        for(int i = 0; i < GameRule.Rows; i++)
        {
            for (int j = 0; j < GameRule.Columns; j++)
            {
                TilePlacementPositions[i, j] = CalculateFinalTilePositions(i,j);
            }
        }  
    }
     
    public Vector3 CalculateFinalTilePositions(int row, int column)
    {
        Vector3 placementPosition = BoardStartingPosition;

        int rowSize = GameRule.Rows;
        int columnSize = GameRule.Columns;

        //calculate the starting height of the rows
        placementPosition.y += TileData.TileSize.y / 2 + TileData.TileSize.y * (rowSize - 1 - row);

        //even number of columns
        if (columnSize % 2 == 0)
        {
            //calculate the starting position of the tiles in the case of even number of columns 
            placementPosition.x -= TileData.TileSize.x * (columnSize / 2 - 1) + TileData.TileSize.x / 2;
        }
        //odd number of columns
        else
        {
            //calculate the starting position of the tiles in the case of odd number of columns 
            placementPosition.x -= TileData.TileSize.x * (columnSize / 2 - 1);
        }

        placementPosition.x += TileData.TileSize.x * column;
        //placementPosition.y += tileData.TileSize.y * (rowSize - 1 - row);

        return placementPosition;
    }


    public void PlaceTileInitially(Transform tileTransform, int tileRow, int tileColumn)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(tileTransform.DOMove(TilePlacementPositions[tileRow, tileColumn], TilePlacementDuration));
        sequence.Append(tileTransform.DOShakeScale(0.3f, 0.2f));
        //sequence.SetDelay(RuntimeTileReplacementDelay);
        //RuntimeTileReplacementDelay += TilePlacementDelay;
    }

    public void ShiftTile(Transform tileTransform, int tileRow, int tileColumn)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(tileTransform.DOMove(TilePlacementPositions[tileRow, tileColumn], 0.3f));
        sequence.Append(tileTransform.DOShakeScale(0.3f, 0.2f));
    }
    






}
