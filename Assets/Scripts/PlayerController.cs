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
    private bool isPowered = false;

    public bool IsPowered()
    {
        return isPowered;
    }

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

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(Vector2.up * 10f, ForceMode2D.Impulse);

        this.enabled = false;

        if (fellOff)
        {
            GameManager.instance.lives = 0;

            Invoke("TriggerGameOver", 1.5f);
        }
        else
        {
            Invoke("Respawn", 1.5f);
        }
    }

    void TriggerGameOver()
    {
        GameManager.instance.LoseLife(); 
    }

    void Respawn()
    {
        GameManager.instance.LoseLife();

        transform.position = new Vector2(0, -3);

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

        rb.linearVelocity = Vector2.zero;

        rb.AddForce(force, ForceMode2D.Impulse);

        StartCoroutine(KnockbackRoutine());
    }

    IEnumerator KnockbackRoutine()
    {
        this.enabled = false;

        yield return new WaitForSeconds(0.3f); 

        this.enabled = true;
        isKnocked = false;
    }

    public void ActivateFirePower(float duration)
    {
        if (isPowered) return;

        StartCoroutine(FirePowerRoutine(duration));
    }

    IEnumerator FirePowerRoutine(float duration)
    {
        isPowered = true;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float timer = 0f;

        while (timer < duration)
        {
            
            sr.color = Color.Lerp(Color.white, Color.orange, Mathf.PingPong(Time.time * 2f, 1));

            timer += Time.deltaTime;
            yield return null;
        }

        
        sr.color = Color.white;
        isPowered = false;
    }
}