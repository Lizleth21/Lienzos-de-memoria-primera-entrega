using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private BoxCollider2D bodyBox;
    private RaycastHit2D hit;
    private Vector3 movePlyr;
    public Animator animator;
    public float speed = 5f;

    private void Start()
    {
        bodyBox = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        float hor = Input.GetAxisRaw("Horizontal");
        float ver = Input.GetAxisRaw ("Vertical");

        // Calculate movement direction and normalize it
        Vector3 movePlyr = new Vector3(hor, ver, 0).normalized;

        animator.SetFloat ("Horizontal", hor);
        animator.SetFloat ("Vertical", ver);

        // Apply movement using Time.deltaTime for smooth speed
        transform.Translate(movePlyr * speed * Time.deltaTime);
    }

}
