using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManger : MonoBehaviour
{
    public Music[] musics;
    private string currentMusicID;
    private void Awake()
    {
        // create audiosources
        foreach (Music m in musics)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;
        }
    }

    public void ChangeMusic(string newMusicID) {
        if (currentMusicID == newMusicID)
        {
            return;
        }
        foreach (Music m in musics)
        {
            if (m.musicID == currentMusicID)
            {
                m.source.Stop();
                break;
            }
        }

        currentMusicID = newMusicID;

        foreach (Music m in musics)
        {
            if (m.musicID == currentMusicID)
            {
                m.source.Play();
                break;
            }
        }

    }
}
