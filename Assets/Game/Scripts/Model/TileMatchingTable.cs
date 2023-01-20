using System.Collections.Generic;

public class TileMatchingTable : ITileMatching
{
    private Dictionary<int, List<Tile>> _matchTable;

    private IBoardShuffler _boardShuffler;

    private int _connectionId;


    public TileMatchingTable(IBoardShuffler boardShuffler)
    {
        _matchTable = new Dictionary<int, List<Tile>>();
        _boardShuffler = boardShuffler;
        _connectionId = 0;
    }

    public void MatchTiles(Tile[,] tiles)
    {
        ClearOldMatches();
        BoardRowCheck(tiles);
        BoardColumnCheck(tiles);
        //matching data is complete

        //no tiles matched
        if (_connectionId == 0)
        {
            _boardShuffler.ShuffleBoard(tiles);
            MatchTiles(tiles);
        }
    }

    public int GetMatchTierIndex(int connectionId, TierData[] tierData)
    {
        List<Tile> matches;
        for (int i = tierData.Length - 1; i >= 0; i--)
        {
            _matchTable.TryGetValue(connectionId, out matches);
            //1 is the default value of not matching with any tiles(it's just matching itself :d) otherwise it is 2 or more
            //we do not create that data for every tile to reduce the amount of elements in the table
            if (tierData[i].LowerMatchLimit <= (matches?.Count ?? 1))
            {
                return i;
            }
        }
        return 0;//DefaultTile
    }

    public List<Tile> GetMatchingTiles(int connectionId)
    {
        return _matchTable[connectionId];
    }

    private void BoardRowCheck(Tile[,] tiles)
    {
        Tile firstMatchingTile = null;

        List<Tile> connectedTiles = new List<Tile>();

        for (int i = 0; i < tiles.GetLength(0); i++)
        {
            for (int j = 0; j < tiles.GetLength(1); j++)
            {
                MatchCheck(tiles, ref firstMatchingTile, connectedTiles, i, j);
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

    private void BoardColumnCheck(Tile[,] tiles)
    {
        Tile firstMatchingTile = null;

        List<Tile> connectedTiles = new List<Tile>();

        for (int i = 0; i < tiles.GetLength(1); i++)
        {
            for (int j = 0; j < tiles.GetLength(0); j++)
            {
                MatchCheck(tiles, ref firstMatchingTile, connectedTiles, j, i);
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

    private void MatchCheck(Tile[,] tiles, ref Tile firstMatchingTile, List<Tile> connectedTiles, int i, int j)
    {
        if (connectedTiles.Count == 0)
        {
            connectedTiles.Add(tiles[i, j]);
            firstMatchingTile = tiles[i, j];
        }
        //varsa ilk elemanın color id sine bak aynıysa ekle
        else if (firstMatchingTile.ColorId == tiles[i, j].ColorId)
        {
            connectedTiles.Add(tiles[i, j]);
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
                connectedTiles.Add(tiles[i, j]);
                firstMatchingTile = tiles[i, j];
            }
            //listenin uzunluğu 1 den fazla değilse listeyi sıfırla
            else
            {
                connectedTiles.Clear();
                connectedTiles.Add(tiles[i, j]);
                firstMatchingTile = tiles[i, j];
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
