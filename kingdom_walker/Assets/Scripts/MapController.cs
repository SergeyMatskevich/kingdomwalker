using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public GameObject tilePrefab;

    public RectTransform mapHolder;

    public List<GameObject> UITiles;

    public void InitMapObjects(MapModel map)
    {
        foreach (GameObject tile in UITiles)
        {
           Destroy(tile);
        }
        PopulateTilesUI(tilePrefab, map);
    }
    
    public void PopulateTilesUI(GameObject prefab, MapModel map)
    {
        RectTransform gameHolder = GameObject.Find("GameController").GetComponent<RectTransform>();
        
        if (!mapHolder)
        {
            mapHolder = new GameObject("Map", typeof(RectTransform)).GetComponent<RectTransform>();
            GameObject.Find("Map").transform.SetParent(gameHolder);
        }
        
        foreach (var tile in map._tiles)
        {
            GameObject instance = UnityEngine.Object.Instantiate(prefab, new Vector3(tile.x , tile.y , 0f), Quaternion.identity);
            instance.transform.SetParent(mapHolder);
            instance.transform.localScale = new Vector3(0.8F, 0.8F);
            UITiles.Add(instance);
            instance.GetComponent<TileView>().model = tile;
            instance.GetComponent<TileView>().SetSprites();
        }
        
        float scale = DefineMapScale(map);
        float xPosition = GetFieldXPosition(map._columns, scale);
        float yPosition = GetFieldXPosition(map._rows, scale);
        Debug.Log(xPosition);
        mapHolder.localScale = new Vector3(scale, scale);
        mapHolder.position = new Vector3(xPosition, yPosition);
        
    }

    public float DefineMapScale(MapModel map)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0F));

        float final = (point.x * 2)/map._columns;
        if (final > 2F)
        {
            return 2F;
        }
        else
        {
            return final;
        }
    }
    
    /*
    public TileModel GetTile(int x, int y)
    {
        
        foreach (TileModel tile in GameController._gC._game.map._tiles)
        {
            if (tile.x == x && tile.y == y)
            {
                return tile;
            }
        }
        return null;
        
    }
    */
    

    public TileModel SetTileForAI(MapModel map, PlayerModel player)
    {
        // get the available _tiles for moving
        // from that list find the _tiles that are close the most to target
        // chose randomly between those _tiles
        // return result
        //Debug.Log("Shit Starts");

        TileModel blueTower = null;
        
        List<TileModel> AItiles = new List<TileModel>();

        foreach (TileModel tile in map._tiles)
        {
            if (tile.tileType == TileType.BlueTower)
            {
                blueTower = tile;
            }

            if (tile.availableForMove)
            {
                AItiles.Add(tile);
            }
        }

        //var AItiles = player.avatar.availableTilesForMove;

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
            Debug.Log(GameController._gC._game._turnNumber + " " + player.isPlayer + " " + tile.distance + " " + tile.x + " " + tile.y);
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

    

    public float GetFieldXPosition(int columns, float scale)
    {
        if (columns%2 == 0)
        {
            //Debug.Log("even");
            return (((float) columns / 2 * scale - scale/2 )*-1F);
        }
        else
        {
            //Debug.Log("odd");
            return (((float) columns - 1) / 2 * scale)*-1F;
        }
    }

}
