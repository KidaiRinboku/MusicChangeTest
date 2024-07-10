using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public AudioSource normalMusic;
    public AudioSource battleMusic;

    private void Start()
    {
        // 音楽をループ再生する設定
        normalMusic.loop = true;
        battleMusic.loop = true;

        // 初期音量と速度を設定
        normalMusic.volume = 1.0f;
        normalMusic.pitch = 1.0f;
        battleMusic.volume = 0.0f;
        battleMusic.pitch = 0.5f;

        // 音楽を再生
        normalMusic.Play();
        battleMusic.Play();
    }
    public void NormalBgmControl()
    {
        normalMusic.volume = 1.0f; 
        battleMusic.volume = 0.0f; 
        normalMusic.pitch = 1.0f;
        battleMusic.pitch = 0.5f;
    }
    public void BattleBgmControl()
    {
        battleMusic.volume = 1.0f; 
        normalMusic.volume = 0.0f; 
        battleMusic.pitch = 1.0f;
        normalMusic.pitch = 2.0f;
    }
}