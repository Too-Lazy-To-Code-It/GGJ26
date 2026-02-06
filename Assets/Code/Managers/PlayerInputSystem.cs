using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    CharacterControls characterControls;

    [SerializeField] int playerNumber;
    [Header("Player Movement Input")]


    [SerializeField] Vector2 movementInput;


    [Header("Player Mini Abilities Input")]




    private void OnEnable()
    {
        if (characterControls == null)
        {
            characterControls = new CharacterControls();
            characterControls.PlayerMovement.Movement.performed += ctx =>
            {
                if (ctx.control.device is Gamepad gamepad &&
                    gamepad == Gamepad.all[playerNumber]) 
                {
                    movementInput = ctx.ReadValue<Vector2>();
                }
            };
           
        }
        characterControls.Enable();
    }

}
