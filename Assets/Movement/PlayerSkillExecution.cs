using UnityEngine;

public class PlayerSkillExecution : MonoBehaviour
{
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
}
