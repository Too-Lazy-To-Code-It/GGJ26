using Code.Scriptable_Objects;
using UnityEngine;

namespace Code.Triggers
{
    public class PowerUpPickup : MonoBehaviour
    {
        [SerializeField] PowerUpSo powerUp;
        void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("WALLAH RANI LENA WALLLAH");
            if (!other.CompareTag("Player"))
                return;
            PowerUpRunner.Instance.Consume(powerUp);
            Debug.Log("WALLAH RANI LENA WALLLAH TRUST TRUST");
            Destroy(gameObject);
        }
    }
}