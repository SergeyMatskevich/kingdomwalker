using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterPlay : MonoBehaviour
{
    public Animator animator;
    public AnimationClip[] clips;

    public float length;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "AttackAnimation":
                    length = clip.length;
                    Debug.Log("Attack lenght " + length);
                    break;
                case "HealAnimation":
                    length = clip.length;
                    Debug.Log("Heal lenght " + length);
                    break;
            }
        }

        Destroy(gameObject, length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
