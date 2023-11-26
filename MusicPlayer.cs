using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private bool allowMusicPlaying = true;
    private bool battleMusicPlaying;
    public AudioSource battleIntro;
    public AudioSource battleMain;

    // Start is called before the first frame update
    void Start()
    {
        PlayBattleIntroMusic();
    }

    // Update is called once per frame
    void Update()
    {
        if (allowMusicPlaying && !battleMusicPlaying && !battleIntro.isPlaying)
            PlayBattleMusic();
            
    }

    public void PlayBattleIntroMusic()
    {
        battleIntro.Play();
    }

    private void PlayBattleMusic()
    {
        battleMusicPlaying = true;
        battleMain.Play();
    }

    public void StopPlaying()
    {
        allowMusicPlaying = false;
        foreach (AudioSource a in gameObject.GetComponents<AudioSource>())
        {
            if (a.isPlaying)
                a.Stop();
        };
    }
}
