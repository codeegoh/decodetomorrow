using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigiShared;

public class AnimateCharacter : MonoBehaviour
{

    Animator animator;

    void Start()
    {

        animator = GetComponent<Animator>();

    }

    public void OnAnimationStateChange(MoveAvatar.AvatarAnimationState animationState)
    {

        switch (animationState)
        {

            case MoveAvatar.AvatarAnimationState.Walk:
            case MoveAvatar.AvatarAnimationState.Run:
                animator.SetBool("isMoving", true);
                break;

            case MoveAvatar.AvatarAnimationState.Idle:
                animator.SetBool("isMoving", false);
                break;

        }

    }

}