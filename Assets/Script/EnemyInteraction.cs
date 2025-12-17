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

    [Header("�⺻ ��� (������ ��簡 ���� ��)")]
    [TextArea(2, 5)]
    public string[] defaultDialogues = {
        "[��] !! ",
        "[��] ",
    };

    private GameObject nearbyEnemy;
    private bool isChoiceOpen = false;
    private int currentDialogueIndex = 0;
    private string[] currentDialogues;
    public static bool IsInDialogue = false;

    private void Start()
    {
        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (choicePanel != null)
            choicePanel.SetActive(false);

        if (allyButton != null)
        {
            allyButton.onClick.AddListener(OnAllyButtonClicked);
        }

        if (killButton != null)
        {
            killButton.onClick.AddListener(OnKillButtonClicked);
        }

        if (buttonPanel != null)
            buttonPanel.SetActive(false);
    }

    private void Update()
    {
        if (isChoiceOpen)
        {
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
        IsInDialogue = true; // 대화 시작 시 true
        isChoiceOpen = true;
        Time.timeScale = 0f; // 게임 일시정지

        Debug.Log("��ȭ ����!");

        isChoiceOpen = true;
        currentDialogueIndex = 0;

        GameObject enemyParent = nearbyEnemy.transform.parent != null ?
            nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

        EnemyDialogue enemyDialogue = enemyParent.GetComponent<EnemyDialogue>();

        if (enemyDialogue != null)
        {
            currentDialogues = enemyDialogue.GetDialogues();
            Debug.Log("�� ���� ��� ���: " + enemyParent.name);
        }
        else
        {
            currentDialogues = defaultDialogues;
            Debug.Log("�⺻ ��� ���");
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
        Debug.Log("������ ǥ��!");

        GameObject enemyParent = nearbyEnemy.transform.parent != null ?
            nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

        EnemyChoiceConfig choiceConfig = enemyParent.GetComponent<EnemyChoiceConfig>();

        if (dialogueText != null)
            dialogueText.text = "��� �ұ�?";

        if (buttonPanel != null)
            buttonPanel.SetActive(true);

        if (allyButton != null)
        {
            if (choiceConfig != null)
            {
                allyButton.gameObject.SetActive(choiceConfig.canBecomeAlly);
            }
            else
            {
                allyButton.gameObject.SetActive(true);
            }
        }

        if (killButton != null)
        {
            if (choiceConfig != null)
            {
                killButton.gameObject.SetActive(choiceConfig.canBeKilled);
            }
            else
            {
                killButton.gameObject.SetActive(true);
            }
        }
    }

    private void CloseChoicePanel()
    {
        IsInDialogue = false; // 대화 종료 시 false
        isChoiceOpen = false;
        
        Debug.Log("��ȭ ����!");

        isChoiceOpen = false;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        nearbyEnemy = null;
        if (QTEManager.Instance == null || !QTEManager.Instance.IsQTEActive())
        {
            Time.timeScale = 1f;
        }
    }

    private void OnAllyButtonClicked()
    {
        Debug.Log("�츮������!");

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
                Debug.LogWarning($"[EnemyInteraction] {enemyParent.name}�� EnemyVisionShare�� �����ϴ�!");
            }
        }

        CloseChoicePanel();
    }

    private void OnKillButtonClicked()
    {
        Debug.Log("���̱�!");

        if (nearbyEnemy != null)
        {
            GameObject enemyParent = nearbyEnemy.transform.parent != null ?
                nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

            // EnemyPatrol�� Die() �޼��� ȣ��
            EnemyPatrol enemyPatrol = enemyParent.GetComponent<EnemyPatrol>();
            if (enemyPatrol != null)
            {
                enemyPatrol.Die();  // Die()�� ī��Ʈ ���� + Destroy ó��
            }
            else
            {
                // EnemyPatrol�� ���� ��� (�ٸ� Ÿ���� ��)
                Debug.LogWarning($"{enemyParent.name}�� EnemyPatrol ������Ʈ�� �����ϴ�.");

                if (EnemySpawnManager.Instance != null)
                {
                    EnemySpawnManager.Instance.RegisterEnemyKill();
                }
                Destroy(enemyParent);
            }
        }

        CloseChoicePanel();
    }

    public bool IsDialogueOpen()
    {
        return isChoiceOpen;
    }
}