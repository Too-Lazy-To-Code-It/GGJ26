using UnityEngine;

namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "StopPlayer", menuName = "Skills/StopPlayer")]
    public class StopPlayerSkill : SkillSo
    {
        public override void Perform(Rigidbody2D rigidbody2D,float cooldown, PlayerInputSystem playerinputsystem)
        {
            throw new System.NotImplementedException();
        }
    }
}