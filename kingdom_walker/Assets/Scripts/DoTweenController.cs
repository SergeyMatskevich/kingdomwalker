using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoTweenController : MonoBehaviour
{
    [SerializeField]
    private Vector3 targetLocation = Vector3.zero;

    [Range(0.5f, 10.0f), SerializeField]
    private float moveDuration = 1.0f;

    [SerializeField]
    private Ease moveEase = Ease.Linear;

    [SerializeField]
    private DoTweenType doTweenType = DoTweenType.MovementOneWay;

    private enum DoTweenType
    {
        MovementOneWay,
        MovementTwoWay,
        MovementTwoWayWithSequence,
        MovementOneWayColorChange,
        MovementOneWayColorChangeAndScale
    }

    public void MoveToPosition(Vector3 position)
    {
        transform.DOMove(position, moveDuration).SetEase(moveEase);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
