using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelection : MonoBehaviour
{
    public List<AudioClip> music = new List<AudioClip>();
    public AudioSource music_Player;
    private void Start()
    {
        int M = Random.Range(0, music.Count);

        music_Player.clip = music[M];
        music_Player.Play();
    }
}
