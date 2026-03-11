using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
    public PlayerMovement playermovement;
    public PlayerSkillExecution playerSkillExecution;
    public PlayerStats playerStats;

    private void Awake()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerSkillExecution = GetComponent<PlayerSkillExecution>();
        playerStats = GetComponent<PlayerStats>();
    }
}
