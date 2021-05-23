using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public int rows;
    public int columns;
    public int forestCnt;
    public int waterCnt;
    public int plainsCnt;
    public List<TileModel> tiles;

    public MapModel(int lvl)
    {
        switch (lvl)
        {
            case 1: //movement plain
                rows = 5;
                columns = 6;
                forestCnt = 0;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 2: // movement forest
                rows = 5;
                columns = 6;
                forestCnt = 28;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 3: // movement water
                rows = 5;
                columns = 6;
                forestCnt = 0;
                waterCnt = 28;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 4: // attack plain
                rows = 5;
                columns = 6;
                forestCnt = 5;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 5: // attack forrest
                rows = 5;
                columns = 6;
                forestCnt = 23;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 6: // attack water
                rows = 5;
                columns = 6;
                forestCnt = 0;
                waterCnt = 23;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 7: // plain
                rows = 5;
                columns = 6;
                plainsCnt = Random.Range(4, 13);
                forestCnt = Random.Range(4, 13);
                waterCnt = 28 - forestCnt - plainsCnt;
                tiles = new List<TileModel>();
                break;
            case 8: // plain heal
                rows = 5;
                columns = 6;
                forestCnt = 5;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 9: // forest heal
                rows = 5;
                columns = 6;
                forestCnt = 23;
                waterCnt = 0;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 10: // water heal
                rows = 5;
                columns = 6;
                forestCnt = 0;
                waterCnt = 23;
                plainsCnt = 28;
                tiles = new List<TileModel>();
                break;
            case 11:
                rows = 5;
                columns = 6;
                plainsCnt = Random.Range(4, 13);
                forestCnt = Random.Range(4,13);
                waterCnt = 28 - forestCnt - plainsCnt;
                tiles = new List<TileModel>();
                break;
        }
    }

}
