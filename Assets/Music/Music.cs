using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public AudioSource theParty;
    public AudioSource theAftermath;
    public AudioSource theIntro;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SwitchToPast() 
    {
        theParty.Play(0);
        Debug.Log("started");
        theAftermath.Stop();
        Debug.Log("stopped");
    }
    public void SwitchToPresent()
    {
        theAftermath.Play(0);
        Debug.Log("started");
        theParty.Stop();
        Debug.Log("stopped");
    }
}
