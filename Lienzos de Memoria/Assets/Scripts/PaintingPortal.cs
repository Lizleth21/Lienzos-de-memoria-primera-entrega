using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PaintingPortal : MonoBehaviour
{
    [Header("Portal Settings")]
    public string targetSceneName = "BananaMassacreLevel";
    public GameObject portalPrompt;       // Drag PortalPrompt UI here
    public AudioSource proximityMusic;    // Plays when player approaches the painting
    public AudioSource enterPortalSound;  // Optional whoosh sound when entering

    private bool isPlayerNear = false;
    private bool isEntering = false;
    private Transform playerTransform;

    void Start()
    {
        if (portalPrompt != null)
            portalPrompt.SetActive(false);
    }

    void Update()
    {
        // WCAG 2.2 Accessible Input: E, Spacebar, or Left Mouse Click
        bool interactPressed = Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0);

        if (isPlayerNear && !isEntering && interactPressed)
        {
            StartCoroutine(AbsorbPlayerIntoPainting());
        }
    }

    private IEnumerator AbsorbPlayerIntoPainting()
    {
        isEntering = true;
        if (portalPrompt != null) portalPrompt.SetActive(false);
        if (enterPortalSound != null) enterPortalSound.Play();

        // Absorption animation: pull player toward painting while spinning and shrinking
        if (playerTransform != null)
        {
            // Disable movement script during transition
            PlayerMovement movement = playerTransform.GetComponent<PlayerMovement>();
            if (movement != null) movement.enabled = false;

            float duration = 1.2f;
            float elapsed = 0f;
            Vector3 startPos = playerTransform.position;
            Vector3 startScale = playerTransform.localScale;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float t = elapsed / duration;

                playerTransform.position = Vector3.Lerp(startPos, transform.position, t);
                playerTransform.Rotate(0f, 0f, 360f * Time.deltaTime);
                playerTransform.localScale = Vector3.Lerp(startScale, Vector3.zero, t);

                yield return null;
            }
        }

        SceneManager.LoadScene(targetSceneName);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            playerTransform = other.transform;

            if (portalPrompt != null) portalPrompt.SetActive(true);
            if (proximityMusic != null && !proximityMusic.isPlaying) proximityMusic.Play();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isEntering)
        {
            isPlayerNear = false;
            if (portalPrompt != null) portalPrompt.SetActive(false);
            if (proximityMusic != null && proximityMusic.isPlaying) proximityMusic.Stop();
        }
    }
}
