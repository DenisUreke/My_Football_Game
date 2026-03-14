using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    private PlayerStats playerStats;
    private GameValues gameValues;

    public Rigidbody2D rb;

    private Vector2 movementInput;
    private Vector2 currentPlayerVelocity;
    private Vector2 currentPlayerDirection;

    private float playerSpeed;
    private float playerAcceleration;

    private float maxSpeed;
    private float maxAcceleration;
    private float maxFriction;

    public Vector2 CurrentPlayerDirection => currentPlayerDirection;
    public Vector2 CurrentPlayerVelocity => currentPlayerVelocity;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
        gameValues = FindFirstObjectByType<GameValues>();
    }

    private void Start()
    {
        playerSpeed = playerStats.Speed / 100f;
        playerAcceleration = playerStats.Acceleration / 100f;

        maxSpeed = gameValues.MaxPlayerSpeed;
        maxAcceleration = gameValues.MaxAccelleration;
        maxFriction = gameValues.MaxFriction;
    }

    private void FixedUpdate()
    {
        Vector2 targetVelocity;
        float rate;

        if (movementInput.sqrMagnitude < 0.01f)
        {
            targetVelocity = Vector2.zero;
            rate = maxFriction;
        }
        else
        {
            targetVelocity = movementInput.normalized * playerSpeed * maxSpeed;

            float alignment = Vector2.Dot
                (
                currentPlayerVelocity.normalized,
                movementInput.normalized
                );

            if (alignment < 0 && movementInput.sqrMagnitude > 0.01f)
            {
                rate = maxFriction;
            }
            else 
            {
                rate = playerAcceleration * maxAcceleration;
            }
        }
        // calculate velcotu to know how far to move the player
        currentPlayerVelocity = Vector2.MoveTowards(
            currentPlayerVelocity,
            targetVelocity,
            rate * Time.fixedDeltaTime 
        );

        Vector2 newPosition = rb.position + currentPlayerVelocity * Time.fixedDeltaTime; // Calculate new position based on current velocity
        rb.MovePosition(newPosition); // Move the Rigidbody2D to the new position
        if (currentPlayerVelocity.sqrMagnitude > 0.01f)
        {
            currentPlayerDirection = currentPlayerVelocity.normalized;
        }
    }

    public void SetMoveInput(Vector2 input) // Called by ControlManager to update movement input
    {
        movementInput = input;
    }
}
