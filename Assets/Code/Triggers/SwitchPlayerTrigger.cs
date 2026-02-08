using System;
using Code.Managers;
using UnityEngine;

namespace Code.Triggers
{
    public class SwitchPlayerTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
                GameManager.Instance.SwitchActivePlayer(SwitchReason.RNG);
        }
    }
}