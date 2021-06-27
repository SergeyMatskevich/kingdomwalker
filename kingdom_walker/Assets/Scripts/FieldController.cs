using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldController : MonoBehaviour
{
    public GameObject tilePrefab;
    public GameObject avatarPrefab;
    public GameObject cardPrefab;
    
    public List<GameObject> avatars;
    public RectTransform avatarsHolder;
    
    public RectTransform tilesHolder;
    public List<GameObject> tiles;

    public RectTransform enemyDeckHolder;
    public List<GameObject> enemyCards;
    public RectTransform playerDeckHolder;
    public List<GameObject> playerCards;
    
    public RectTransform resourcesHolder;
    public GameObject resources;

    public void InitGameField(GameModel game)
    {
        tilesHolder.localScale = new Vector3(1F, 1F);
        tilesHolder.position = new Vector3(0, 0);
        
        foreach (GameObject tile in tiles)
        {
            Destroy(tile);
        }
        
        tiles.Clear();

        foreach (GameObject avatar in avatars)
        {
            Destroy(avatar);
        }
        
        avatars.Clear();

        foreach (GameObject card in enemyCards)
        {
            Destroy(card);
        }
        
        enemyCards.Clear();
        
        foreach (GameObject card in playerCards)
        {
            Destroy(card);
        }
        
        playerCards.Clear();

        foreach (var tile in game._map._tiles)
        {
            GameObject instance = UnityEngine.Object.Instantiate(tilePrefab, new Vector3(tile.x , tile.y , 0f), Quaternion.identity);
            instance.transform.SetParent(tilesHolder);
            instance.transform.localScale = new Vector3(0.8F, 0.8F);
            tiles.Add(instance);
            instance.GetComponent<TileView>().model = tile;
            tile.view = instance.GetComponent<TileView>();
            instance.GetComponent<TileView>().SetSprites();
        }
        
        foreach (var player in game._players)
        {

            GameObject instancePlayer = Instantiate(avatarPrefab, new Vector3(player.avatar.position.x, player.avatar.position.y, 0F), Quaternion.identity);
            instancePlayer.transform.SetParent(avatarsHolder);
            instancePlayer.GetComponent<AvatarView>().model = player;
            instancePlayer.GetComponent<AvatarView>().UpdateHP();
            instancePlayer.GetComponent<AvatarView>().UpdatePlayerAnimation();
            instancePlayer.transform.localScale = avatarsHolder.transform.localScale;
            instancePlayer.transform.position = new Vector3((float)player.avatar.position.x, (float)player.avatar.position.y);
            avatars.Add(instancePlayer);
            
            InitPlayerDeck(player);
        }
        
        float scale = DefineScale(game._map._columns);
        float xPosition = GetFieldXPosition(game._map._columns, scale);
        float yPosition = GetFieldXPosition(game._map._rows, scale);
        tilesHolder.localScale = new Vector3(scale, scale);
        tilesHolder.position = new Vector3(xPosition, yPosition);
        
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0F));

        float final = (point.x * 2);

        resourcesHolder.position = new Vector3(0, yPosition - 1.5F);
        resourcesHolder.GetComponent<RectTransform>().sizeDelta = new Vector2(final/2, 1F);
        Debug.Log(game.GetPlayer().gamer);
        resources.GetComponent<ResourceView>().SetupResource(game.GetPlayer().gamer);
            
        if (GameController._gC._game.GetActivePlayer().isPlayer)
        {
            GameController._gC._gEC.InstantiateMessage();
        }
        else
        {
            //GameController._gC._game.GetActivePlayer().SelectCard();
            //GameController._gC.InvokeCard();
            Invoke("InvokeEnemyMoveAnimation", 0.5F);
            Invoke("SetNextTurn", 1F);
                
        }
        
    }

    public void InitPlayerDeck(PlayerModel player)
    {
        
        
        if (player.deck != null)
        {
            if (player.deck.Count > 0)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (player.isPlayer)
                    {
                        GameObject cardInstance = Instantiate(cardPrefab, playerDeckHolder);
                        cardInstance.transform.localPosition = new Vector3(2.5F * i, 0f);
                        playerCards.Add(cardInstance);
                        if (player.deck.Count > i)
                        {
                            cardInstance.GetComponent<CardView>().model = player.deck[i];
                        }
                        else
                        {
                            cardInstance.GetComponent<CardView>().model = null;
                        }

                        cardInstance.GetComponent<CardView>().SetSprites();
                    }
                    else
                    {
                        GameObject cardInstance = Instantiate(cardPrefab, enemyDeckHolder);
                        cardInstance.transform.localPosition = new Vector3(2.5F * i, 0f);
                        enemyCards.Add(cardInstance);
                        if (player.deck.Count > i)
                        {
                            cardInstance.GetComponent<CardView>().model = player.deck[i];
                        }
                        else
                        {
                            cardInstance.GetComponent<CardView>().model = null;
                        }

                        cardInstance.GetComponent<CardView>().SetSprites();
                    }

                }

            }


            Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0F));

            float width = (point.x * 2) / 5;

            float height = point.y - 1.25F;

            // onePieceSpace = 200 * 1.4 * 0.85 * X
            float scale = width / 2.5F; // dirty hack, need to refactor;

            playerDeckHolder.localScale = new Vector3(scale, scale);
            playerDeckHolder.position = new Vector3(-4.5F, height * -1F);

            enemyDeckHolder.localScale = new Vector3(scale, scale);
            enemyDeckHolder.position = new Vector3(-4.5F, height);
        }

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
    
    public float DefineScale(int i)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,0F));

        float final = (point.x * 2)/i;
        if (final > 2F)
        {
            return 2F;
        }
        else
        {
            return final;
        }
    }
}
