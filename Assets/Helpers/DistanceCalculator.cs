using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    private PlayerUnit previousPlayer;
    private GameValues gameValues;

    private void Awake()
    {
        gameValues = FindAnyObjectByType<GameValues>();
    }

    // Closest player to the ball that is not the ballholder
    public PlayerUnit GetClosestPlayer(BallControl ball, PlayerUnit[] players, PlayerUnit currentPlayer)
    {
        PlayerUnit closestPlayer = null;
        float closestDistance = float.MaxValue;

        foreach (var player in players)
        {
            float distance = Vector2.Distance(ball.transform.position, player.transform.position);
            if (distance < closestDistance && player != currentPlayer && player != previousPlayer)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }
        previousPlayer = currentPlayer;
        return closestPlayer;
    }

    // Closest player in the direction the ballholder is facing that is not the ballholder, if none found return the ballholder's current facing direction
    public Vector2 GetClosestPassDirection(PlayerUnit[] players, PlayerUnit currentPlayer)
    {
        PlayerUnit closestPassDirection = GetClosestPlayerInDirection(players, currentPlayer);

        if (closestPassDirection != null)
        {
            return (closestPassDirection.transform.position - currentPlayer.transform.position).normalized;
        }

        return currentPlayer.playermovement.CurrentPlayerDirection;
    }

    public PlayerUnit GetClosestPlayerInDirection(PlayerUnit[] players, PlayerUnit currentPlayer)
    {
        if (currentPlayer == null || players == null)
        {
            return null;
        }

        PlayerUnit bestCandidate = null;
        float closestDistance = float.MaxValue;

        foreach (var player in players)
        {
            if (player == currentPlayer || player == null)
            {
                continue;
            }
            else
            {
                float angle = GetAngleBetweenPlayers(player, currentPlayer);
                float distance = GetDistanceBetweenPlayers(player, currentPlayer);

                if (angle <= gameValues.MaxPassAngle && distance < closestDistance)
                {
                    closestDistance = distance;
                    bestCandidate = player;
                }
            }

        }
        return bestCandidate;
    }

    public float GetDistanceBetweenPlayers(PlayerUnit player1, PlayerUnit player2)
    {
        return Vector2.Distance(player1.transform.position, player2.transform.position); // get distance between two players
    }

    public float GetAngleBetweenPlayers(PlayerUnit target, PlayerUnit ballholder)
    {
        Vector2 directionToTarget = (target.transform.position - ballholder.transform.position).normalized; // get vector to target  B - A Gives direction from A to B
        return Vector2.Angle(ballholder.playermovement.CurrentPlayerDirection, directionToTarget); // get angle between ballholder's facing direction and direction to target
    }
}
