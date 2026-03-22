using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb;
    private DistanceCalculator distanceCalculator;
    private StateHolder stateHolder;
    private GameValues gameValues;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        distanceCalculator = FindAnyObjectByType<DistanceCalculator>();
        stateHolder = FindAnyObjectByType<StateHolder>();
        gameValues = FindAnyObjectByType<GameValues>();
    }

    public void RequestPass(float power, PlayerUnit controlledPlayer)
    {
        stateHolder.CurrentBallHolder = controlledPlayer;
        stateHolder.CurrentBlockedPlayer = controlledPlayer;
        stateHolder.PassDirection = distanceCalculator.GetClosestPassDirection(stateHolder.Players, stateHolder.CurrentBallHolder);
        stateHolder.PassPower = power;
        stateHolder.PassRequested = true;
    }
    public void RequestShoot(float power, PlayerUnit controlledPlayer)
    {
        stateHolder.ShootPower = power;
        stateHolder.ShootRequested = true;
    }

    private void ClearPassRequest()
    {
        stateHolder.PassRequested = false;
    }
    private void ClearShootRequest()
    {
        stateHolder.ShootRequested = false;
    }

    private void FixedUpdate()
    {
        if (stateHolder.CurrentBallState == BallState.Free) 
        { 
            foreach (var player in stateHolder.Players)
            {
                if (player == stateHolder.CurrentBlockedPlayer)
                {
                    stateHolder.DecrementBlockedTimer(Time.fixedDeltaTime);

                    if (stateHolder.BlockedPlayerTimer <= 0)
                    {
                        stateHolder.CurrentBlockedPlayer = null;
                    }
                    continue;
                }
                if (Vector2.Distance(transform.position, player.transform.position) <= 1f)
                { 
                    if(player.playerSkillExecution.CaptureBall())
                    {
                        stateHolder.CurrentBallState = BallState.PlayerControlled;
                        stateHolder.CurrentBallHolder = player;
                        rb.bodyType = RigidbodyType2D.Kinematic;
                        GetComponent<CircleCollider2D>().enabled = false;
                    }
                }

            }
        }
        else if (stateHolder.CurrentBallState == BallState.PlayerControlled || stateHolder.CurrentBallState == BallState.ComputerControlled)
        {
            if (stateHolder.CurrentBallHolder.playerSkillExecution.Dribble(stateHolder.CurrentBallHolder))
            {
                Vector2 newPosition = stateHolder.CurrentBallHolder.playermovement.rb.position + stateHolder.CurrentBallHolder.playermovement.CurrentPlayerDirection * 1f;
                rb.MovePosition(newPosition);
            }
            else
            {
                stateHolder.CurrentBlockedPlayer = stateHolder.CurrentBallHolder;
                stateHolder.ResetBlockedTimer();
                stateHolder.CurrentBallState = BallState.Free;
                stateHolder.CurrentBallHolder = null;
                rb.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<CircleCollider2D>().enabled = true;
            }
            if (stateHolder.PassRequested)
            {
                stateHolder.CurrentBallState = BallState.Free;
                rb.bodyType = RigidbodyType2D.Dynamic;
                GetComponent<CircleCollider2D>().enabled = true;
                rb.AddForce(stateHolder.PassDirection * stateHolder.PassPower * gameValues.PassPowerMultiplier, ForceMode2D.Impulse);
                ClearPassRequest();

            }

        }

    }

}
