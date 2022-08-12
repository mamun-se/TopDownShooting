using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothSpeed = 0.125f;
    public float clampval = 0.25f;
    public Vector3 offset;
    public Transform target = null;

    void FixedUpdate ()
    {
       // Transform target = GameManager.gameManagerInstance.GetCurrentPlayerTransform();
        Vector3 desiredPosition = target.position + offset;
        transform.position = desiredPosition;
        transform.LookAt(target);
    }
}
