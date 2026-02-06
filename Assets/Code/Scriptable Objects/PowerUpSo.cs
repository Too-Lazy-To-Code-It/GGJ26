using UnityEngine;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "PowerUps", menuName = "PowerUps")]
    public abstract class PowerUpSo : ScriptableObject
    {
        public void Consume()
        {
            Destroy(this);
        }
    }
}