using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    private int direction = 1;

    public Transform groundCheckLeft;
    public Transform groundCheckRight;
    public float checkDistance = 0.7f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        CheckGround();
        Move();
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(direction * speed, rb.linearVelocity.y);
    }

    void CheckGround()
    {
        Transform activeCheck = direction == 1 ? groundCheckRight : groundCheckLeft;

        RaycastHit2D groundInfo = Physics2D.Raycast(
            activeCheck.position,
            Vector2.down,
            checkDistance,
            groundLayer
        );

        if (!groundInfo.collider)
        {
            Flip();
        }
    }

    void Flip()
    {
        direction *= -1;
    }

        void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();

            float playerY = collision.transform.position.y;
            float enemyY = transform.position.y;

            // 👇 PLAYER PULOU EM CIMA
            if (playerY > enemyY + 0.3f && playerRb.linearVelocity.y < 0)
            {
                Destroy(gameObject);

                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f);
            }
            else
            {
                // 👊 IMPACTO LATERAL
                float dir = collision.transform.position.x > transform.position.x ? 1 : -1;

                Vector2 force = new Vector2(dir * 8f, 6f);

                player.ApplyKnockback(force);

                rb.linearVelocity = new Vector2(-dir * 4f, 4f);

                player.TakeDamage();
            }
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheckLeft != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(groundCheckLeft.position, groundCheckLeft.position + Vector3.down * checkDistance);
        }

        if (groundCheckRight != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(groundCheckRight.position, groundCheckRight.position + Vector3.down * checkDistance);
        }
    }
}