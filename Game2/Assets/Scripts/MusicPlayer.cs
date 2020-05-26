using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code authored by Jashan https://answers.unity.com/questions/11314/audio-or-music-to-continue-playing-between-scene-c.html
public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance
    {
        get { return instance; }
    }
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
