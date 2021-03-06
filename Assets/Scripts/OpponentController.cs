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

    public float targetDistance;                // the distance from the player that the bot tried to maintain 
    public float minimumTargetDistance;         // the closest possible distance to the player before the bot decides to go back
    public float targetDistanceStrictness;      // a float value from 0 to 1 that determines how strictly the bot follows the target distance: 
                                                // the higher the value, the more strictly it follows

    public float distanceToAttack;              // the maximum distance from the player where the bot might choose to attack
    public float attackDistanceStrictness;      // a float value from 0 to 1 that determines how strictly the bot follows the distance strictness: 
                                                // the higher the value, the more strictly it follows

    bool inWarningZone = false;

    public bool movingForawrd;

    public PriorityManager priorityManager;

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

        if (priorityManager.priority == PriorityManager.Priority.PLAYER)
        {
            if (timeSinceLastMove > movementTime)
            {
                timeSinceLastMove = 0;
                targetDistance = Random.Range(minimumTargetDistance, 5);
            }
            else
            {
                timeSinceLastMove += Time.deltaTime;
            }

            targetDistance = Random.Range(minimumTargetDistance, 5);
            DefensiveMove();
        } else
        {
            if (timeSinceLastMove > movementTime)
            {
                timeSinceLastMove = 0;
                targetDistance = Random.Range(minimumTargetDistance, minimumTargetDistance);
            }
            else
            {
                timeSinceLastMove += Time.deltaTime;
            }

            targetDistance = Random.Range(minimumTargetDistance, minimumTargetDistance);
            OffensiveMove();
        }
    }

    private void OffensiveMove()
    {
        timeSinceLastMove = 0;
        float randomMovementDecision = Random.Range(0, 100) / 100;


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
        if (randomMovementDecision > targetDistanceStrictness)
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
            movingForawrd = false;
            animator.SetBool("MovingForward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else if (vertical > 0)
        {
            movingForawrd = true;
            animator.SetBool("MovingBackward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else
        {
            ResetAnimator();
        }

        Vector3 newPos = rb_.position + moveDelta;

        rb_.position = newPos;

        Attack();
    }

    private void DefensiveMove()
    {
        timeSinceLastMove = 0;
        float randomMovementDecision = Random.Range(0, 100) / 100;
        

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
        if (randomMovementDecision > targetDistanceStrictness)
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
            movingForawrd = false;
            animator.SetBool("MovingForward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else if (vertical > 0)
        {
            movingForawrd = true;
            animator.SetBool("MovingBackward", true);
            moveDelta = forward * Speed * vertical * deltaTime * ForceMultiplier;
        }
        else
        {
            ResetAnimator();
        }

        Vector3 newPos = rb_.position + moveDelta;

        rb_.position = newPos;

        Attack();
    }

    private void Attack()
    {
        float randomAttackDecision = Random.Range(0, 100) / 100;

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        bool attacking = stateInfo.IsName("Attack1AForwardFencing_RM");

        if (player.DistanceFromOpponent() < distanceToAttack && randomAttackDecision < attackDistanceStrictness && !attacking)
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
