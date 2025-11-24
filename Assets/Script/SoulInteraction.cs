using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SoulInteraction : MonoBehaviour
{
    [Header("UI References")]
    public GameObject interactionHint;
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

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

        if (nearbySoul != null && Input.GetKeyDown(KeyCode.E))
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

        Debug.Log($"{nearbySoul.soulName}과 대화 시작!");

        isDialogueOpen = true;
        currentDialogueIndex = 0;
        currentDialogues = nearbySoul.GetDialogues();

        if (interactionHint != null)
            interactionHint.SetActive(false);

        if (dialoguePanel != null)
            dialoguePanel.SetActive(true);

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
            CollectSoul();
        }
    }

    private void CollectSoul()
    {
        Debug.Log($"{nearbySoul.soulName} 수집!");

        // 영혼 수집 처리
        nearbySoul.Collect();

        // 대화 패널만 닫기
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);

        // interactionHint는 끄지 않음! (남겨둠)
        // if (interactionHint != null)
        //     interactionHint.SetActive(false);

        isDialogueOpen = false;
        Time.timeScale = 1f;

        // nearbySoul을 null로 만들지 않음 (계속 추적)
        // nearbySoul = null;
    }
}
