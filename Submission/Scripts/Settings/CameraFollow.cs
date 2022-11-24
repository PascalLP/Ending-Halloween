using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.5f;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        //offset = transform.position - target.position;
    }


    void FixedUpdate()
    {
        if (!playerHealth.isDead)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed*Time.deltaTime);
            transform.position = smoothedPosition;
        }
    }
}
