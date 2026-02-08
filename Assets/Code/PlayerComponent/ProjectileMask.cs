using System;
using UnityEngine;

namespace Code.PlayerComponent
{
    public class ProjectileMask : MonoBehaviour
    {
        public float speed = 5f;

        private void Update()
        {
            transform.position+=transform.right * (Time.deltaTime * speed);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Walls"))
                Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}