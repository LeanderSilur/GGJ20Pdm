using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData
{
    public enum TIME { PRESENT, PAST };
    private static TIME time = TIME.PRESENT;
    public static AudioSource lastPlayedTrack;

    static public void SetTime(TIME t)
    {
        time = t;
    }

    public static bool IsPresent()
    {
        return time == TIME.PRESENT;
    }

    public static bool IsPast()
    {
        return time == TIME.PAST;
    }
}
