using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public float playTime;
    int hour;
    int minute;
    int second;

    public int cntApple; // 튜토리얼 사과
    public int[] cntItem;

    public Text playTimeText;
    public Text[] itemText;
    public Image[] itemImage;

    PlayerStatus _playerStatus;

    void Awake()
    {
        _playerStatus = GameObject.Find("Unity_Chan_humanoid").GetComponent<PlayerStatus>();
    }

    void Update()
    {
        ItemCounting();
        ItemExist();
        ItemUsing();
    }

    void LateUpdate()
    {
        PlayTime();
    }

    void ItemUsing()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (cntItem[0] < 1)
            {
                Debug.Log("아이템 없음");
            }
            else
            {
                if (_playerStatus.currentHp == _playerStatus.maxHp)
                {
                    Debug.Log("이미 HP가 가득 찼습니다.");
                }
                else
                {
                    _playerStatus.currentHp += 50f;
                    Debug.Log("아이템 사용");
                    cntItem[0]--;
                }
            }
        }
    }

    void ItemCounting()
    {
        for (int i=0; i<cntItem.Length; i++)
        {
            itemText[i].text = cntItem[i].ToString();
        }
    }

    void ItemExist()
    {
        for (int i=0; i<cntItem.Length; i++)
        {
            if (cntItem[i] < 1)
            {
                itemImage[i].color = new Color(1f, 1f, 1f, 0.5f);
            }
            else
            {
                itemImage[i].color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }

    void PlayTime()
    {
        playTime += Time.deltaTime;

        hour = (int)(playTime / 3600);
        minute = (int)((playTime - (hour * 3600)) / 60);
        second = (int)(playTime % 60);
        playTimeText.text = string.Format("{0:00}", hour) + ":" + string.Format("{0:00}", minute) + ":" + string.Format("{0:00}", second);
    }
}
