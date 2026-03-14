using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallControl : MonoBehaviour
{
    [SerializeField] PlayerUnit[] players;
    private PlayerUnit currentPlayer;

    private BallState currentBallState;

    public BallState CurrentBallState => currentBallState;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        currentBallState = BallState.Free;
    }

    private void FixedUpdate()
    {
        if (currentBallState == BallState.Free) 
        { 
            foreach (var player in players)
            {
                if (Vector2.Distance(transform.position, player.transform.position) <= 1f)
                { 
                    if(player.playerSkillExecution.CaptureBall())
                    {
                        currentBallState = BallState.PlayerControlled;
                        currentPlayer = player;
                        rb.bodyType = RigidbodyType2D.Kinematic;
                        GetComponent<CircleCollider2D>().enabled = false;
                    }
                }

            }
        }
        else if (currentBallState == BallState.PlayerControlled || currentBallState == BallState.ComputerControlled)
        {
            Vector2 newPosition = currentPlayer.playermovement.rb.position + currentPlayer.playermovement.CurrentPlayerDirection * 1.5f; // * 0.5f + (Vector2)currentPlayer.transform.position;
            rb.MovePosition(newPosition);

        }

    }

}
