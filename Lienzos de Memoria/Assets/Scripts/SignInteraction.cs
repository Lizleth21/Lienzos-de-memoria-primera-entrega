using UnityEngine;
using UnityEngine.SceneManagement;

public class SignInteraction : MonoBehaviour
{
    [Header("UI References")]
    public GameObject interactPrompt;    // Drag InteractPrompt here
    public GameObject signMessagePanel;  // Drag SignMessagePanel here

    [Header("Scene Settings")]
    public string museumSceneName = "SampleScene";

    private bool isPlayerNear = false;
    private bool isSignOpen = false;

    void Start()
    {
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (signMessagePanel != null) signMessagePanel.SetActive(false);
    }

    void Update()
    {
        // WCAG 2.2 Accessible Input: E, Spacebar, or Left Mouse Click
        bool interactPressed = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (isPlayerNear && !isSignOpen && interactPressed)
        {
            OpenSign();
        }
        else if (isSignOpen && Input.GetKeyDown(KeyCode.R))
        {
            ReturnToMuseum();
        }
        else if (isSignOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSign();
        }
    }

    private void OpenSign()
    {
        isSignOpen = true;
        if (interactPrompt != null) interactPrompt.SetActive(false);
        if (signMessagePanel != null) signMessagePanel.SetActive(true);
    }

    public void CloseSign()
    {
        isSignOpen = false;
        if (signMessagePanel != null) signMessagePanel.SetActive(false);
        if (isPlayerNear && interactPrompt != null) interactPrompt.SetActive(true);
    }

    public void ReturnToMuseum()
    {
        SceneManager.LoadScene(museumSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            if (!isSignOpen && interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            if (interactPrompt != null) interactPrompt.SetActive(false);
            if (isSignOpen) CloseSign();
        }
    }
}