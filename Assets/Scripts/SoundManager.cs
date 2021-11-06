using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public AudioSource ParrySound;
    public AudioSource ScoreSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayParrySound()
    {
        ParrySound.Play();
    }

    public void PlayScoreSound()
    {
        if (!ScoreSound.isPlaying)
        {
            ScoreSound.Play();
        }
    }
}
