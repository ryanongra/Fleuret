using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlRigidbody : MonoBehaviour
{

    public float Speed = 5;//how fast to move in game unit
    public float TurnRate = 180;//how fast to turn in sec
    public float ForceMultiplier = 30;
    public float parryDuration = 0.5f;
    public float parryTimer = 0;

    Rigidbody rb_;
    Animator animator;

    public OpponentController opponent;
    Animator opponentAnimator;
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();//GetComponent<Rigidbody>() allow us to get the Rigidbody component inside the gameObject where this script is attached.
        animator = GetComponent<Animator>();
        opponentAnimator = opponent.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
      
        float vertical = Input.GetAxis("Vertical");//get the up/down or w/s key input, return a value from -1 to 1
        float horizontal = Input.GetAxis("Horizontal");//get the left/right or a/d key input, return a value from -1 to 1

        float deltaTime = Time.fixedDeltaTime;

        Quaternion rotationDelta = Quaternion.Euler(0, TurnRate * horizontal * deltaTime, 0);

        Quaternion newRotation = rb_.rotation * rotationDelta;

        Vector3 forward = newRotation * Vector3.forward;
        Vector3 moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;

        if (moveDelta.x < 0)
        {
            animator.SetBool("MovingForward", true);
        } else if (moveDelta.x > 0)
        {
            animator.SetBool("MovingBackward", true);
        }
        else
        {
            ResetAnimator();
        }

        Vector3 newPos = rb_.position + moveDelta;

        rb_.position = newPos;


        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetTrigger("Lunge");
        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            animator.SetTrigger("Parry");
            if (DistanceFromOpponent() < 2.1)
            {
                opponentAnimator.SetTrigger("GetParried");
                opponent.GetDisabled();
            }
            //parryTimer = parryDuration;
        }
/*        if (parryTimer > -1)
        {
            parryTimer -= Time.deltaTime;
        }*/

    }

    public float DistanceFromOpponent()
    {
        return this.transform.position.x - opponent.transform.position.x;
    }

    public bool IsParrying()
    {
        return parryTimer > 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon") && !opponent.IsDisabled())
        {
            print("opponent touche");
        }
    }

    void ResetAnimator()
    {
        animator.SetBool("MovingForward", false);
        animator.SetBool("MovingBackward", false);
    }
}