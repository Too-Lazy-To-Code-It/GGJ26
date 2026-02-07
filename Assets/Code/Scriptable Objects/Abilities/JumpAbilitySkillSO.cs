using UnityEngine;


namespace Code.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "JumpAbility", menuName = "Skills/Jump")]

    public class JumpAbilitySkillSO : SkillSo
    {
        public override void Perform(Rigidbody2D rigidbody2D,float cooldown,PlayerInputSystem playerInputSystem) 
        {
            if (cooldown <= 0 && playerInputSystem.jump)
            {
                rigidbody2D.linearVelocity = new Vector2(rigidbody2D.linearVelocity.x, 7f);
                playerInputSystem.HandleJumping();
            }
            
        }
     
    }
}
