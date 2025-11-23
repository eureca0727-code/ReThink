using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyInteraction : MonoBehaviour
{
    [Header("UI References")]
    public GameObject interactionHint;
    public GameObject choicePanel;
    public TextMeshProUGUI dialogueText;
    public GameObject buttonPanel;
    public Button allyButton;
    public Button killButton;

    [Header("기본 대사 (적에게 대사가 없을 때)")]
    [TextArea(2, 5)]
    public string[] defaultDialogues = {
        "[적] 넌 누구지?!",
        "[나] 조용히 해. 소리 지르면...",
        "[적] 제발 살려줘!",
        "[나] 선택하겠어."
    };

    private GameObject nearbyEnemy;
    private bool isChoiceOpen = false;
    private int currentDialogueIndex = 0;
    private string[] currentDialogues; // 현재 사용할 대사

    private void Start()
    {
        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (allyButton != null)
        {
            allyButton.gameObject.SetActive(true);
            allyButton.onClick.AddListener(OnAllyButtonClicked);
        }

        if (killButton != null)
        {
            killButton.gameObject.SetActive(true);
            killButton.onClick.AddListener(OnKillButtonClicked);
        }

        if (buttonPanel != null)
            buttonPanel.SetActive(false);
    }

    private void Update()
    {
        if (isChoiceOpen)
        {
            // TimeScale 유지
            if (Time.timeScale != 0f)
            {
                Time.timeScale = 0f;
            }

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextDialogue();
            }
            return;
        }

        if (nearbyEnemy != null && Input.GetKeyDown(KeyCode.E))
        {
            OpenChoicePanel();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBody")
        {
            nearbyEnemy = collision.gameObject;
            if (interactionHint != null)
                interactionHint.SetActive(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBody")
        {
            nearbyEnemy = null;
            if (interactionHint != null)
                interactionHint.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBody")
        {
            nearbyEnemy = collision.gameObject;
            if (interactionHint != null)
                interactionHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "EnemyBody")
        {
            nearbyEnemy = null;
            if (interactionHint != null)
                interactionHint.SetActive(false);
        }
    }

    private void OpenChoicePanel()
    {
        Debug.Log("대화 시작!");

        isChoiceOpen = true;
        currentDialogueIndex = 0;

        // 적의 부모 오브젝트에서 대사 가져오기
        GameObject enemyParent = nearbyEnemy.transform.parent != null ?
            nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

        EnemyDialogue enemyDialogue = enemyParent.GetComponent<EnemyDialogue>();

        if (enemyDialogue != null)
        {
            currentDialogues = enemyDialogue.GetDialogues();
            Debug.Log("적 고유 대사 사용: " + enemyParent.name);
        }
        else
        {
            currentDialogues = defaultDialogues;
            Debug.Log("기본 대사 사용");
        }

        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(true);

        Time.timeScale = 0f;

        if (buttonPanel != null)
            buttonPanel.SetActive(false);

        ShowDialogue();
    }

    private void ShowDialogue()
    {
        if (dialogueText != null && currentDialogueIndex < currentDialogues.Length)
        {
            dialogueText.text = currentDialogues[currentDialogueIndex];
        }
    }

    private void NextDialogue()
    {
        currentDialogueIndex++;

        if (currentDialogueIndex < currentDialogues.Length)
        {
            ShowDialogue();
        }
        else
        {
            ShowButtons();
        }
    }

    private void ShowButtons()
    {
        Debug.Log("선택지 표시!");

        if (dialogueText != null)
            dialogueText.text = "어떻게 할까?";

        if (buttonPanel != null)
            buttonPanel.SetActive(true);

        if (allyButton != null)
            allyButton.gameObject.SetActive(true);

        if (killButton != null)
            killButton.gameObject.SetActive(true);
    }

    private void CloseChoicePanel()
    {
        Debug.Log("대화 종료!");

        isChoiceOpen = false;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        nearbyEnemy = null;
        Time.timeScale = 1f;
    }

    private void OnAllyButtonClicked()
    {
        Debug.Log("우리편으로!");

        // 시야 공유 활성화
        if (nearbyEnemy != null)
        {
            GameObject enemyParent = nearbyEnemy.transform.parent != null ?
                nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

            EnemyVisionShare visionShare = enemyParent.GetComponent<EnemyVisionShare>();
            if (visionShare != null)
            {
                visionShare.BecomeAlly();
            }
            else
            {
                Debug.LogWarning($"[EnemyInteraction] {enemyParent.name}에 EnemyVisionShare가 없습니다!");
            }
        }

        CloseChoicePanel();
    }
    private void OnKillButtonClicked()
    {
        Debug.Log("죽이기!");

        if (nearbyEnemy != null)
        {
            GameObject enemyParent = nearbyEnemy.transform.parent != null ?
                nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

            Destroy(enemyParent);
        }

        CloseChoicePanel();
    }

    public bool IsDialogueOpen()
    {
        return isChoiceOpen;
    }
}
