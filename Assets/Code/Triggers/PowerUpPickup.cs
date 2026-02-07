using Code.Scriptable_Objects;
using UnityEngine;

namespace Code.Triggers
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] PowerUpSo powerUp;
        [SerializeField] PowerUpRunner runner;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            runner.Consume(powerUp);
            Destroy(gameObject);
        }
    }
}