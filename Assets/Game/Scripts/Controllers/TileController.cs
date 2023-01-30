public class TileController
{
    private Tile _tile;

    public TileController(Tile tile)
    {
        _tile = tile;
    }
    
    public int GetColorId()
    {
        return _tile.ColorId;
    }
    public int GetSortingOrder()
    {
        return (20 - _tile.Row);
    }
    public string GetTierName(ITileMatching tileMatching, TierData[] tierData)
    {
        return tileMatching.GetMatchTierName(_tile.ConnectionId, tierData);
    }

    public void ResetTileProperties()
    {
        _tile.Blasted = false;
        _tile.Shifted = false;
    }

    public Tile Tile { get => _tile; set => _tile = value; }

}


