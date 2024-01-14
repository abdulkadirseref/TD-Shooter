using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters movementParameters;
    [SerializeField] private InputReader inputReader = default;
    private Rigidbody2D rb;




    private void OnEnable()
    {
        inputReader.moveEvent += OnMove;
    }

    private void OnDisable()
    {
        inputReader.moveEvent -= OnMove;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {

    }


    void OnMove(Vector2 movement)
    {
        rb.velocity = movement * movementParameters.MoveSpeed;
    }

}
