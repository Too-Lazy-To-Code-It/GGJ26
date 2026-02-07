using UnityEngine;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "PowerUps", menuName = "PowerUps")]
    public abstract class PowerUpSo : ScriptableObject
    {
        public float duration;

        public abstract void Apply();
        public abstract void Revert();
    }
}