using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpponentController : MonoBehaviour
{
    public float Speed;
    public float TurnRate = 180;
    public float ForceMultiplier;

    public float disabledTime = 0.5f;
    float disabledRemaining = 0;

    public float movementTime = 0.5f;
    public float timeSinceLastMove = 0;

    public PlayerControlRigidbody player;
    Animator playerAnimator;

    Rigidbody rb_;
    Animator animator;

    public float targetDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb_ = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerAnimator = player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (IsDisabled())
        {
            disabledRemaining -= Time.deltaTime;
        }
        if (timeSinceLastMove > movementTime)
        {
            timeSinceLastMove = 0;
            targetDistance = Random.Range(2, 5);
        } else
        {
            timeSinceLastMove += Time.deltaTime;
        }

        targetDistance = Random.Range(2, 5);
        Move();


        


    }

    private void Move()
    {
        timeSinceLastMove = 0;
        float randomMovementDecision = Random.Range(0, 10);
        print(randomMovementDecision);
        float horizontal = 0;
        float direction;
        if (targetDistance > player.DistanceFromOpponent())
        {
            direction = 1;
        }
        else //if (targetDistance < player.DistanceFromOpponent() - 1)
        {
            direction = -1;
        } /*else
        {
            direction = 0;
        }*/
        float vertical;
        if (randomMovementDecision > 8)
        {
            vertical = 1 * direction;
        }
        else
        {
            vertical = -1 * direction;
        }

        float deltaTime = Time.fixedDeltaTime;

        Quaternion rotationDelta = Quaternion.Euler(0, TurnRate * horizontal * deltaTime, 0);

        Quaternion newRotation = rb_.rotation * rotationDelta;

        Vector3 forward = newRotation * Vector3.forward;
        Vector3 moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;

        if (moveDelta.x < 0)
        {
            animator.SetBool("MovingForward", true);
        }
        else if (moveDelta.x > 0)
        {
            animator.SetBool("MovingBackward", true);
        }
        else
        {
            ResetAnimator();
        }

        Vector3 newPos = rb_.position + moveDelta;

        rb_.position = newPos;
    }

    public bool IsDisabled()
    {
        return disabledRemaining > 0;
    }

    public void GetDisabled()
    {
        print("disabled");
        disabledRemaining = disabledTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            print("player touche");
        }
    }

    void ResetAnimator()
    {
        animator.SetBool("MovingForward", false);
        animator.SetBool("MovingBackward", false);
    }
}
