using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulInteraction : MonoBehaviour
{
    [Header("UI References")]
    public GameObject interactionHint;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    [Header("Target Soul Choice UI")]
    public GameObject targetChoicePanel;
    public Button escapeButton;

    [Header("Enemy Button Panel (끄기용)")]
    public GameObject enemyButtonPanel;

    private Soul nearbySoul;
    private bool isDialogueOpen = false;
    private int currentDialogueIndex = 0;
    private string[] currentDialogues;

    private void Start()
    {
        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (targetChoicePanel != null)
            targetChoicePanel.SetActive(false);

        if (escapeButton != null)
        {
            escapeButton.onClick.AddListener(OnEscapeButtonClicked);
        }
    }

    private void Update()
    {
        if (isDialogueOpen)
        {
            if (Time.timeScale != 0f)
                Time.timeScale = 0f;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                NextDialogue();
            }
            return;
        }

        if (nearbySoul != null && !nearbySoul.IsCollected && Input.GetKeyDown(KeyCode.E))
        {
            OpenDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Soul soul = collision.GetComponent<Soul>();
        if (soul != null && !soul.IsCollected)
        {
            nearbySoul = soul;
            if (interactionHint != null)
                interactionHint.SetActive(true);

            Debug.Log($"[SoulInteraction] Soul 감지: {soul.soulName}");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Soul soul = collision.GetComponent<Soul>();

        if (soul != null && !soul.IsCollected && soul == nearbySoul)
        {
            if (interactionHint != null && !interactionHint.activeSelf && !isDialogueOpen)
            {
                interactionHint.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Soul soul = collision.GetComponent<Soul>();
        if (soul != null && soul == nearbySoul)
        {
            nearbySoul = null;
            if (interactionHint != null)
                interactionHint.SetActive(false);
        }
    }

    private void OpenDialogue()
    {
        if (nearbySoul == null) return;

        Debug.Log($"[SoulInteraction] {nearbySoul.soulName}과 대화 시작!");

        isDialogueOpen = true;
        currentDialogueIndex = 0;
        currentDialogues = nearbySoul.GetDialogues();

        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

        // Enemy Button Panel 강제로 끄기
        if (enemyButtonPanel != null)
            enemyButtonPanel.SetActive(false);

        Time.timeScale = 0f;

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
            // 대화 끝날 때도 Enemy Button Panel 끄기
            if (enemyButtonPanel != null)
                enemyButtonPanel.SetActive(false);

            if (nearbySoul.IsTargetSoul())
            {
                ShowTargetSoulChoice();
            }
            else
            {
                CollectSoul();
            }
        }
    }

    private void ShowTargetSoulChoice()
    {
        Debug.Log("[SoulInteraction] Target Soul 선택지 표시");

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // Enemy Button Panel 확실하게 끄기
        if (enemyButtonPanel != null)
            enemyButtonPanel.SetActive(false);

        if (targetChoicePanel != null)
            targetChoicePanel.SetActive(true);
    }

    private void OnEscapeButtonClicked()
    {
        Debug.Log("[SoulInteraction] 도망치기 선택!");

        if (targetChoicePanel != null)
            targetChoicePanel.SetActive(false);

        CollectTargetSoul();

        Time.timeScale = 1f;
        isDialogueOpen = false;
    }

    private void CollectSoul()
    {
        Debug.Log($"[SoulInteraction] {nearbySoul.soulName} 수집!");

        nearbySoul.Collect();

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        if (interactionHint != null)
            interactionHint.SetActive(false);

        isDialogueOpen = false;
        Time.timeScale = 1f;
    }

    private void CollectTargetSoul()
    {
        Debug.Log($"[SoulInteraction] 목표 영혼 구출! 적 소환 시작!");

        nearbySoul.Collect();

        if (EnemySpawnManager.Instance != null)
        {
            EnemySpawnManager.Instance.SpawnChasingEnemies();
        }
    }
}