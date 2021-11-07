using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public bool paused = true;
    public bool nextPoint = false;
    public GameObject pauseMenu;
    public GameObject nextPointMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
        }

        if (paused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
        } else if (nextPoint)
        {
            Time.timeScale = 0;
            nextPointMenu.SetActive(true);
        } else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            nextPointMenu.SetActive(false);
        }
    }
}
