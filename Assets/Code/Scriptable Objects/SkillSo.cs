using UnityEngine;
using UnityEngine.AdaptivePerformance;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Skill", menuName = "Skills")]
    public abstract class SkillSo : ScriptableObject
    {
        public float coolDown;
        
        public abstract void  Perform();
    }
} 