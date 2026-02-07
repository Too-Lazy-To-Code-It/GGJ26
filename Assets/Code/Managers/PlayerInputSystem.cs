using UnityEngine;
using UnityEngine.InputSystem;
using Code.Managers;

public class PlayerInputSystem : MonoBehaviour
{
    public static PlayerInputSystem instance;
    CharacterControls characterControls;
    [SerializeField] int playerNumber;

    [Header("Player Movement Input")]
    [SerializeField]public  Vector2 movementInput;
    [SerializeField] public float horizontalInput;
    [SerializeField] public float verticalInput;
    [SerializeField] public float moveAmount;
    [SerializeField] public bool jump = false;
    [SerializeField] public bool dash = false;

    [Header("Player Mini/Strong Abilities Input")]
    [SerializeField] public bool abilityInput = false ;
    [SerializeField] public bool miniAbilityInput = false;



    private void Awake()
    {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            playerNumber = GameManager.Instance.ActivePlayerIndex;
    }
    private void OnEnable()
    {
        if (characterControls == null)
        {
            characterControls = new CharacterControls();
            characterControls.PlayerMovement.Movement.performed += ctx =>
            {
                if (ctx.control.device is Gamepad gamepad &&
                    gamepad == Gamepad.all[GameManager.Instance.ActivePlayerIndex]) 
                {
                    movementInput = ctx.ReadValue<Vector2>();
                }
            };
            characterControls.PlayerMovement.Jump.performed += jumping =>
            {
                if (jumping.control.device is Gamepad gamepad &&
                    gamepad == Gamepad.all[GameManager.Instance.ActivePlayerIndex])
                {
                    jump = true;
                }
            };
            characterControls.PlayerMovement.Dash.performed += Dash =>
            {
                if (Dash.control.device is Gamepad gamepad &&
                                    gamepad == Gamepad.all[GameManager.Instance.ActivePlayerIndex])
                {
                    dash = true;

                }

            };
            characterControls.PlayerAbilities.PlayerInputs.performed += i =>
            {
                if (i.control.device is Gamepad gamepad &&
                                    gamepad == Gamepad.all[GameManager.Instance.ActivePlayerIndex])
                {
                    abilityInput = true;
                }
            
            };
            characterControls.PlayerAbilities.PlayerInputs.performed += miniAbility =>
            {
                if (miniAbility.control.device is Gamepad gamepad &&
                                    gamepad != Gamepad.all[GameManager.Instance.ActivePlayerIndex])
                {
                    miniAbilityInput = true;
                }

            };
            

        }
        characterControls.Enable();
    }
    private void Update()
    {
        HandleAllInputs();
    }
    private void HandleAllInputs()
    {
        HandleAbility();
        HandleMiniAbility();
        HandleDash();
        HandleMovementInput();
       
    }
    
    public void SetActivePlayer(int playerIndex)
    {
        playerNumber = playerIndex;
    }
    private void HandleAbility()
    {
        if (abilityInput)
        {
            abilityInput = false;
            Debug.Log("test  ability");
        }
    }
    private void HandleMiniAbility()
    {
        if (miniAbilityInput)
        {
            miniAbilityInput = false;
            Debug.Log("test mini ability");
        }
    }
    public void HandleJumping()
    {
        if (jump)
        {
            jump = false;
            Debug.Log("test jump");
        }
    }
    public void HandleDash()
    {
        if (dash)
        {
            dash = false;
            Debug.Log("test jump");
        }
    }
    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));
        if (moveAmount <= 0.5 && moveAmount > 0)
        {
            moveAmount = 0.5f;
        }
        else if (moveAmount >0.5 && moveAmount <= 1)
        {
            moveAmount = 1;
        }
    }
}
