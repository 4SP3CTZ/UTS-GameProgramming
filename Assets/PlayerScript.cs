using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public float recoilForce = 8f;
    public float recoilDuration = 0.2f;
    private float recoilTimer = 0f;

    public Animator animator;
    private SpriteRenderer spriteRenderer;

    private Vector2 recoilDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
     
        if (recoilTimer <= 0)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            movement = movement.normalized;
        }

        
        animator.SetBool("isRunning", movement.magnitude > 0);

        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else if (movement.x > 0)
            spriteRenderer.flipX = false;

        
        if (recoilTimer > 0)
            recoilTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        if (recoilTimer > 0)
        {
            rb.velocity = recoilDirection * recoilForce;
        }
        else
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Monster"))
        {
        
            recoilDirection = (rb.position - collision.GetContact(0).point).normalized;

            recoilTimer = recoilDuration;

            animator.SetTrigger("Hurt");
            Debug.Log("Player is hurt");
        }

        if (collision.collider.CompareTag("Animal"))
        {
            Debug.Log("MOO");
        }
    }
}