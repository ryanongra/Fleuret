using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PriorityManager : MonoBehaviour
{

    public PlayerControlRigidbody player;
    public OpponentController opponent;

    public enum Priority {PLAYER, OPPONENT, NONE};

    public Priority priority = Priority.NONE;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // if player moving forward and opponent not moving forward, player gets priority
        if (player.movingForward && !opponent.movingForawrd)
        {
            priority = Priority.PLAYER;
        }
        // if opponent moving forward and player not moving forward, opponent gets priority
        if (!player.movingForward && opponent.movingForawrd)
        {
            priority = Priority.OPPONENT;
        }
        // if no one moving no priority
        if (!player.movingForward && !opponent.movingForawrd)
        {
            priority = Priority.NONE;
        }

        if (opponent.IsDisabled())
        {
            playerParry();
        }

    }

    public void playerParry()
    {
        priority = Priority.PLAYER;
    }

    public void opponenetParry()
    {
        priority = Priority.OPPONENT;
    }

    public Priority GetPriority()
    {
        return priority;
    }
}
