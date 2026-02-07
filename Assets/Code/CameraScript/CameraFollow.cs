using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float FollowSpeed = 2f;
    public Transform target;
    
    void LateUpdate()
    {
        Vector3 newpos = new Vector3(target.position.x, target.position.y, -10f);
        transform.position = Vector3.Slerp(transform.position, newpos,FollowSpeed*Time.deltaTime);
    }
}
