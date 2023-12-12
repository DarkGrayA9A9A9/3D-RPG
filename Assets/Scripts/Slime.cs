using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public float hp;
    public float xp;
    public float atk;
    public float rezenTime;

    public float hitDelay;
    public float attackDelay;
    public bool hitDelaying;
    public bool attackDelaying;

    public PlayerStatus _playerStatus;

    public AudioClip attackSound;
    public AudioClip hitSound;
    public AudioClip dieSound;
    AudioSource _audioSource;

    void Awake()
    {
        _playerStatus = GameObject.Find("Unity_Chan_humanoid").GetComponent<PlayerStatus>();
        _audioSource = GetComponent<AudioSource>();
    }

    
    void Update()
    {
        KillMoster();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && !hitDelaying)
        {
            HitSound();
            hitDelaying = true;
            _playerStatus.currentHp -= atk;
            Invoke("HitDelaying", hitDelay);
        }
        else if (other.gameObject.tag == "Attack" && !attackDelaying)
        {
            AttackSound();
            attackDelaying = true;
            Invoke("AttackDelaying", attackDelay);
            hp -= _playerStatus.atk;
        }
    }

    void HitDelaying()
    {
        hitDelaying = false;
    }

    void AttackDelaying()
    {
        attackDelaying = false;
    }

    void KillMoster()
    {
        if (hp <= 0)
        {
            DieSound();
            _playerStatus.currentXp += xp;
            gameObject.SetActive(false);
            Invoke("Rezen", rezenTime);
        }
    }

    void Rezen()
    {
        hp = 50;
        this.gameObject.SetActive(true);
    }
    
    void AttackSound()
    {
        _audioSource.PlayOneShot(attackSound);
    }

    void HitSound()
    {
        _audioSource.PlayOneShot(hitSound);
    }

    void DieSound()
    {
        _audioSource.PlayOneShot(dieSound);
    }
}
