using System;
using Code.Managers;
using UnityEngine;

namespace Code.Triggers
{
    public class ObstacleDamage : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                PlayerManager.Instance.HealthDecrement();
        }
    }
}