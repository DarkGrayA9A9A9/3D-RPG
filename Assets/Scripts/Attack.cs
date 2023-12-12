using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float remain;
    public float delay;
    public bool isDelay;
    public GameObject weapon;

    AudioSource _audioSource;
    public AudioClip swingSound;
    
    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        Attacking();
    }

    void Attacking()
    {
        if (Input.GetMouseButton(0) && !isDelay)
        {
            isDelay = true;
            weapon.gameObject.SetActive(true);
            SwingSound();
            Invoke("WeaponActive", remain);
            Invoke("Delaying", delay);
        }
    }

    void WeaponActive()
    {
        weapon.gameObject.SetActive(false);
    }

    void Delaying()
    {
        isDelay = false;
    }

    void SwingSound()
    {
        _audioSource.PlayOneShot(swingSound);
    }
}
