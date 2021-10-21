using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] musics;
    [SerializeField] private AudioSource audioSourceMusic;
    private MainQuestion mainQuestion;
    private static int CurrentTrack;
    private void Start()
    {
        if (CurrentTrack > musics.Length)
        {
            CurrentTrack = 0;
        }
        audioSourceMusic.PlayOneShot(musics[Random.Range(0, musics.Length)]);
        MainQuestion.GameIsEnd += EndGame;
    }
    private void OnEnable()
    {
        MainQuestion.GameIsEnd += EndGame;
    }
    private void OnDisable()
    {
        MainQuestion.GameIsEnd -= EndGame;
    }
    public void EndGame()
    {
        audioSourceMusic.Stop();
        Destroy(this);
    }
    void Update()
    {
        if (!audioSourceMusic.isPlaying)
        {
            CurrentTrack++;
            if (CurrentTrack > musics.Length)
            {
                CurrentTrack = 0;
            }
            audioSourceMusic.PlayOneShot(musics[CurrentTrack]);
        }
    }
}
