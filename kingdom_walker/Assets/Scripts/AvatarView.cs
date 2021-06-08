using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class AvatarView : MovingObject
{
    public PlayerModel model;

    public Animator animator;

    //public TextMeshProUGUI hp;
    public TextMeshPro hp;
    
    protected override void Start()
    {
        base.Start();

        GameController._gC.OnAvatarChangeHP += UpdateHP;
    }

    public void UpdatePlayerAnimation()
    {
        animator.SetBool("isPlayer", model.isPlayer);
    }

    public void UpdateHP()
    {
        hp.text = model.avatar.currentHP.ToString() + "/" + model.avatar.maxHP.ToString();
    }

    public void MoveAvatar(Vector3 end, TileModel tile)
    {
        GetComponent<SpriteRenderer>().flipX = true;
        // false направо
        // true налево
        if (end.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        
        GetComponent<Rigidbody2D>().DOMove(end, 1F, false);
        model.avatar.previousPosition = model.avatar.position;
        model.avatar.position = tile;
    }

    public void MoveToTile(Vector3 end)
    {
        GetComponent<SpriteRenderer>().flipX = true;
        // false направо
        // true налево
        if (end.x > transform.position.x)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        Move(end);
    }
}
