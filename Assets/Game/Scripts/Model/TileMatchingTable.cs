using System.Collections.Generic;

public class TileMatchingTable : ITileMatching
{
    private Dictionary<int, List<Tile>> _matchTable;
    private int _connectionId;
    private IBoardShuffler _boardShuffler;

    public TileMatchingTable(IBoardShuffler boardShuffler)
    {
        _matchTable = new Dictionary<int, List<Tile>>();
        _connectionId = 0;
        _boardShuffler = boardShuffler;
    }

    public void MatchTiles(Board board)
    {
        ClearOldMatches();
        BoardRowCheck(board);
        BoardColumnCheck(board);
        //matching data is complete

        //no tiles matched
        if (_connectionId == 0)
        {
            _boardShuffler.ShuffleBoard(board);
            MatchTiles(board);
        }
    }

    /*public int GetMatchTier(Tile tile, GameRule gameRule)
    {
        
        if (tile.ConnectionId == -1)
        {
            return 0;
        }
        else if(gameRule.MinimumMatchCounts.tierC <=  _matchTable[tile.ConnectionId].Count)
        {
            return 3;
        }
        else if (gameRule.MinimumMatchCounts.tierB <= _matchTable[tile.ConnectionId].Count)
        {
            return 2;
        }
        else if (gameRule.MinimumMatchCounts.tierA <= _matchTable[tile.ConnectionId].Count)
        {
            return 1;
        }

        return 0;
    }*/

    public int GetMatchTierIndex(Tile tile, GameRule gameRule)
    {
        if (tile.ConnectionId == -1)
        {
            return 0;//DefaultTile
        }

        for (int i = gameRule.TierDatas.Length - 1; i >= 0; i--)
        {
            if (gameRule.TierDatas[i].lowerMatchLimit <= _matchTable[tile.ConnectionId].Count)
            {
                return i;
            }
        }

        return 0;//DefaultTile
    }

    public List<Tile> GetMatchingTiles(Tile tile)
    {
        return _matchTable[tile.ConnectionId];
    }

    private void BoardRowCheck(Board board)
    {
        Tile firstMatchingTile = null;

        List<Tile> connectedTiles = new List<Tile>();

        for (int i = 0; i < board.Tiles.GetLength(0); i++)
        {
            for (int j = 0; j < board.Tiles.GetLength(1); j++)
            {
                MatchCheck(board, ref firstMatchingTile, connectedTiles, i, j);
            }

            //benzer kontrolleri burda sadece arrayin sonuna geldiğimiz için yapalım
            //listenin uzunluğuna bak 1 den fazlaysa ekle
            //listeyi temizle
            if (connectedTiles.Count > 1)
            {
                AddMatchToTable(connectedTiles);
            }
            connectedTiles.Clear();

        }
    }

    private void BoardColumnCheck(Board board)
    {
        Tile firstMatchingTile = null;

        List<Tile> connectedTiles = new List<Tile>();

        for (int i = 0; i < board.Tiles.GetLength(1); i++)
        {
            for (int j = 0; j < board.Tiles.GetLength(0); j++)
            {
                MatchCheck(board, ref firstMatchingTile, connectedTiles, j, i);
            }

            //benzer kontrolleri burda sadece arrayin sonuna geldiğimiz için yapalım
            //listenin uzunluğuna bak 1 den fazlaysa ekle
            //listeyi temizle
            if (connectedTiles.Count > 1)
            {
                AddMatchToTable(connectedTiles);
            }
            connectedTiles.Clear();
        }
    }

    private void MatchCheck(Board board, ref Tile firstMatchingTile, List<Tile> connectedTiles, int i, int j)
    {
        if (connectedTiles.Count == 0)
        {
            connectedTiles.Add(board.Tiles[i, j]);
            firstMatchingTile = board.Tiles[i, j];
        }
        //varsa ilk elemanın color id sine bak aynıysa ekle
        else if (firstMatchingTile.ColorId == board.Tiles[i, j].ColorId)
        {
            connectedTiles.Add(board.Tiles[i, j]);
        }
        else
        {
            //eğer tileların rengi farklıysa listenin uzunluğuna bak listenin uzunluğu 1 den fazlaysa
            //hashmap e  connectionIdyi alıp ekle
            //ardından elindeki elemanı listeyi sıfırlayıp listeye ekle
            //connection idyi 1 artır
            if (connectedTiles.Count > 1)
            {
                AddMatchToTable(connectedTiles);

                connectedTiles.Clear();
                connectedTiles.Add(board.Tiles[i, j]);
                firstMatchingTile = board.Tiles[i, j];
            }
            //listenin uzunluğu 1 den fazla değilse listeyi sıfırla
            else
            {
                connectedTiles.Clear();
                connectedTiles.Add(board.Tiles[i, j]);
                firstMatchingTile = board.Tiles[i, j];
            }
        }
    }

    private void AddMatchToTable(List<Tile> connectedTiles)
    {
        //Bu listede daha önceden başka tilelar ile eşleştirilmiş bir tile varsa o eşleştirme idsini bul
        int tileConnectionId = -1;//not connected tile id
        for(int i = 0; i < connectedTiles.Count; i++)
        {
            if(connectedTiles[i].ConnectionId != -1)//this means this tile is connected
            {
                if(tileConnectionId == -1)//minimum atanmadıysa
                {
                    tileConnectionId = connectedTiles[i].ConnectionId;
                }
                else if(tileConnectionId > connectedTiles[i].ConnectionId)//minimum atandı ise
                {
                    tileConnectionId = connectedTiles[i].ConnectionId;
                }
            }
        }
        

        //listede daha önceden hiçbir eşleşme olmamış o zaman direkt ekleyebiliriz
        if (tileConnectionId == -1)
        {
            for (int i = 0; i < connectedTiles.Count; i++)
            {
                connectedTiles[i].ConnectionId = _connectionId;
            }

            _matchTable.Add(_connectionId, new List<Tile>(connectedTiles));
            _connectionId++;
        }
        //bu listede daha önceden bu tilelar ile eşleşmiş bir tile bulunuyor ve idsi bu
        else if(tileConnectionId != -1)
        {
            //en baştakinin idsine hepsini ekleyerek
            for (int i = 0; i < connectedTiles.Count; i++)
            {
                //daha önceden birleştirilmiş tileları ilk eşleşen tile ile birleştiriyoruz
                if (connectedTiles[i].ConnectionId != -1 && connectedTiles[i].ConnectionId != tileConnectionId)
                {
                    int connectedId = connectedTiles[i].ConnectionId;

                    for (int j = 0; j < _matchTable[connectedId].Count; j++)
                    {
                        _matchTable[tileConnectionId].Add(_matchTable[connectedId][j]);
                        _matchTable[connectedId][j].ConnectionId = tileConnectionId;
                    }

                    _matchTable.Remove(connectedId);
                }
                else if(connectedTiles[i].ConnectionId == -1)
                {
                    _matchTable[tileConnectionId].Add(connectedTiles[i]);
                    connectedTiles[i].ConnectionId = tileConnectionId;
                }
            }
        }  

        
    }

    private void ClearOldMatches()
    {
        _matchTable.Clear();
        _connectionId = 0;
    }

    
}
