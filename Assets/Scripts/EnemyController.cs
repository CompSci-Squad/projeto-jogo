using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 2f;
    private int direction = 1;

    public Transform groundCheck;
    public float checkDistance = 1f;
    public LayerMask groundLayer;

    void Update()
    {
        CheckGround(); // 👈 CHECA ANTES
        Move();
    }

    void Move()
    {
        transform.Translate(Vector2.right * direction * speed * Time.deltaTime);
    }

    void CheckGround()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(
            groundCheck.position,
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

        // vira sprite
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;

        // move o GroundCheck pro outro lado IMEDIATAMENTE
        groundCheck.localPosition = new Vector2(
            -groundCheck.localPosition.x,
            groundCheck.localPosition.y
        );
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(
                groundCheck.position,
                groundCheck.position + Vector3.down * checkDistance
            );
        }
    }
}