using UnityEngine;
using TMPro;

public class GuideDialogue : MonoBehaviour
{
    [Header("UI References")]
    public GameObject dialoguePanel;     // Drag DialoguePanel here
    public TextMeshProUGUI dialogueText; // Drag DialogueText here

    [Header("Audio & Music")]
    public AudioSource dialogueMusic;    // Plays mysterious music while dialogue is open
    public AudioSource pageFlipSound;    // Optional short sound when passing each line

    [Header("Dialogue Lines (English)")]
    [TextArea(2, 4)]
    public string[] dialogueLines = new string[]
    {
        "Welcome to the Museum of Colombian Memory! Beyond these quiet halls lies centuries of powerful history...",
        "Be careful, though. Visitors say this museum is full of strange phenomena—some paintings seem to have a life of their own.",
        "From the Thousand Days' War and the Banana Massacre to El Bogotazo and the 1991 Constitution, every canvas holds a secret.",
        "If you get too close to certain artworks, you might find yourself pulled straight into the past!",
        "Walk over to the main gallery painting and press E, Space, or Click when you are ready to step inside history."
    };

    private int currentLineIndex = 0;
    private bool isPlayerNear = false;
    private bool isDialogueOpen = false;

    void Start()
    {
        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }

    void Update()
    {
        // WCAG 2.2 Accessible Input: E key, Spacebar, or Left Mouse Click
        bool interactPressed = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (isPlayerNear && !isDialogueOpen && interactPressed)
        {
            StartDialogue();
        }
        else if (isDialogueOpen && interactPressed)
        {
            NextLine();
        }
    }

    private void StartDialogue()
    {
        isDialogueOpen = true;
        currentLineIndex = 0;
        if (dialoguePanel != null) dialoguePanel.SetActive(true);
        
        // Start mysterious dialogue music
        if (dialogueMusic != null && !dialogueMusic.isPlaying)
        {
            dialogueMusic.Play();
        }

        ShowCurrentLine();
    }

    private void NextLine()
    {
        currentLineIndex++;
        if (currentLineIndex < dialogueLines.Length)
        {
            ShowCurrentLine();
        }
        else
        {
            EndDialogue();
        }
    }

    private void ShowCurrentLine()
    {
        if (dialogueText != null) dialogueText.text = dialogueLines[currentLineIndex];
        if (pageFlipSound != null) pageFlipSound.Play();
    }

    private void EndDialogue()
    {
        isDialogueOpen = false;
        if (dialoguePanel != null) dialoguePanel.SetActive(false);

        // Stop dialogue music when closing the box
        if (dialogueMusic != null && dialogueMusic.isPlaying)
        {
            dialogueMusic.Stop();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (isDialogueOpen) EndDialogue();
        }
    }
}