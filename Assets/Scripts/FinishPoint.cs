using UnityEngine;

public class FinishPoint : MonoBehaviour
{
    private Door door;

    void Start()
    {
        door = GetComponent<Door>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && door.IsOpen())
        {
            GameManager.instance.WinGame();
        }
    }
}