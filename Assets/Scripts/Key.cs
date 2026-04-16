using UnityEngine;

public class Key : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.instance.hasKey = true;

            // 🔥 MOSTRA O ÍCONE
            GameManager.instance.keyIcon.SetActive(true);

            Destroy(gameObject);
        }
    }
}