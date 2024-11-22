using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters movementParameters;
    [SerializeField] private InputReader inputReader = default;
    [SerializeField] private Animator animator;
    [SerializeField] private GunManager gunManager;
    private Rigidbody2D rb;
    private bool facingRight = true;



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

    private void FixedUpdate()
    {
        if (facingRight == false && rb.linearVelocity.x > 0)
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;

            foreach (Transform item in gunManager.gunSlots)
            {
                item.transform.rotation = Quaternion.Euler(0f, 0f, 0f);

                Vector2 newLocalScale = item.transform.localScale;
                newLocalScale.x *= -1;
                item.transform.localScale = newLocalScale;
            }
        }
        else if (facingRight == true && rb.linearVelocity.x < 0)
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;

            foreach (Transform item in gunManager.gunSlots)
            {
                item.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                Vector2 newLocalScale = item.transform.localScale;
                newLocalScale.x *= -1;
                item.transform.localScale = newLocalScale;
            }
        }
    }


    void OnMove(Vector2 movement)
    {
        rb.linearVelocity = movement * movementParameters.MoveSpeed;
        if (Mathf.Abs(rb.linearVelocityX) > 0 || Mathf.Abs(rb.linearVelocityY) > 0)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

}
