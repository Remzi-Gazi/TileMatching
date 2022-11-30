using System.Collections.Generic;

public interface ITileMatching
{
    void MatchTiles(Board board);

    int GetMatchTier(Tile tile, GameRule gameRule);

    List<Tile> GetMatchingTiles(Tile tile);
}
