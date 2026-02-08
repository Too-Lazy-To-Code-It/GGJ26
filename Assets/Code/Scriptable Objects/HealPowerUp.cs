using Code.Managers;
using UnityEngine;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "PowerUps/Heal Player")]
    public class HealPowerUp : PowerUpSo
    {
        public override void Apply()
        {
            PlayerManager.Instance.HealthIncrement();
        }

        public override void Revert()
        {
            
        }
    }
}