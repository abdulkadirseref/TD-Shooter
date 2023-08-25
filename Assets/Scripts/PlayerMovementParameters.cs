using UnityEngine;

[CreateAssetMenu(fileName = "PlayerMovementParameters", menuName = "Custom/Movement Parameters")]
public class PlayerMovementParameters : ScriptableObject
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 10f;
    

    public float MoveSpeed => moveSpeed;
    public float JumpForce => jumpForce;
}
