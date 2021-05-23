using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class MapController : MonoBehaviour
{
    public GameObject tilePrefab;

    public List<GameObject> UITiles;

    public void InitMapObjects(MapModel map)
    {
        FillTilesInModel(map); 
        SetPlains(map);
        SetTowers(map);
        SetTilesForType(map, TileType.Forest, map.forestCnt);
        SetTilesForType(map, TileType.Water, map.waterCnt);
        PopulateTilesUI(tilePrefab, map);
        //PopulateTilesUI();
        //PopulateAvatarsUI();


    }

    public void FillTilesInModel(MapModel map)
    {

        for (int i = 0; i < map.columns; ++i)
        {
            for (int j = 0; j < map.rows; ++j)
            {
                TileModel tile = new TileModel(i, j);
                map.tiles.Add(tile);
            }
        }
    }

    public void SetTowers(MapModel map)
    {
        foreach (TileModel tile in map.tiles)
        {
            if (tile.x == 0 && tile.y == 0)
            {
                tile.tileType = TileType.BlueTower;
            }
            else if (tile.x == map.columns - 1 && tile.y == map.rows - 1)
            {
                tile.tileType = TileType.RedTower;
            }
        }
    }

    public void SetPlains(MapModel map)
    {
        foreach (TileModel tile in map.tiles)
        {
            tile.tileType = TileType.Plain;
        }
    }

    public void SetTilesForType(MapModel map, TileType type, int count)
    {
        for (int i = 0; i < count; ++i)
        {
            bool done = false;

            while (!done)
            {
                int rand = UnityEngine.Random.Range(0, map.tiles.Count);
                
                if (map.tiles[rand].tileType == TileType.Plain)
                {
                    map.tiles[rand].tileType = type;
                    done = true;
                }
            }
        }
    }

    public void PopulateTilesUI(GameObject prefab, MapModel map)
    {
        RectTransform gameHolder = GameObject.Find("GameController").GetComponent<RectTransform>();
        RectTransform mapHolder = new GameObject("Map", typeof(RectTransform)).GetComponent<RectTransform>();

        foreach (var tile in map.tiles)
        {
            //GameObject instance = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(tileModel.x, tileModel.y, 0f), Quaternion.identity);
            GameObject instance = UnityEngine.Object.Instantiate(prefab, new Vector3(tile.x , tile.y , 0f), Quaternion.identity);
            instance.transform.SetParent(mapHolder);
            instance.transform.localScale = new Vector3(0.8F, 0.8F);
            UITiles.Add(instance);
            //instance.GetComponent<TileController>().SetTileInfo(tileModel.x, tileModel.y, tileModel.fog, tileModel.isSelected, false);
            instance.GetComponent<TileView>().model = tile;
            instance.GetComponent<TileView>().SetSprites();
        }

        GameObject.Find("Map").transform.SetParent(gameHolder);

        //Camera cam = Camera.main;

        //Debug.Log(cam.ScreenToWorldPoint(GameObject.Find("Map").transform.position));
        //Debug.Log(cam.WorldToScreenPoint(GameObject.Find("Map").transform.position));
        
        mapHolder.localScale = new Vector3(1.83F, 1.83F);
        mapHolder.position = new Vector3(-4.56F, -3.7F);

        //float target = (Screen.width) / mapModel._columns;
        //Debug.Log(target);
        //mapHolder.localScale = new Vector3(target / x, target / y);
        //mapHolder.position = new Vector3(target / 2 - Screen.width / 2, -(target * mapModel._columns / 2));
    }

    public void SetPlayerMoves(PlayerModel player, MapModel map, PlayerModel opponent)
    {
        player.avatar.availableTilesForMove.Clear();
        ClearTiles(map);
        UpdateTileUI();
        // пробежать по всем тайлам. Если есть тайлы в которые можно ступить -делаем их авайлабл фор мув

        foreach (TileModel tile in map.tiles)
        {
            if (ApplyMoveType(tile, player.avatar.moveType, player) && tile != opponent.avatar.position)
            {
                player.avatar.availableTilesForMove.Add(tile);
                tile.availableForMove = true;
                UpdateTileUI();
            }
        }
        /*
        foreach (TileModel tile in player.avatar.availableTilesForMove)
        {
            Debug.Log(player.isPlayer + " " + tile.x + " " + tile.y);
        }
        */
    }

    private bool ApplyMoveType(TileModel tile, MoveType moveType, PlayerModel player)
    {
        int absoluteDefference = Mathf.Abs(player.avatar.position.x - tile.x) +
                                 Mathf.Abs(player.avatar.position.y - tile.y);

        bool result = false;

        switch (player.avatar.moveType)
        {
            case MoveType.SingleMove:

                result = absoluteDefference == player.avatar.movement;
                break;
            case MoveType.RollingRoll:
                result = absoluteDefference != 0 && absoluteDefference <= 2;
                break;
            case MoveType.HorseTrainer:
                result = absoluteDefference != 0 && absoluteDefference <= 3 &&
                    (Mathf.Abs(player.avatar.position.x - tile.x) == 2 && Mathf.Abs(player.avatar.position.y - tile.y) == 1
                    || Mathf.Abs(player.avatar.position.x - tile.x) == 1 && Mathf.Abs(player.avatar.position.y - tile.y) == 2);
                break;
            case MoveType.CabCarl:
                result = absoluteDefference != 0 && absoluteDefference <= 2
                    && (tile.x == player.avatar.position.x || tile.y == player.avatar.position.y);
                break;
            case MoveType.Bishop:
                result = absoluteDefference != 0 && absoluteDefference <= 4 &&
                (Mathf.Abs(player.avatar.position.x - tile.x) == 1 && Mathf.Abs(player.avatar.position.y - tile.y) == 1
                || Mathf.Abs(player.avatar.position.x - tile.x) == 2 && Mathf.Abs(player.avatar.position.y - tile.y) == 2);
                break;
        }

        return result;
    }

    public void UpdateTileUI()
    {
        foreach (GameObject tile in UITiles)
        {
            tile.GetComponent<TileView>().UpdateAnimation();
        }
    }

    public TileModel GetTile(int x, int y)
    {
        foreach (TileModel tile in GameController._gC._game.map.tiles)
        {
            if (tile.x == x && tile.y == y)
            {
                return tile;
            }
        }
        return null;
    }

    public void ClearTiles(MapModel map)
    {
        foreach (TileModel tile in map.tiles)
        {
            tile.availableForMove = false;
        }

        UpdateTileUI();
    }

    public TileModel SetTileForAI(MapModel map, PlayerModel player)
    {
        // get the available tiles for moving
        // from that list find the tiles that are close the most to target
        // chose randomly between those tiles
        // return result
        //Debug.Log("Shit Starts");

        TileModel blueTower = null;

        foreach (TileModel tile in map.tiles)
        {
            if (tile.tileType == TileType.BlueTower)
            {
                blueTower = tile;
            }
        }

        var AItiles = player.avatar.availableTilesForMove;

        //Debug.Log(AItiles.Count);

        //sort by their distance

        float minDistance = float.MaxValue;

        for (int i = 0; i < AItiles.Count; i++)
        {
            var calcDistance = Math.Sqrt(Math.Pow(AItiles[i].x - blueTower.x, 2) + Math.Pow(AItiles[i].y - blueTower.y, 2));
            AItiles[i].distance = (float)calcDistance;
            //Debug.Log(calcDistance);
            //Debug.Log((float)calcDistance);
        }

        List<TileModel> orderedList = AItiles.OrderBy(tile => tile.distance).ToList();

        foreach (TileModel tile in orderedList)
        {
            Debug.Log(GameController._gC.turnNumber + " " + player.isPlayer + " " + tile.distance + " " + tile.x + " " + tile.y);
        }

        List<TileModel> resultList = new List<TileModel>();

        for (int i = 0; i < orderedList.Count; i++)
        {
            if (orderedList[i].distance <= minDistance)
            {
                resultList.Add(orderedList[i]);
                minDistance = orderedList[i].distance;
            }
            else
            {
                break;
            }
        }

        var random = new System.Random();
        int index = random.Next(resultList.Count);

        var result = resultList[index];

        foreach (TileModel tile in AItiles)
        {
            tile.distance = 0F;
        }

        return result;
    }

    public GameObject GetTileUI(TileModel tile)
    {
        foreach (GameObject tileUI in UITiles)
        {
            if (tileUI.GetComponent<TileView>().model == tile)
            {
                return tileUI;
            }
        }

        return null;
    }


}
