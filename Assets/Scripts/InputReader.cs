using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Game/Input Reader")]
public class InputReader : ScriptableObject, CustomInput.IPlayerActions
{

    public event UnityAction<Vector2> moveEvent = delegate { };


    private CustomInput input;



    private void OnEnable()
    {

        if (input == null)
        {
            input = new CustomInput();
            input.Player.SetCallbacks(this);
        }

        input.Enable();
        Debug.Log("enabled");
    }



    private void OnDisable()
    {
        input.Disable();
    }


    public void OnMovement(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
        // Debug.Log(context.ReadValue<Vector2>());
    }
}
