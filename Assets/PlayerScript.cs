using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;

    public float recoilForce = 30f;
    public Animator animator;

    // Sprite flipping support
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
    }

    void Update()
    {
      
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

       
        if (movement.x != 0 || movement.y != 0)
            animator.SetBool("isRunning", true);
        else
            animator.SetBool("isRunning", false);

        
        if (movement.x < 0)
            spriteRenderer.flipX = true;
        else if (movement.x > 0)
            spriteRenderer.flipX = false;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        while (collision.collider.CompareTag("Monster"))
        {
           
            Vector2 direction = (transform.position - collision.transform.position).normalized;
            rb.AddForce(direction * recoilForce, ForceMode2D.Impulse);

           
            if (animator != null)
                animator.SetTrigger("Hurt");

            Debug.Log("Player is hurt");
        } else {
            break;
        }

        if (collision.collider.CompareTag("Animal"))
        {
            Debug.Log("MOO");
        }
    }
}