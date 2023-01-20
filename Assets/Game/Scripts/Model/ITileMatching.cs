using System.Collections.Generic;

public interface ITileMatching
{
    void MatchTiles(Tile[,] tile);

    int GetMatchTierIndex(int connectionId, TierData[] tierData);

    List<Tile> GetMatchingTiles(int connectionId);
}
