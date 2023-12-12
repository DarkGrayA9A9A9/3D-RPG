using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalking : MonoBehaviour
{
    public int textNum = 0; // �ؽ�Ʈ �迭�� ��
    public int index = 0; // �ؽ�Ʈ ���ڿ� ��
    public float talkingSpeed; // ��ȭ �ӵ�
    
    public string[] talkText = new string[4]; // ����Ʈ ���� �� ���
    public string nameText; // NPC �̸�

    public bool meetNPC; // ��ȭ ���� ���� �ȿ� ������� ����
    public bool activeTextBox; // ��ȭâ Ȱ��ȭ ����
    public bool doTalking; // ��縦 ġ�� �������� ���� ����
    public bool getQuest; // ����Ʈ ���� ����
    public int questState; // ����Ʈ ���� 0=����Ʈ ���� ��, 1=����Ʈ ���� �� �Ϸ� �Ұ���, 2=����Ʈ ���� �� �Ϸ� ����, 3=����Ʈ �Ϸ� ��

    public Text showTalk;
    public Text showName;
    public GameObject textBox;

    public ThirdPersonController playerCntl;
    public GameManager _gameManager;

    public Transform target;

    void Awake()
    {
        playerCntl = GameObject.Find("Unity_Chan_humanoid").GetComponent<ThirdPersonController>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    
    void Update()
    {
        TextInit();
        ShowQuestState();

        if (!activeTextBox) // ��ȭâ ��Ȱ��ȭ �� ����
        {
            playerCntl.talking = false;
            TalkStart();
        }
        else // ��ȭâ Ȱ��ȭ �� ����
        {
            playerCntl.talking = true; // �÷��̾��� �������� ��Ȱ��ȭ ��Ű�� ����

            if (Input.GetKeyDown(KeyCode.F)) // ��ȭ �ִϸ��̼� �߿� FŰ�� ������ TextSkip() �Լ��� ������
            {
                if (doTalking)
                {
                    TextSkip();
                }
                else
                {
                    TalkPaging();
                }
            } 
        }

        if (getQuest)
        {
            QuestAbleOrDisable();
        }
    }

    void TextInit()
    {
        showName.text = nameText;

        if (questState == 0)
        {
            talkText[0] = "�ȳ�, ����Ƽ¯.";
            talkText[1] = "Ȥ�� �ð� �Ǹ� �� ������ �� �־�?";
            talkText[2] = "����, �׷� ��� �� ���� ������ ��.";
            talkText[3] = "�׷� ��Ź�Ұ�.";
        }
        else if (questState == 1)
        {
            talkText[0] = "��ٸ��� ������.";
            talkText[1] = "";
            talkText[2] = "";
            talkText[3] = "";
        }
        else if (questState == 2)
        {
            talkText[0] = "���� ����, ���п� ��Ҿ�";
            talkText[1] = "�׷� �� ��~";
            talkText[2] = "";
            talkText[3] = "";
        }
        else if (questState == 3)
        {
            talkText[0] = "�� ��~";
            talkText[1] = "";
            talkText[2] = "";
            talkText[3] = "";
        }
    }

    void TalkStart() // ��ȭ ����
    {
        if (Input.GetKeyDown(KeyCode.F) && meetNPC)
        {
            transform.LookAt(target);
            showTalk.text = "";
            textBox.gameObject.SetActive(true);
            activeTextBox = true;
            doTalking = true;
            index = 0;
            textNum = 0;

            Invoke("Talking", talkingSpeed);
        }
    }

    void Talking() // ��ȭ ��
    {
        if(showTalk.text == talkText[textNum])
        {
            doTalking = false;
            return;
        }

        if (doTalking)
        {
            showTalk.text += talkText[textNum][index];
            index++;
            Invoke("Talking", talkingSpeed);
        }
        else
        {
            return;
        }
    }

    void TalkPaging() // ��ȭâ �ѱ��
    {
        if (questState == 0)
        {
            if (textNum >= 3)
            {
                TextBoxExit();
                questState = 1;
                getQuest = true;
            }
            else
            {
                showTalk.text = "";
                textNum++;
                index = 0;
                doTalking = true;
                Invoke("Talking", talkingSpeed);
            }
        }
        else if (questState == 2)
        {
            getQuest = false;

            if (textNum >= 1)
            {
                TextBoxExit();
                questState = 3;
                _gameManager.cntApple--;
            }
            else
            {
                showTalk.text = "";
                textNum++;
                index = 0;
                doTalking = true;
                Invoke("Talking", talkingSpeed);
            }
        }
        else
        {
            TextBoxExit();
        }
    }

    void TextSkip() // �ؽ�Ʈ �ִϸ��̼� ��ŵ
    {
        doTalking = false;
        showTalk.text = talkText[textNum];
    }

    void TextBoxExit() // ��ȭ�� ������ ������ �� ��ȭâ ��Ȱ��ȭ
    {
        textBox.gameObject.SetActive(false);
        activeTextBox = false;
    }

    void QuestAbleOrDisable() // ����Ʈ �Ϸ� ����, �Ұ��� ��ȯ���ִ� �Լ�
    {
        questState = _gameManager.cntApple < 1 ? 1 : 2;
    }

    void ShowQuestState() // ����Ʈ ���¸� �ð������� �����ִ� �Լ�
    {

    }

    void OnTriggerEnter(Collider other) // ������ ���� ��ȭ�� �����ϰ� �Ѵ�
    {
        if (other.gameObject.tag == "Player")
        {
            meetNPC = true;
        }
    }

    void OnTriggerExit(Collider other) // ������ ���� ��ȭ�� �Ұ����ϰ� �Ѵ�
    {
        if (other.gameObject.tag == "Player")
        {
            meetNPC = false;
        }
    }
}
