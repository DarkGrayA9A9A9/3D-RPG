using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public bool isGameover;

    public int level = 1;
    public float currentXp;
    public float levelUpXp;
    public float currentHp;
    public float maxHp;
    public float atk;

    public Slider _hpSlider;
    public Text _hpValue;
    public Image _hpGauge;

    public Slider _xpSlider;
    public Text _xpValue;
    public Text _levelText;
    public Image _xpGauge;

    public GameObject gameOverText;

    AudioSource _audioSource;
    public AudioClip levelUpSound;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameover)
        {
            Regame();
        }
        else
        {
            GameOver();
        }

        HpInit();
        XpInit();
    }

    void HpInit()
    {
        HpSetting();
        HpSliderSetting();
        ShowHpText();
        HpGaugeColor();
    }

    void XpInit()
    {
        LevelUpSetting();
        XpSliderSetting();
        ShowXpText();
        ShowLevel();
        LevelGauge();
        LevelUpXpSetting();
    }

    void HpSetting()
    {
        if (currentHp > maxHp)
        {
            currentHp = maxHp;
        }
    }

    void HpSliderSetting()
    {
        _hpSlider.value = currentHp / maxHp;
    }
    
    void ShowHpText()
    {
        _hpValue.text = (int)currentHp + "/" + (int)maxHp;
    }

    void HpGaugeColor()
    {
        if ((currentHp / maxHp) < 0.01f)
        {
            _hpGauge.color = new Color(0f, 0f, 0f, 0f);
        }
        else if ((currentHp / maxHp) < 0.25f)
        {
            _hpGauge.color = new Color(1f, 0f, 0f, 1f);
        }
        else if ((currentHp / maxHp) < 0.5f)
        {
            _hpGauge.color = new Color(1f, 1f, 0f, 1f);
        }
        else
        {
            _hpGauge.color = new Color(0f, 1f, 0f, 1f);
        }
    }

    void LevelUpSetting()
    {
        if (currentXp >= levelUpXp && level < 20)
        {
            LevelUpSound();
            level++;
            currentXp -= levelUpXp;
        }
    }

    void LevelUpSound()
    {
        _audioSource.PlayOneShot(levelUpSound);
    }

    void XpSliderSetting()
    {
        _xpSlider.value = currentXp / levelUpXp;
    }

    void ShowXpText()
    {
        _xpValue.text = (100 * (currentXp / levelUpXp)).ToString("F1") + "%";
    }
    void ShowLevel()
    {
        _levelText.text = "Lv" + level;
    }

    void LevelGauge()
    {
        if (currentXp / levelUpXp < 0.01f)
        {
            _xpGauge.color = new Color(0f, 0f, 0f, 0f);
        }
        else
        {
            _xpGauge.color = new Color(0f, 1f, 1f, 1f);
        }
    }

    void LevelUpXpSetting()
    {
        switch (level)
        {
            case 1:
                levelUpXp = 10;
                maxHp = 100;
                break;
            case 2:
                levelUpXp = 15;
                maxHp = 120;
                break;
            case 3:
                levelUpXp = 20;
                maxHp = 140;
                break;
            case 4:
                levelUpXp = 25;
                maxHp = 160;
                break;
            case 5:
                levelUpXp = 30;
                maxHp = 180;
                break;
            case 6:
                levelUpXp = 40;
                maxHp = 200;
                break;
            case 7:
                levelUpXp = 50;
                maxHp = 220;
                break;
            case 8:
                levelUpXp = 60;
                maxHp = 240;
                break;
            case 9:
                levelUpXp = 70;
                maxHp = 260;
                break;
            case 10:
                levelUpXp = 80;
                maxHp = 280;
                break;
            case 11:
                levelUpXp = 100;
                maxHp = 300;
                break;
            case 12:
                levelUpXp = 125;
                maxHp = 320;
                break;
            case 13:
                levelUpXp = 150;
                maxHp = 340;
                break;
            case 14:
                levelUpXp = 175;
                maxHp = 360;
                break;
            case 15:
                levelUpXp = 200;
                maxHp = 380;
                break;
            case 16:
                levelUpXp = 250;
                maxHp = 400;
                break;
            case 17:
                levelUpXp = 300;
                maxHp = 420;
                break;
            case 18:
                levelUpXp = 350;
                maxHp = 440;
                break;
            case 19:
                levelUpXp = 400;
                maxHp = 470;
                break;
            case 20:
                Debug.Log("¸¸·¾ ´Þ¼º!");
                levelUpXp = 99999;
                maxHp = 500;
                break;
        }
    }

    void GameOver()
    {
        if (currentHp <= 0)
        {
            isGameover = true;
            Time.timeScale = 0;
            gameOverText.SetActive(true);
        }
    }

    Vector3 startPosition = new Vector3(70f, 22f, 38f);
    void Regame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            isGameover = false;
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
