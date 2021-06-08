using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour
{
    public GameObject avatarPrefab;
    public GameObject hpPrefab;
    public GameObject healAnimation;
    public GameObject attackAnimation;

    public GameObject floatingText;

    public List<GameObject> avatars;
    public GameObject avatarPlayer;
    public GameObject avatarEnemy;
    public TextMesh hpPlayer;
    public TextMesh hpEnemy;

    public RectTransform playersHolder;

    
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
        foreach (GameObject avatar in avatars)
        {
            Destroy(avatar);
        }
        
        foreach (var player in game._players)
        {
            PopulatePlayerOnUI(player,avatarPrefab,playersHolder);
        }
        
    }

    public void PopulatePlayerOnUI(PlayerModel player, GameObject prefab, RectTransform holder)
    {
        GameObject instancePlayer = Instantiate(prefab, new Vector3(player.avatar.position.x, player.avatar.position.y, 0F), Quaternion.identity);
        instancePlayer.transform.SetParent(holder);
        instancePlayer.GetComponent<AvatarView>().model = player;
        instancePlayer.transform.localScale = holder.transform.localScale;
        avatars.Add(instancePlayer);
    }

    public void PopulateAvatartsUI(GameObject prefab, GameModel game)
    {
        RectTransform gameHolder = GameObject.Find("GameController").GetComponent<RectTransform>();
        RectTransform mapHolder = GameObject.Find("Map").GetComponent<RectTransform>();
        RectTransform playersHolder = new GameObject("Players", typeof(RectTransform)).GetComponent<RectTransform>();

        // this might be moved to level setup
        //game.player.avatar.position = GameController._gC._mC.GetTile(0, 0);
        //game.enemy.avatar.position = GameController._gC._mC.GetTile(5, 4);

        GameObject instancePlayer = Instantiate(prefab, new Vector3(0F, 0F, 0F), Quaternion.identity);
        instancePlayer.transform.SetParent(playersHolder);
        avatarPlayer = instancePlayer;
        //instancePlayer.GetComponent<AvatarView>().model = game.player.avatar;
        //hpPlayer.text = game.player.avatar.currentHP.ToString() + " / " + game.player.avatar.maxHP.ToString();
        
        GameObject instanceEnemy = Instantiate(prefab, new Vector3(5F, 4F, 0F), Quaternion.identity);
        instanceEnemy.transform.SetParent(playersHolder);
        avatarEnemy = instanceEnemy;
        //instanceEnemy.GetComponent<AvatarView>().model = game.enemy.avatar;
        //hpEnemy.text = game.enemy.avatar.currentHP.ToString() + " / " + game.enemy.avatar.maxHP.ToString();
        
        GameObject.Find("Players").transform.SetParent(mapHolder);
        GameObject.Find("Players").transform.localPosition = new Vector3(0F, 0F, 0F);
        GameObject.Find("Players").transform.localScale = new Vector3(1F, 1F, 1F);
        
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
