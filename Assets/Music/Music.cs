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

    //bool firstTime = true;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // Play Music
        if (Input.GetKeyUp("1"))
        {
           Play(theIntro);
        }
        else if (Input.GetKeyUp("2"))
        {
            Play(theParty);
        }
        else if (Input.GetKeyUp("3"))
        {
            Play(theAftermath);
        }

        /*
        if (firstTime && Input.GetMouseButtonDown(0))
        {
            firstTime = false;
            theIntro.Stop();
            theAftermath.Play();
        }
        else if (!firstTime && getActiveAudio() == null && lastPlayed != null)
        {
            lastPlayed.Play();
        }
        */
    }

    public void Play(AudioSource track)
    {
        List<AudioSource> alreadyPlaying = getActiveAudio();
        bool trackIsAlreadyPlaying = false;
        foreach ( AudioSource el in alreadyPlaying)
        {
            if (el != track)
            {
                el.Stop();
            }
            else
            {
                trackIsAlreadyPlaying = true;
            }
        }
        if (!trackIsAlreadyPlaying)
        {
            track.Play();
            GameData.lastPlayedTrack = track;
        }
    }

    public List<AudioSource> getActiveAudio()
    {
        List<AudioSource> playing = new List<AudioSource>();
        if (theParty.isPlaying) { playing.Add(theParty); };
        if (theAftermath.isPlaying) { playing.Add(theAftermath); };
        if (theIntro.isPlaying) { playing.Add(theIntro); };
        return playing;
    }

    
    /*
    public void SwitchToPast() 
    {
        Play(theParty);
       
        //theParty.Play();
        //theParty.timeSamples = TimePartyMusic;
       // TimeAftermathMusic = theAftermath.timeSamples;
        //theAftermath.Stop();

    }
    public void SwitchToPresent(bool intro = false)
    {
        Play(theAftermath);
        

        //theAftermath.Play();
        //theAftermath.timeSamples = TimeAftermathMusic;
        //TimePartyMusic = theParty.timeSamples;
        //theParty.Stop();
        //Debug.Log("stopped");
    }
    */

    void FixedUpdate()
    {
        /*
        if (theAftermath.isPlaying || theParty.isPlaying)
        {
            theIntro.volume = theIntro.volume - 0.01f;
        }
        */
    }
}
