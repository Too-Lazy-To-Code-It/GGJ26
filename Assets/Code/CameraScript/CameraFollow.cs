using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float followSpeed = 5f;

    [Header("X Bounds")]
    [SerializeField] float minX;
    [SerializeField] float maxX;

    float fixedY;
    float fixedZ;

    void Start()
    {
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        float targetX = Mathf.Clamp(target.position.x, minX, maxX);

        Vector3 desiredPos = new Vector3(targetX, fixedY, fixedZ);

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPos,
            followSpeed * Time.deltaTime
        );
    }
}