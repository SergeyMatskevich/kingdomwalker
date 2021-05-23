using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarView : MovingObject
{
    public AvatarModel model;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
