using System.Collections.Generic;

public class BoardController
{
    private Board _board;
    private ITileMatching _tileMatching;
    private BlastHandler _blastHandler;
    private IColorSelector _colorSelector;

    public BoardController(GameRule gameRule)
    {
        _colorSelector = new ColorSelector(gameRule.ColorCount);
        _board = new Board(_colorSelector, gameRule);
        _tileMatching = new TileMatchingTable();
        _blastHandler = new BlastHandler(_colorSelector);
    }

    public void MatchTiles()
    {
        _tileMatching.MatchTiles(_board.Tiles);
    }

    public void AttachTilesToViews(TileView[,] tileViews)
    {
        for (int i = 0; i < tileViews.GetLength(0); i++)
        {
            for (int j = 0; j < tileViews.GetLength(1); j++)
            {
                tileViews[i, j].SetTile(_board.Tiles[i, j]);
            }
        }
    }

    public void InteractWithTile(int connectionId)
    {
        if (connectionId != -1)
        {
            List<Tile> blastedTiles = _tileMatching.GetMatchingTiles(connectionId);
            _blastHandler.BlastTiles(_board.Tiles, blastedTiles);

            _blastHandler.ShiftTiles(_board.Tiles);
            _blastHandler.RespawnBlastedTiles(_board.Tiles, blastedTiles);

            _tileMatching.ClearOldMatches(_board.Tiles);
            _tileMatching.MatchTiles(_board.Tiles);
        }
    }

    
    
    public ITileMatching TileMatching { get => _tileMatching; set => _tileMatching = value; }
}