

using System.Collections;
using Code.Scriptable_Objects;
using UnityEngine;

namespace Code.Triggers
{
    public class PowerUpRunner : MonoBehaviour
    {
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