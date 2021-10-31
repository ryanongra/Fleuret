using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreboardController : MonoBehaviour
{
    public Image playerLight;
    public Image opponentLight;

    public Text playerScoreDisplay;
    public Text OpponentScoreDisplay;

    public int playerScore;
    public int opponentScore;

    public float cutOffTime = 2.5f;
    public float timeTillCutOff = 2.6f;

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        opponentScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (cutOffTime < timeTillCutOff)
        {
            playerLight.color = Color.clear;
            opponentLight.color = Color.clear;
        } else
        {
            timeTillCutOff += Time.deltaTime;
        }
    }

    public void PlayerHit()
    {
        playerLight.color = Color.red;
        timeTillCutOff = 0;
        print("red");
    }

    public void OpponentHit()
    {
        opponentLight.color = Color.green;
        timeTillCutOff = 0;
        print("green");
    }
}
