using System.Collections.Generic;
public interface ITileMatching
{
    void MatchTiles(Tile[,] tile);

    string GetMatchTierName(int connectionId, TierData[] tierData);

    List<Tile> GetMatchingTiles(int connectionId);

    void ClearOldMatches(Tile[,] tiles);
}
