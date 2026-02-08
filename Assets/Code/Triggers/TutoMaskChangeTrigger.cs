using System;
using Code.Managers;
using UnityEngine;

namespace Code.Triggers
{
    public class TutoMaskChangeTrigger : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (name == "final")
                {
                    GameManager.Instance.categoryResolver = "Human";
                    return;
                }
                TutorialManager.Instance.SwapSprites();
            }
                
        }
    }
}