using UnityEngine;
using DG.Tweening;

public class Tweener : MonoBehaviour
{
    [SerializeField] private Vector3 _initialTileSpawnPosition;

    [SerializeField] private Vector3 _tileReplacementHeight;

    [SerializeField] private Vector3 _boardStartingPosition;

    [SerializeField] private float _tilePlacementDelay;

    [SerializeField] private float _tilePlacementDuration;

    private Vector3[,] _tilePlacementPositions;

    private GameRule _gameRule;
    private TileData _tileData;

    public void InitializeTweener(GameRule gameRule, TileData tileData)
    {
        DOTween.SetTweensCapacity(300, 150);
        _gameRule = gameRule;
        _tileData = tileData;

        _tilePlacementPositions = new Vector3[_gameRule.Rows, _gameRule.Columns];
        for (int i = 0; i < _gameRule.Rows; i++)
        {
            for (int j = 0; j < _gameRule.Columns; j++)
            {
                _tilePlacementPositions[i, j] = CalculateFinalTilePositions(i, j);
            }
        }
    }
     
    public Vector3 CalculateFinalTilePositions(int row, int column)
    {
        Vector3 placementPosition = _boardStartingPosition;

        int rowSize = _gameRule.Rows;
        int columnSize = _gameRule.Columns;

        //calculate the starting height of the rows
        placementPosition.y += _tileData.TileSize.y / 2 + _tileData.TileSize.y * (rowSize - 1 - row);

        //even number of columns
        if (columnSize % 2 == 0)
        {
            //calculate the starting position of the tiles in the case of even number of columns 
            placementPosition.x -= _tileData.TileSize.x * (columnSize / 2 - 1) + _tileData.TileSize.x / 2;
        }
        //odd number of columns
        else
        {
            //calculate the starting position of the tiles in the case of odd number of columns 
            placementPosition.x -= _tileData.TileSize.x * (columnSize / 2 - 1);
        }

        placementPosition.x += _tileData.TileSize.x * column;
        //placementPosition.y += tileData.TileSize.y * (rowSize - 1 - row);

        return placementPosition;
    }


    public void PlaceTileInitially(Transform tileTransform, int tileRow, int tileColumn)
    {
        tileTransform.position = _initialTileSpawnPosition;

        Sequence sequence = DOTween.Sequence();
        TilePlacementSequence(sequence, tileTransform, tileRow, tileColumn);
        sequence.SetDelay((tileRow*_gameRule.Columns + tileColumn) * _tilePlacementDelay);
    }

    public void ShiftRemainingTile(Transform tileTransform, int tileRow, int tileColumn)
    {
        Sequence sequence = DOTween.Sequence();
        TilePlacementSequence(sequence, tileTransform,tileRow,tileColumn);
    }

    public void ShiftRespawnedTile(Transform tileTransform, int tileRow, int tileColumn)
    {
        tileTransform.position = _tilePlacementPositions[tileRow, tileColumn] + _tileReplacementHeight;

        Sequence sequence = DOTween.Sequence();
        TilePlacementSequence(sequence, tileTransform, tileRow, tileColumn);
    }
    
    public void TilePlacementSequence(Sequence sequence, Transform tileTransform, int tileRow, int tileColumn)
    {
        sequence.Append(tileTransform.DOMove(_tilePlacementPositions[tileRow, tileColumn], _tilePlacementDuration));
        sequence.Append(tileTransform.DOShakeScale(0.3f, 0.2f));
    }
    






}
