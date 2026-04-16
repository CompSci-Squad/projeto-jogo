using UnityEngine;

public class Door : MonoBehaviour
{
    public Sprite closedSprite;
    public Sprite openSprite;

    private SpriteRenderer sr;
    private bool isOpen = false;

    void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        sr.sprite = closedSprite;
    }

    void Update()
    {
        if (!isOpen && GameManager.instance.hasKey)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        isOpen = true;
        sr.sprite = openSprite;
    }

    public bool IsOpen()
    {
        return isOpen;
    }
}