using Code.Scriptable_Objects;
using UnityEngine;

namespace Code.Triggers
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] PowerUpSo powerUp;
        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;
            PowerUpRunner.Instance.Consume(powerUp);
            Destroy(gameObject);
        }
    }
}