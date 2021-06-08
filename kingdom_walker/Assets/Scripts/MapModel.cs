using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapModel
{
    public int _rows;
    public int _columns;
    public int _forestCnt;
    public int _waterCnt;
    public int _plainsCnt;
    public int _rTx = 5;
    public int _rTy = 4;
    public int _bTx = 0;
    public int _bTy = 0;
    public readonly List<TileModel> _tiles;

    public MapModel(int Rows = 5, int Columns = 6, int ForestCount = 0, int WaterCount = 0, int PlainsCount = 28, int RTX = 0, 
        int RTY = 0, int BTX = 5, int BTY = 4)
    {
        _rows = Rows;
        _columns = Columns;
        _forestCnt = ForestCount;
        _waterCnt = WaterCount;
        _plainsCnt = PlainsCount;
        _rTx = RTX;
        _rTy = RTY;
        _bTx = BTX;
        _bTy = BTY;
        _tiles = new List<TileModel>();

        FillTilesInModel();
        SetPlains();
        SetTowers();
        SetTilesForType( TileType.Forest, _forestCnt);
        SetTilesForType( TileType.Water, _waterCnt);
        
    }

    public void ClearTilesForMove()
    {
        foreach (TileModel tile in _tiles)
        {
            tile.availableForMove = false;
            tile.view.UpdateAnimation();
        }
        
    }

    public void FillTilesInModel()
    {

        for (int i = 0; i < _columns; ++i)
        {
            for (int j = 0; j < _rows; ++j)
            {
                
                TileModel tile = new TileModel(i, j);
                _tiles.Add(tile);
            }
        }
    }

    public void SetTowers()
    {
        foreach (TileModel tile in _tiles)
        {
            if (tile.x == _bTx && tile.y == _bTy)
            {
                tile.tileType = TileType.BlueTower;
            }
            else if (tile.x == _rTx && tile.y == _rTy)
            {
                tile.tileType = TileType.RedTower;
            }
        }
    }

    public void SetPlains()
    {
        foreach (TileModel tile in _tiles)
        {
            tile.tileType = TileType.Plain;
        }
    }

    public void SetTilesForType(TileType type, int count)
    {
        for (int i = 0; i < count; ++i)
        {
            bool done = false;

            while (!done)
            {
                int rand = UnityEngine.Random.Range(0, _tiles.Count);
                
                if (_tiles[rand].tileType == TileType.Plain)
                {
                    _tiles[rand].tileType = type;
                    done = true;
                }
            }
        }
    }
}
