using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GetItem : MonoBehaviour
{
    public SoundEffect _soundEffect;
    public GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _soundEffect = GameObject.Find("ItemSound").GetComponent<SoundEffect>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _gameManager.cntApple++;
            _soundEffect.SoundEffects();

            Destroy(this.gameObject);
            //gameObject.SetActive(false);
        }
    }
}
