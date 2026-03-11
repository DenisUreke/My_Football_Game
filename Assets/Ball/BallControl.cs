using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
public class BallControl : MonoBehaviour
{
    [SerializeField] PlayerUnit[] players;

    private bool isControlled = false;
    public bool IsControlled => isControlled;

    private void FixedUpdate()
    {
        if (!isControlled) 
        { 
            foreach (var player in players)
            {
                if (Vector2.Distance(transform.position, player.transform.position) <= 0.5f)
                { 
                    if(player.playerSkillExecution.CaptureBall())
                    {
                        isControlled = true;
                    }
                }

            }
        }
        if (isControlled)
        { 
        
        }
        
    }

}
