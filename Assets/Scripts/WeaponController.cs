using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public OpponentController opponent;
    public PlayerControlRigidbody player;
    Animator opponentAnimator;
    Animator playerAnimator;

    // Start is called before the first frame update
    void Start()
    {
        opponentAnimator = opponent.GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

/*    private void OnTriggerEnter(Collider collision)
    {
        print("collision detected");
        if (collision.transform.CompareTag("Weapon"))
        {
            if (player.IsParrying())
            {
                opponentAnimator.SetTrigger("GetParried");
                print("opponent got parried");
            }
*//*            if (opponent.isParrying())
            {
                playerAnimator.SetTrigger("GetParried");
            }*//*
        }
    }*/
}
