using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public Music music;
   

    // Start is called before the first frame update
    void Start()
    {
        //music =  Music.Instantiate(music);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
           Debug.Log("E key was released");
            if (music is Music) {
                music.SwitchToPast();
            }
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            Debug.Log("R key was released");
            if (music is Music)
            {
                music.SwitchToPresent();
               
            }
        }
    }
}
