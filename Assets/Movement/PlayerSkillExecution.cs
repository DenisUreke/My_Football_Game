using UnityEngine;

public class PlayerSkillExecution : MonoBehaviour
{
    PlayerInputReader inputReader;

    private void Awake()
    {
        inputReader = FindAnyObjectByType<PlayerInputReader>();
    }
    public void Shoot()
    {
        Debug.Log("Shoot executed");
    }

    public void Pass()
    {
        Debug.Log("Pass executed");
    }

    public bool CaptureBall()
    {
        return true;
    }

    public bool Dribble(PlayerUnit player)
    {
        float turnDegree = Mathf.Abs(getTurnDegree(player));
        if (turnDegree > 170f)
        {
            Debug.Log("Dribble failed due to sharp turn");
            return false;
        }
        return true;
    }

    public float getTurnDegree(PlayerUnit player)
    {
        Vector2 oldDirection = player.playermovement.CurrentPlayerDirection;
        Vector2 newDirection = inputReader.RawMoveInput.normalized;
        float turnDegree = Vector2.SignedAngle(oldDirection, newDirection);
        return turnDegree;
    }
}
