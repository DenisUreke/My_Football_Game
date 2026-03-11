using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DistanceCalculator
{
    private PlayerUnit previousPlayer;

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
}
