using UnityEngine;
using System.Collections;

[System.Serializable]
public class audioInfo {

    public bool audio;

    public audioInfo()
    {

    }

    public void setAudio(bool audio)
    {
        this.audio = audio;
    }

    public bool getAudio()
    {
        return this.audio;
    }

}
