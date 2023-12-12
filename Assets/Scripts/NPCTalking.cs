using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class NPCTalking : MonoBehaviour
{
    public int textNum = 0; // 텍스트 배열의 수
    public int index = 0; // 텍스트 문자열 수
    public float talkingSpeed; // 대화 속도
    
    public string[] talkText = new string[4]; // 퀘스트 수락 전 대사
    public string nameText; // NPC 이름

    public bool meetNPC; // 대화 가능 범위 안에 들었는지 여부
    public bool activeTextBox; // 대화창 활성화 여부
    public bool doTalking; // 대사를 치는 중인지에 대한 여부
    public bool getQuest; // 퀘스트 수락 여부
    public int questState; // 퀘스트 상태 0=퀘스트 수락 전, 1=퀘스트 수락 후 완료 불가능, 2=퀘스트 수락 후 완료 가능, 3=퀘스트 완료 후

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

        if (!activeTextBox) // 대화창 비활성화 시 실행
        {
            playerCntl.talking = false;
            TalkStart();
        }
        else // 대화창 활성화 시 실행
        {
            playerCntl.talking = true; // 플레이어의 움직임을 비활성화 시키기 위함

            if (Input.GetKeyDown(KeyCode.F)) // 대화 애니메이션 중에 F키를 누르면 TextSkip() 함수를 실행함
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
            talkText[0] = "안녕, 유니티짱.";
            talkText[1] = "혹시 시간 되면 좀 도와줄 수 있어?";
            talkText[2] = "고마워, 그럼 사과 한 개만 가져다 줘.";
            talkText[3] = "그럼 부탁할게.";
        }
        else if (questState == 1)
        {
            talkText[0] = "기다리고 있을게.";
            talkText[1] = "";
            talkText[2] = "";
            talkText[3] = "";
        }
        else if (questState == 2)
        {
            talkText[0] = "정말 고마워, 덕분에 살았어";
            talkText[1] = "그럼 또 봐~";
            talkText[2] = "";
            talkText[3] = "";
        }
        else if (questState == 3)
        {
            talkText[0] = "또 봐~";
            talkText[1] = "";
            talkText[2] = "";
            talkText[3] = "";
        }
    }

    void TalkStart() // 대화 시작
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

    void Talking() // 대화 중
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

    void TalkPaging() // 대화창 넘기기
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

    void TextSkip() // 텍스트 애니메이션 스킵
    {
        doTalking = false;
        showTalk.text = talkText[textNum];
    }

    void TextBoxExit() // 대화가 완전히 끝났을 때 대화창 비활성화
    {
        textBox.gameObject.SetActive(false);
        activeTextBox = false;
    }

    void QuestAbleOrDisable() // 퀘스트 완료 가능, 불가능 전환해주는 함수
    {
        questState = _gameManager.cntApple < 1 ? 1 : 2;
    }

    void ShowQuestState() // 퀘스트 상태를 시각적으로 보여주는 함수
    {

    }

    void OnTriggerEnter(Collider other) // 범위에 들어가면 대화가 가능하게 한다
    {
        if (other.gameObject.tag == "Player")
        {
            meetNPC = true;
        }
    }

    void OnTriggerExit(Collider other) // 범위에 들어가면 대화가 불가능하게 한다
    {
        if (other.gameObject.tag == "Player")
        {
            meetNPC = false;
        }
    }
}
