using System.Collections.Generic;
using UnityEngine;

public class BoardController : MonoBehaviour
{
    [SerializeField]
    private GameRule _gameRule;
    [SerializeField]
    private TileData _tileData;
    
    private Board _board;
    private TileController[,] _tileControllers;

    private IColorSelector _colorSelector;
    private ITileMatching _tileMatching;
    private IBoardShuffler _boardShuffler;
    private BlastHandler _blastHandler;

    //components
    private TileGenerator _tileGenerator;
    private Tweener _tweener;

    private void Awake()
    {
        _tileGenerator = GetComponent<TileGenerator>();
        _tweener = GetComponent<Tweener>();
        _tweener.InitializeTweener(_gameRule,_tileData);

        _colorSelector = new ColorSelector();
        _boardShuffler = new RandomSwapBoardShuffler();
        _tileMatching = new TileMatchingTable(_boardShuffler);
        _blastHandler = new BlastHandler();
        

        _board = new Board(_gameRule, _colorSelector);
        _tileMatching.MatchTiles(_board);

        _tileControllers = _tileGenerator.GenerateTiles(_board, _gameRule, _tileData, _tileMatching, _tweener);
    }

    public void InteractWithTile(TileController tileController)
    {
        if (tileController.Tile.ConnectionId != -1)
        {
            List<Tile> matchingTiles = _tileMatching.GetMatchingTiles(tileController.Tile);
            _blastHandler.BlastTiles(_board, matchingTiles);
            HashSet<int> uniqueColumns = _blastHandler.FindBlastedColumns(matchingTiles);

            _blastHandler.ShiftTilesAfterBlast(_board, uniqueColumns);
            _blastHandler.RespawnBlastedTiles(_board, matchingTiles, _gameRule.ColorCount, _colorSelector);

            _board.ResetConnections();
            _tileMatching.MatchTiles(_board);


            for (int i = 0; i < _gameRule.Columns; i++)
            {
                //bottom to top
                for (int j = _gameRule.Rows - 1; j >= 0; j--)
                {
                    _tileControllers[j, i].UpdateSprite(_tileMatching, _gameRule, _tileData);
                    _tileControllers[j, i].UpdateTile();
                }
            }
        }
    }
    
}
