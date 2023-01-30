using System;
using UnityEngine;

public class BoardView : MonoBehaviour
{
    [SerializeField]
    private GameRule _gameRule;
    [SerializeField]
    private TileData _tileData;

    private BoardController _boardController;

    //components
    private TileGenerator _tileGenerator;

    //Views
    private TileView[,] _tileViews;

    private void Awake()
    {
        _boardController = new BoardController(_gameRule);

        _tileGenerator = GetComponent<TileGenerator>();
        
        _tileViews = _tileGenerator.GenerateTiles(_gameRule);

        _boardController.AttachTilesToViews(_tileViews);
        
        _boardController.MatchTiles();
        
        UpdateAllTiles();
    }

    private void UpdateAllTiles()
    {
        for (int i = 0; i < _gameRule.Rows; i++)
        {
            for (int j = 0; j < _gameRule.Columns; j++)
            {
                _tileViews[i, j].UpdateView(_tileData, _boardController.TileMatching);
            }
        }
    }

    private void OnEnable()
    {
        TileView.OnClick += InteractWithTile;
    }

    private void OnDisable()
    {
        TileView.OnClick -= InteractWithTile;
    }

    public void InteractWithTile(int connectionId)
    {
        _boardController.InteractWithTile(connectionId);
        UpdateAllTiles();
    }
}
