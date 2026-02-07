using UnityEngine;
using Code.Scriptable_Objects;
using System.Collections.Generic;
using System.Collections;

namespace Code.Managers 
{
    public class PlayerManager : MonoBehaviour
    {
        [Header("Player Information")]
        public Collider2D playerCollider;
        public Rigidbody2D playerRigidbody2D;
        public Health playerHealth;
        [Header("Player Movement Information")]
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;
        public float speed = 8f;
        public float speedIncrement = 15f;
        public float jumpingPower = 16f;
        public JumpState jumpState;
        private Vector2 moveDirection;
        [Header("Player Abilities Information")]
        public AbilitiesData data;
        public List<float> cooldownAbilities;
        [Header("Dash Settings")]
        public float dashForce = 20f;
        public float dashDuration = 0.15f;

        private bool isDashing;
        private Vector2 dashDirection;
        [Header("ground Settings")]
        public bool ground = true;
        [Header("VFX Variables")]
        public ParticleSystem changePlayerVFX;
        public ParticleSystem jumpPlayerVFX;
        private void Awake()
        {
            playerHealth = GetComponent<Health>();
        }
        public void HandleMovement()
        {
            if (isDashing)
                return;
            playerRigidbody2D.linearVelocity = new Vector2(PlayerInputSystem.instance.horizontalInput * speed, playerRigidbody2D.linearVelocity.y);

        }
        public void HandleJump()
        {
          
           
            if (PlayerInputSystem.instance.jump && jumpState == JumpState.canJump)
            {
                playerRigidbody2D.linearVelocity = new Vector2(playerRigidbody2D.linearVelocity.x, jumpingPower);
                PlayerInputSystem.instance.HandleJumping();
                jumpState = JumpState.cannotJump;
                if (jumpPlayerVFX != null)
                {
                    jumpPlayerVFX.Play();
                }
            }
           
        }
        private void Update()
        {
            CheckGround();
            HandleDash();
            HandleJump();
            HandleMovement();
            
        }
        public void PerformSkills()
        {
            foreach (var effect in data.skills)
            {
                effect.Perform(playerRigidbody2D,0,PlayerInputSystem.instance);
            }
        }
        [ContextMenu("test decrement health")]
        public void HealthDecrement()
        {
            playerHealth.Decrement();
            if (playerHealth.currentHP > 0)
            {
                speed += speedIncrement;
            }
           
        }
       [ContextMenu("test increment health")]
        public void HealthIncrement()
        {
            if (playerHealth.currentHP >= playerHealth.maxHP)
                return;

            playerHealth.Increment();
            speed -= speedIncrement;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.CompareTag("Enemies"))
            {
                 HealthDecrement();
            }
        }
        public void HandleDash()
        {
            if (!PlayerInputSystem.instance.dash || isDashing)
                return;

            // Read input direction
            Vector2 inputDir = new Vector2(
                PlayerInputSystem.instance.horizontalInput,
                PlayerInputSystem.instance.verticalInput
            );

            // If no input, dash forward (right as default)
            if (inputDir == Vector2.zero)
                inputDir = Vector2.right;

            dashDirection = inputDir.normalized;

            StartCoroutine(DashCoroutine());

            // consume input
            PlayerInputSystem.instance.HandleDash();
        }
        private IEnumerator DashCoroutine()
        {
            isDashing = true;

            float originalGravity = playerRigidbody2D.gravityScale;
            playerRigidbody2D.gravityScale = 0f;

            playerRigidbody2D.linearVelocity = dashDirection * dashForce;

            yield return new WaitForSeconds(dashDuration);

            playerRigidbody2D.gravityScale = originalGravity;
            isDashing = false;
        }
        public void CheckGround()
        {
            if (playerRigidbody2D.linearVelocity.y == 0)
            {
                jumpState = JumpState.canJump;
            }
        }
        public void ChangePlayerSprite()
        {

        }
        public void ChangePlayerJumpPushToNegative()
        {
            if (changePlayerVFX != null)
            {
                changePlayerVFX.Play();
            }
           
            jumpingPower = -jumpingPower;
            playerRigidbody2D.gravityScale = -playerRigidbody2D.gravityScale;
        }
    }
   
  
}

public enum JumpState
{
    canJump,
    cannotJump
}
