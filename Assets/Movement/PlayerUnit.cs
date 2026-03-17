using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public PlayerMovement playermovement;
    public PlayerSkillExecution playerSkillExecution;
    public PlayerStats playerStats;


    private float dribbleInstability = 0f;

    public float DribbleInstability => dribbleInstability;

    private void Awake()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerSkillExecution = FindFirstObjectByType<PlayerSkillExecution>();
        playerStats = GetComponent<PlayerStats>();
    }
}
