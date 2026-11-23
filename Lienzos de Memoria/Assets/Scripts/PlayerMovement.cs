using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public AudioSource footstepAudio;
    public float speed = 5f;

    private Rigidbody2D rb;
    private Vector2 movePlyr;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (footstepAudio == null)
        {
            footstepAudio = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        // Supports both WASD and Arrow keys (WCAG 2.2 alternative input)
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw("Vertical");

        movePlyr = new Vector2(hor, ver).normalized;

        animator.SetFloat("Horizontal", hor);
        animator.SetFloat("Vertical", ver);

        // Play footstep sound only while moving
        HandleFootstepAudio();
    }

    private void FixedUpdate()
    {
        // Physics-based movement so colliders stop the player cleanly
        rb.MovePosition(rb.position + movePlyr * speed * Time.fixedDeltaTime);
    }

    private void HandleFootstepAudio()
    {
        if (footstepAudio == null) return;

        bool isMoving = movePlyr.sqrMagnitude > 0.01f;

        if (isMoving && !footstepAudio.isPlaying)
        {
            footstepAudio.Play();
        }
        else if (!isMoving && footstepAudio.isPlaying)
        {
            footstepAudio.Stop();
        }
    }
}