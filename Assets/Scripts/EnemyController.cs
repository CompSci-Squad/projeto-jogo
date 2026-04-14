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

            // pega o ponto de contato
            ContactPoint2D contact = collision.GetContact(0);

            // verifica se o player está ACIMA do inimigo
            if (contact.normal.y <= -0.5f)
            {
                // matou o inimigo
                Destroy(gameObject);

                // quique estilo Mario
                playerRb.linearVelocity = new Vector2(playerRb.linearVelocity.x, 10f);
            }
            else
            {
                // tomou dano
                collision.gameObject.GetComponent<PlayerController>().Die();
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