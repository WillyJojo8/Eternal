using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Jugador
    public float smoothSpeed = 0.125f;
    public Vector3 offset; // 0, 0, -10 por defecto

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothed = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothed;
    }
}