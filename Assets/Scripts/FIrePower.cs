using UnityEngine;

public class FirePower : MonoBehaviour
{
    public float duration = 10f;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().ActivateFirePower(duration);

            Destroy(gameObject);
        }
    }
}