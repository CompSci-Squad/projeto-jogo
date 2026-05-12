using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float smoothSpeed = 5f;

    public float minY = 0f;

    void LateUpdate()
    {
        if (target == null) return;

        float targetY = target.position.y;

        if (targetY < minY)
        {
            targetY = minY;
        }

        Vector3 desiredPosition = new Vector3(
            target.position.x,
            targetY,
            transform.position.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}