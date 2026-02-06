using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputSystem : MonoBehaviour
{
    CharacterControls characterControls;
    [SerializeField] Vector2 movementInput;
    [SerializeField] float testInput;
    [SerializeField] int playerNumber;
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
