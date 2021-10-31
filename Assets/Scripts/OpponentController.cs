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

    public ScoreboardController scoreboard;

    public float targetDistance;

    bool inWarningZone = false;

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
            targetDistance = Random.Range(1, 5);
        } else
        {
            timeSinceLastMove += Time.deltaTime;
        }

        targetDistance = Random.Range(1, 5);
        Move();


        


    }

    private void Move()
    {
        timeSinceLastMove = 0;
        float randomMovementDecision = Random.Range(0, 10);

        float horizontal = 0;
        float direction;
        if (targetDistance > player.DistanceFromOpponent())
        {
            direction = 1;
        }
        else
        {
            direction = -1;
        } 
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
        Vector3 moveDelta = new Vector3();

        if (player.DistanceFromOpponent() < 3 && player.DistanceFromOpponent() > 2 && !inWarningZone) 
        {
            ResetAnimator();
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else if (vertical < 0 && !inWarningZone)
        {
            animator.SetBool("MovingForward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else if (vertical > 0)
        {
            animator.SetBool("MovingBackward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else
        {
            ResetAnimator();
        }

        Vector3 newPos = rb_.position + moveDelta;

        rb_.position = newPos;


        if (player.DistanceFromOpponent() < 2)
        {
            animator.SetTrigger("Lunge");
        }
    }

    public bool IsDisabled()
    {
        return disabledRemaining > 0;
    }

    public void GetDisabled()
    {
        disabledRemaining = disabledTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Weapon"))
        {
            scoreboard.PlayerHit();
        }
        if(other.transform.CompareTag("OpponentWarningZone"))
        {
            inWarningZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("OpponentWarningZone"))
        {
            inWarningZone = false;
        }
    }

    void ResetAnimator()
    {
        animator.SetBool("MovingForward", false);
        animator.SetBool("MovingBackward", false);
    }
}
