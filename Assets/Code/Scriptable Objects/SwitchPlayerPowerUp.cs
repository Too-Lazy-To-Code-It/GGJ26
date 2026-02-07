using Code.Managers;
using UnityEngine;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(menuName = "PowerUps/Switch Player")]
    public class SwitchPlayerPowerUp : PowerUpSo
    {
        public override void Apply()
        {
            GameManager.Instance.SwitchActivePlayer(SwitchReason.PowerUp);
        }

        public override void Revert()
        {
        }
    }
}