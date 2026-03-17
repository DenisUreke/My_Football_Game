using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallControl : MonoBehaviour
{
    [SerializeField] PlayerUnit[] players;
    private PlayerUnit currentBallHolder;

    private BallState currentBallState;

    public BallState CurrentBallState => currentBallState;

    private Rigidbody2D rb;
    private DistanceCalculator distanceCalculator;
    private GameValues gameValues;

    // Timers to prevent immediate re-capture of the ball after a pass or shot
    public PlayerUnit currentBlockedPlayer;
    public float blockedPlayerTimer;

    // Actions
    private bool passRequested = false;
    private bool shootRequested = false;

    Vector2 passDirection;
    Vector2 shootDirection;

    private float passPower;
    private float shootPower;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        distanceCalculator = FindAnyObjectByType<DistanceCalculator>();
        gameValues = FindAnyObjectByType<GameValues>();
    }

    private void Start()
    {
        currentBallState = BallState.Free;
        blockedPlayerTimer = gameValues.BlockedPlayerTimer;
    }

    public void RequestPass(float power, PlayerUnit controlledPlayer)
    {
        currentBallHolder = controlledPlayer;
        currentBlockedPlayer = controlledPlayer;
        passDirection = distanceCalculator.GetClosestPassDirection(players, currentBallHolder);
        passPower = power;
        passRequested = true;
    }
    public void RequestShoot(float power, PlayerUnit controlledPlayer)
    {
        shootPower = power;
        shootRequested = true;
    }

    private void ClearPassRequest()
    {
        passRequested = false;
    }
    private void ClearShootRequest()
    {
        shootRequested = false;
    }

    private void FixedUpdate()
    {
        if (currentBallState == BallState.Free) 
        { 
            foreach (var player in players)
            {
                if (player == currentBlockedPlayer)
                {
                    blockedPlayerTimer -= Time.fixedDeltaTime;

                    if (blockedPlayerTimer <= 0)
                    {
                        currentBlockedPlayer = null;
                    }
                    continue;
                }
                if (Vector2.Distance(transform.position, player.transform.position) <= 1f)
                { 
                    if(player.playerSkillExecution.CaptureBall())
                    {
                        currentBallState = BallState.PlayerControlled;
                        currentBallHolder = player;
                        rb.bodyType = RigidbodyType2D.Kinematic;
                        GetComponent<CircleCollider2D>().enabled = false;
                    }
                }

            }
        }
        else if (currentBallState == BallState.PlayerControlled || currentBallState == BallState.ComputerControlled)
        {
            if (currentBallHolder.playerSkillExecution.Dribble(currentBallHolder))
            {
                Vector2 newPosition = currentBallHolder.playermovement.rb.position + currentBallHolder.playermovement.CurrentPlayerDirection * 1f;
                rb.MovePosition(newPosition);
            }
            else
            {
                currentBlockedPlayer = currentBallHolder;
                blockedPlayerTimer = 0.2f;
                currentBallState = BallState.Free;
                currentBallHolder = null;
                rb.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<CircleCollider2D>().enabled = true;
            }
            if (passRequested)
            {
                
            }

        }

    }

}
