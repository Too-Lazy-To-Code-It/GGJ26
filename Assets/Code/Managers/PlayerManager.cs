using UnityEngine;
using Code.Scriptable_Objects;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.InputSystem;

namespace Code.Managers 
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
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
        public GameObject PlatformAbility;
        public bool canUseMiniAbilityHuman = true ;
        public bool canUseMiniAbilityMask = true;
        public bool canUseMajorAbilityHuman = true;
        public bool canUseMajorAbilityMask = true;
        public float cooldownAbilities;
        
        [Header("Dash Settings")]
        public float dashForce = 20f;
        public float dashDuration = 0.15f;
        public bool canUseDash = true;
        private bool isDashing;
        private Vector2 dashDirection;
        [Header("ground Settings")]
        public bool ground = true;
        [Header("VFX Variables")]
        public ParticleSystem changePlayerVFX;
        public ParticleSystem jumpPlayerVFX;
        [Header("Freeze Ability")]
        public float maxFreezeTime = 3f;        // max duration you can freeze
        public float freezeCooldown = 2f;       // time it takes to refill after full use
        private float freezeMeter = 0f;
        private bool freezeActive = false;
        private bool freezeLocked = false;      // true if meter hit max
        private float freezeCooldownTimer = 0f;
        
        public GameObject projectilePrefab;
        private void Awake()
        {
            if(Instance == null)
                Instance = this;
            else
            {
                Destroy(this);
            }
            playerHealth = GetComponent<Health>();
        }
        public void HandleMovement()
        {
            if (isDashing)
                return;
            playerRigidbody2D.linearVelocity = new Vector2(PlayerInputSystem.instance.horizontalInput * speed, playerRigidbody2D.linearVelocity.y);

        }
        public void AntiHandleMovement()
        {
            if (isDashing)
                return;
            playerRigidbody2D.linearVelocity = new Vector2(-1 * speed, playerRigidbody2D.linearVelocity.y);
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
            HandleMiniAbility();
            HandleMajorAbility();
            HandleFreezeAbility();
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

            if (collision.collider.CompareTag("Walls"))
            {
                if (jumpState == JumpState.cannotJump && !PlayerInputSystem.instance.humanInCharge)
                {

                    DashFunction2(new Vector2(-0.5f, 0.5f));
                    playerRigidbody2D.gravityScale = -15;
                }
                if (jumpState == JumpState.cannotJump && PlayerInputSystem.instance.humanInCharge)
                {

                    DashFunction2(new Vector2(-0.5f, -0.5f));
                    playerRigidbody2D.gravityScale = 15;
                }

            }
            if (collision.collider.CompareTag("LWalls"))
            {

                if (jumpState == JumpState.cannotJump && !PlayerInputSystem.instance.humanInCharge)
                {
                    DashFunction2(new Vector2(0.5f, 0.5f));
                    playerRigidbody2D.gravityScale =-15;
                }
                if (jumpState == JumpState.cannotJump && PlayerInputSystem.instance.humanInCharge)
                {
                    DashFunction2(new Vector2(0.5f, -0.5f));
                    playerRigidbody2D.gravityScale = 15;
                }

            }
        }
        public void HandleDash()
        {

            if (canUseDash && PlayerInputSystem.instance.dash)
            {
                StartCoroutine(DashCoroutineFunction());
            }
        }
        public void DashFunction(Vector2 input)
        {
           
            Debug.Log("TESTAAAAAAAAAAA");
            // Read input direction
            Vector2 inputDir = input;

            // If no input, dash forward (right as default)
            if (inputDir == Vector2.zero)
                inputDir = Vector2.right;

            dashDirection = inputDir.normalized;

            StartCoroutine(DashCoroutine(dashForce));

            // consume input
            PlayerInputSystem.instance.HandleDash();
        }
        public void DashFunction2(Vector2 input)
        {

            Debug.Log("TESTAAAAAAAAAAA");
            // Read input direction
            Vector2 inputDir = input;

            // If no input, dash forward (right as default)
            if (inputDir == Vector2.zero)
                inputDir = Vector2.right;

            dashDirection = inputDir.normalized;

            StartCoroutine(DashCoroutine(25));

            // consume input
            PlayerInputSystem.instance.HandleDash();
        }
        private IEnumerator DashCoroutineFunction()
        {
            DashFunction(new Vector2(
                PlayerInputSystem.instance.horizontalInput,
                PlayerInputSystem.instance.verticalInput
            ));
            canUseDash = false;
            yield return new WaitForSeconds(5f);
            canUseDash = true;
        }
        public void HandleMiniAbility()
        {
           
            if (PlayerInputSystem.instance.miniAbilityInput && PlayerInputSystem.instance.humanInCharge == false && canUseMiniAbilityMask)
            {
                StartCoroutine(MiniAbilityCoroutineMask());
            }
            if (PlayerInputSystem.instance.miniAbilityInput && PlayerInputSystem.instance.humanInCharge == true && canUseMiniAbilityHuman)
            {
                //StartCoroutine(MiniAbilityCoroutineMask());
            }
        }

        private IEnumerator MiniAbilityCoroutineMask()
        {
            canUseMiniAbilityMask = false;
            GameObject gameObject = Instantiate(PlatformAbility, transform.position, transform.rotation);
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
            canUseMiniAbilityMask = true;
        }

        public void MajorAbilityMask()
        {
            canUseMajorAbilityMask = false;
            GameObject gameObject = Instantiate(projectilePrefab, transform.position, transform.rotation);
            canUseMajorAbilityMask = true;
        }
        public void HandleMajorAbility()
        {

            if (PlayerInputSystem.instance.abilityInput && PlayerInputSystem.instance.humanInCharge == false)
            {
                //StartCoroutine();
            }
            if (PlayerInputSystem.instance.abilityInput && PlayerInputSystem.instance.humanInCharge == true)
            {
                MajorAbilityMask();
            }
        }
        private IEnumerator MajorAbilityCoroutineMask()
        {
            canUseMajorAbilityMask = false;
            GameObject gameObject = Instantiate(PlatformAbility, transform.position, transform.rotation);
            yield return new WaitForSeconds(5f);
            Destroy(gameObject);
            canUseMiniAbilityMask = true;
        }
        private IEnumerator DashCoroutine(float dashForcee)
        {
            isDashing = true;

            float originalGravity = playerRigidbody2D.gravityScale;
            playerRigidbody2D.gravityScale = 0f;

            playerRigidbody2D.linearVelocity = dashDirection * dashForcee;

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
        public void ChangePlayerJumpPushToNegative()
        {
            if (changePlayerVFX != null)
            {
                changePlayerVFX.Play();
            }
           
            jumpingPower = -jumpingPower;
            playerRigidbody2D.gravityScale = -playerRigidbody2D.gravityScale;
        }


        void HandleFreezeAbility()
        {
            // Activate ability if input is pressed and not locked
            if (PlayerInputSystem.instance.stop && !freezeLocked)
            {
                freezeActive = true;
                AntiHandleMovement();
                freezeMeter += Time.deltaTime;

                if (freezeMeter >= maxFreezeTime)
                {
                    freezeMeter = maxFreezeTime;
                    freezeLocked = true;
                    freezeActive = false;        // auto-stop if fully used
                    freezeCooldownTimer = freezeCooldown;
                }
            }
            else
            {
                // Release early
                freezeActive = false;
            }

            // Handle cooldown if locked
            if (freezeLocked)
            {
                freezeCooldownTimer -= Time.deltaTime;
                if (freezeCooldownTimer <= 0f)
                {
                    freezeLocked = false;
                    freezeMeter = 0f;  // reset meter
                }
            }
        }
    }
   
  
}

public enum JumpState
{
    canJump,
    cannotJump
}
