using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject avatarPrefab;
    public GameObject hpPrefab;
    public GameObject healAnimation;
    public GameObject attackAnimation;

    public GameObject floatingText;

    public GameObject avatarPlayer;
    public GameObject avatarEnemy;
    public TextMesh hpPlayer;
    public TextMesh hpEnemy;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitAvatars(GameModel game)
    {
        PopulateAvatartsUI(avatarPrefab, game);
    }

    public void PopulateAvatartsUI(GameObject prefab, GameModel game)
    {
        RectTransform gameHolder = GameObject.Find("GameController").GetComponent<RectTransform>();
        RectTransform mapHolder = GameObject.Find("Map").GetComponent<RectTransform>();
        RectTransform playersHolder = new GameObject("Players", typeof(RectTransform)).GetComponent<RectTransform>();

        game.player.avatar.position = GameController._gC._mC.GetTile(0, 0);
        game.enemy.avatar.position = GameController._gC._mC.GetTile(5, 4);

        GameObject instancePlayer = Instantiate(prefab, new Vector3(0F, 0F, 0F), Quaternion.identity);
        instancePlayer.transform.SetParent(playersHolder);
        avatarPlayer = instancePlayer;
        instancePlayer.GetComponent<AvatarView>().model = game.player.avatar;

        hpPlayer.text = game.player.avatar.currentHP.ToString() + " / " + game.player.avatar.maxHP.ToString();
        

        GameObject instanceEnemy = Instantiate(prefab, new Vector3(5F, 4F, 0F), Quaternion.identity);
        instanceEnemy.transform.SetParent(playersHolder);
        avatarEnemy = instanceEnemy;
        instanceEnemy.GetComponent<AvatarView>().model = game.enemy.avatar;

        hpEnemy.text = game.enemy.avatar.currentHP.ToString() + " / " + game.enemy.avatar.maxHP.ToString();


        GameObject.Find("Players").transform.SetParent(mapHolder);
        GameObject.Find("Players").transform.localPosition = new Vector3(0F, 0F, 0F);
        GameObject.Find("Players").transform.localScale = new Vector3(1F, 1F, 1F);
        
    }

    public void AvatarMove(PlayerModel player, TileModel tile)
    {
        Vector3 end = GameController._gC._mC.GetTileUI(tile).transform.position;

        if (player.isPlayer)
        {
            avatarPlayer.GetComponent<DoTweenController>().MoveToPosition(end);
            //avatarPlayer.GetComponent<AvatarView>().MoveToTile(end);
            player.avatar.previousPosition = player.avatar.position;
            player.avatar.position = tile;
        }
        else
        {
            avatarEnemy.GetComponent<DoTweenController>().MoveToPosition(end);
            //avatarEnemy.GetComponent<AvatarView>().MoveToTile(end);
            player.avatar.previousPosition = player.avatar.position;
            player.avatar.position = tile;
        }
    }

    public void ResetAvatar(PlayerModel player)
    {
        player.avatar.moveType = MoveType.SingleMove;
        //player.activeCard.onTable = false;
        //player.activeCard = null;
    }

    public void InstantiateHeal(PlayerModel player)
    {
        if (player.isPlayer)
        {
            Instantiate(healAnimation, avatarPlayer.transform.position, Quaternion.identity);
        }
        else {
            Instantiate(healAnimation, avatarEnemy.transform.position, Quaternion.identity);
        }
    }

    public void InstantiateAttack(PlayerModel player)
    {
        if (player.isPlayer)
        {
            Instantiate(attackAnimation, avatarEnemy.transform);
        }
        else
        {
            Instantiate(attackAnimation, avatarPlayer.transform);
        }
    }

    public void InstantiateFloatingText(PlayerModel player, string text, Color color)
    {
        float offset = 3.15F;

        ///Vector3 instantiat

        Debug.Log("FloatingText");
        if (player.isPlayer)
        {

            GameObject instance = Instantiate(floatingText, new Vector3 (avatarPlayer.transform.position.x, avatarPlayer.transform.position.y + offset), Quaternion.identity);
            instance.GetComponent<Renderer>().sortingLayerName = "Action";
            instance.GetComponent<TextMesh>().text = text;
            instance.GetComponent<TextMesh>().color = color;
        }
        else
        {
            GameObject instance = Instantiate(floatingText, new Vector3(avatarEnemy.transform.position.x, avatarEnemy.transform.position.y + offset), Quaternion.identity);
            instance.GetComponent<Renderer>().sortingLayerName = "Action";
            instance.GetComponent<TextMesh>().text = text;
            instance.GetComponent<TextMesh>().color = color;
        }
    }

    public void UpdateHealth(PlayerModel player)
    {
        if (player.isPlayer)
        {
            hpPlayer.text = player.avatar.currentHP.ToString() + " / " + player.avatar.maxHP.ToString();
        }
        else
        {
            hpEnemy.text = player.avatar.currentHP.ToString() + " / " + player.avatar.maxHP.ToString();
        }
    }


}
