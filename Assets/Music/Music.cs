using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource theParty;
    public AudioSource theAftermath;
    public AudioSource theIntro;
    private int TimeAftermathMusic;
    private int TimePartyMusic;
    private float IntroVolume;

    bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (firstTime && Input.GetMouseButtonDown(0))
        {
            firstTime = false;
            theIntro.Stop();
            theAftermath.Play();
        }
    }

  

    public void SwitchToPast() 
    {
        Debug.Log(TimePartyMusic);
        theParty.Play();
        theParty.timeSamples = TimePartyMusic;
        Debug.Log("started");
        TimeAftermathMusic = theAftermath.timeSamples;
        theAftermath.Stop();
        Debug.Log("stopped");

    }
    public void SwitchToPresent()
    {
        if (firstTime)
        {
            theIntro.Play();

        }
        else {
            theAftermath.Play();
            theAftermath.timeSamples = TimeAftermathMusic;
            TimePartyMusic = theParty.timeSamples;
            theParty.Stop();
            Debug.Log("stopped");
        }


    }

    void FixedUpdate()
    {
        if (theAftermath.isPlaying || theParty.isPlaying)
        {
            theIntro.volume = theIntro.volume - 0.01f;
        }
    }
}
