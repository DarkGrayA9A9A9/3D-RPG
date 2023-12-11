using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    AudioSource _audioSource;
    public AudioClip getItem;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundEffects()
    {
        _audioSource.clip = getItem;
        _audioSource.Play();
    }
}
