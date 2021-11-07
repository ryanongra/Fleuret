using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreboardController : MonoBehaviour
{
    public Image playerLight;
    public Image opponentLight;

    public Text playerScoreDisplay;
    public Text opponentScoreDisplay;

    public static int playerScore;
    public static int opponentScore;

    public float cutOffTime = 2.5f;
    public float timeTillCutOff = 2.6f;

    public SoundManager soundManager;

    bool analysing = false;
    bool paused = false;

    bool playerHit = false;
    bool oppHit = false;

    public PriorityManager priorityManager;

    public GameController gameController;

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
        playerScoreDisplay.text = playerScore.ToString();
        opponentScoreDisplay.text = opponentScore.ToString();

        if (paused)
        {
            Time.timeScale = 0;
        }

    }

    public void PlayerHit()
    {
        soundManager.PlayScoreSound();
        playerLight.color = Color.red;
        timeTillCutOff = 0;
        playerHit = true;
        if (opponentLight.color == Color.green)
        {
            AnalyseHit();
        }
    }

    public void OpponentHit()
    {
        soundManager.PlayScoreSound();
        opponentLight.color = Color.green;
        timeTillCutOff = 0;
        oppHit = true;
        if (playerLight.color == Color.red)
        {
            AnalyseHit();
        }
    }

    private void AnalyseHit()
    {
        analysing = true;
        if (playerHit && !oppHit)
        {
            Debug.Log("Player score 1");
            playerScore++;
        } else if (!playerHit && oppHit)
        {
            Debug.Log("Opp score 2");
            opponentScore++;
        } else if (playerHit && oppHit)
        {
            switch (priorityManager.GetPriority())
            {
                case PriorityManager.Priority.PLAYER:
                    Debug.Log("Player score");
                    playerScore++;
                    break;
                case PriorityManager.Priority.OPPONENT:
                    Debug.Log("Opp score");
                    opponentScore++;
                    break;
                default:
                    break;
            }
        }
        playerHit = false;
        oppHit = false;
        analysing = false;
        gameController.nextPoint = true;
    }

    IEnumerator WaitCoroutine()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);

        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSecondsRealtime(3);

        //After we have waited 5 seconds print the time again.
        Debug.Log("Finished Coroutine at timestamp : " + Time.time);
    }
}
