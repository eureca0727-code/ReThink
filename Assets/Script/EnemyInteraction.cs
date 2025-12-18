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

<<<<<<< HEAD
    [Header("ï¿½âº» ï¿½ï¿½ï¿½ (ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ç°¡ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½)")]
    [TextArea(2, 5)]
    public string[] defaultDialogues = {
        "[ï¿½ï¿½] !! ",
        "[ï¿½ï¿½] ",
=======
    [Header("±âº» ´ë»ç (Àû¿¡°Ô ´ë»ç°¡ ¾øÀ» ¶§)")]
    [TextArea(2, 5)]
    public string[] defaultDialogues = {
        "[Àû] !! ",
        "[³ª] ",
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    };

    private GameObject nearbyEnemy;
    private bool isChoiceOpen = false;
    private int currentDialogueIndex = 0;
    private string[] currentDialogues;
<<<<<<< HEAD
    public static bool IsInDialogue = false;
=======
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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
<<<<<<< HEAD
        IsInDialogue = true; // ëŒ€í™” ì‹œìž‘ ì‹œ true
        isChoiceOpen = true;
        Time.timeScale = 0f; // ê²Œìž„ ì¼ì‹œì •ì§€

        Debug.Log("ï¿½ï¿½È­ ï¿½ï¿½ï¿½ï¿½!");
=======
        Debug.Log("´ëÈ­ ½ÃÀÛ!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

        isChoiceOpen = true;
        currentDialogueIndex = 0;

        GameObject enemyParent = nearbyEnemy.transform.parent != null ?
            nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

        EnemyDialogue enemyDialogue = enemyParent.GetComponent<EnemyDialogue>();

        if (enemyDialogue != null)
        {
            currentDialogues = enemyDialogue.GetDialogues();
<<<<<<< HEAD
            Debug.Log("ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½: " + enemyParent.name);
=======
            Debug.Log("Àû °íÀ¯ ´ë»ç »ç¿ë: " + enemyParent.name);
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
        }
        else
        {
            currentDialogues = defaultDialogues;
<<<<<<< HEAD
            Debug.Log("ï¿½âº» ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½");
=======
            Debug.Log("±âº» ´ë»ç »ç¿ë");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
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
<<<<<<< HEAD
        Debug.Log("ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ Ç¥ï¿½ï¿½!");
=======
        Debug.Log("¼±ÅÃÁö Ç¥½Ã!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

        GameObject enemyParent = nearbyEnemy.transform.parent != null ?
            nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

        EnemyChoiceConfig choiceConfig = enemyParent.GetComponent<EnemyChoiceConfig>();

        if (dialogueText != null)
<<<<<<< HEAD
            dialogueText.text = "ï¿½î¶»ï¿½ï¿½ ï¿½Ò±ï¿½?";
=======
            dialogueText.text = "¾î¶»°Ô ÇÒ±î?";
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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
<<<<<<< HEAD
        IsInDialogue = false; // ëŒ€í™” ì¢…ë£Œ ì‹œ false
        isChoiceOpen = false;
        
        Debug.Log("ï¿½ï¿½È­ ï¿½ï¿½ï¿½ï¿½!");
=======
        Debug.Log("´ëÈ­ Á¾·á!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

        isChoiceOpen = false;

        if (choicePanel != null)
            choicePanel.SetActive(false);

        nearbyEnemy = null;
<<<<<<< HEAD
        if (QTEManager.Instance == null || !QTEManager.Instance.IsQTEActive())
        {
            Time.timeScale = 1f;
        }
=======
        Time.timeScale = 1f;
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
    }

    private void OnAllyButtonClicked()
    {
<<<<<<< HEAD
        Debug.Log("ï¿½ì¸®ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½!");
=======
        Debug.Log("¿ì¸®ÆíÀ¸·Î!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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
<<<<<<< HEAD
                Debug.LogWarning($"[EnemyInteraction] {enemyParent.name}ï¿½ï¿½ EnemyVisionShareï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½!");
=======
                Debug.LogWarning($"[EnemyInteraction] {enemyParent.name}¿¡ EnemyVisionShare°¡ ¾ø½À´Ï´Ù!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0
            }
        }

        CloseChoicePanel();
    }

    private void OnKillButtonClicked()
    {
<<<<<<< HEAD
        Debug.Log("ï¿½ï¿½ï¿½Ì±ï¿½!");
=======
        Debug.Log("Á×ÀÌ±â!");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

        if (nearbyEnemy != null)
        {
            GameObject enemyParent = nearbyEnemy.transform.parent != null ?
                nearbyEnemy.transform.parent.gameObject : nearbyEnemy;

<<<<<<< HEAD
            // EnemyPatrolï¿½ï¿½ Die() ï¿½Þ¼ï¿½ï¿½ï¿½ È£ï¿½ï¿½
            EnemyPatrol enemyPatrol = enemyParent.GetComponent<EnemyPatrol>();
            if (enemyPatrol != null)
            {
                enemyPatrol.Die();  // Die()ï¿½ï¿½ Ä«ï¿½ï¿½Æ® ï¿½ï¿½ï¿½ï¿½ + Destroy Ã³ï¿½ï¿½
            }
            else
            {
                // EnemyPatrolï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½ï¿½ (ï¿½Ù¸ï¿½ Å¸ï¿½ï¿½ï¿½ï¿½ ï¿½ï¿½)
                Debug.LogWarning($"{enemyParent.name}ï¿½ï¿½ EnemyPatrol ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Æ®ï¿½ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½Ï´ï¿½.");
=======
            // EnemyPatrolÀÇ Die() ¸Þ¼­µå È£Ãâ
            EnemyPatrol enemyPatrol = enemyParent.GetComponent<EnemyPatrol>();
            if (enemyPatrol != null)
            {
                enemyPatrol.Die();  // Die()°¡ Ä«¿îÆ® Áõ°¡ + Destroy Ã³¸®
            }
            else
            {
                // EnemyPatrolÀÌ ¾ø´Â °æ¿ì (´Ù¸¥ Å¸ÀÔÀÇ Àû)
                Debug.LogWarning($"{enemyParent.name}¿¡ EnemyPatrol ÄÄÆ÷³ÍÆ®°¡ ¾ø½À´Ï´Ù.");
>>>>>>> 1ab43000bb30df9695c6a15123d97ed3b7f1bbe0

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