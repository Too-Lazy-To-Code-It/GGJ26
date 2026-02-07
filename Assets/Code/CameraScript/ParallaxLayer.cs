using UnityEngine;

namespace Code.CameraScript
{
    public class ParallaxLayer : MonoBehaviour
    {
        [SerializeField] Transform cameraTransform;
        [SerializeField, Range(0f, 1f)] float parallaxFactor = 0.3f;

        float lastCameraX;

        void Start()
        {
            lastCameraX = cameraTransform.position.x;
        }

        void LateUpdate()
        {
            float deltaX = cameraTransform.position.x - lastCameraX;

            transform.position += Vector3.right * deltaX * parallaxFactor;

            lastCameraX = cameraTransform.position.x;
        }
    }
}