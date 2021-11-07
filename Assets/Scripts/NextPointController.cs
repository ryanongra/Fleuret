using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextPointController : MonoBehaviour
{
    public GameController gameController;

    public PlayerControlRigidbody player;

    public OpponentController opponent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.transform.position = new Vector3(2.446f, 0.5f, 3.627f);
            opponent.transform.position = new Vector3(-1.54f, 0.5f, 3.3f);
            gameController.nextPoint = false;
        }
    }
}
