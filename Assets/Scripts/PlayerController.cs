using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 8f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isDead = false;
    private bool isKnocked = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(move * speed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        if (transform.position.y < -10f)
        {
            Die(true);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    public void Die(bool fellOff = false)
    {
        if (isDead) return;
        isDead = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // zera velocidade
        rb.linearVelocity = Vector2.zero;

        // quique pra cima
        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        // desativa controle
        this.enabled = false;

        if (fellOff)
        {
            // perdeu todas as vidas direto
            GameManager.instance.lives = 0;

            // chama game over depois de um tempo
            Invoke("TriggerGameOver", 1.5f);
        }
        else
        {
            // morte normal (inimigo)
            Invoke("Respawn", 1.5f);
        }
    }

    void TriggerGameOver()
    {
        GameManager.instance.LoseLife(); // vai cair em 0 → Game Over
    }

    void Respawn()
    {
        // se estiver usando vidas:
        GameManager.instance.LoseLife();

        // reinicia posição
        transform.position = new Vector2(0, -3);

        // reativa controle
        this.enabled = true;

        isDead = false;
    }

    public void TakeDamage()
    {
        GameManager.instance.LoseLife();
    }
    
    public void ApplyKnockback(Vector2 force)
    {
        if (isKnocked) return;

        isKnocked = true;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        // zera velocidade antes do impacto
        rb.linearVelocity = Vector2.zero;

        // aplica força
        rb.AddForce(force, ForceMode2D.Impulse);

        // desativa controle temporariamente
        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        this.enabled = false;

        yield return new WaitForSeconds(0.3f); // tempo sem controle

        this.enabled = true;
        isKnocked = false;
    }
}