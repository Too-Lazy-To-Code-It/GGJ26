

using System;
using System.Collections;
using Code.Scriptable_Objects;
using UnityEngine;

namespace Code.Triggers
{
    public class PowerUpRunner : MonoBehaviour
    {
        public static PowerUpRunner Instance;

        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
            {
                Destroy(gameObject);
            }
        }

        public void Consume(PowerUpSo powerUp)
        {
            StartCoroutine(Run(powerUp));
        }

        IEnumerator Run(PowerUpSo powerUp)
        {
            powerUp.Apply();
            yield return new WaitForSeconds(powerUp.duration);
            powerUp.Revert();
        }
    }
}