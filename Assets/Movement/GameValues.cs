using UnityEngine;

public class GameValues : MonoBehaviour
{
    [Header("Game Settings")]

    [Header("Max Speed")]
    [SerializeField] private float maxPlayerSpeed = 0f;

    [Header("Max Accelleration")]
    [SerializeField] private float maxAccelleration = 0f;

    [Header("Max Friction")]
    [SerializeField] private float maxFriction = 0f;

    [Header("Acceptable Pass Angle")]
    [SerializeField] private float maxPassAngle = 45f;

    [Header("Blocked Player Timer")]
    [SerializeField] private float blockedPlayerTimer = 0.2f;

    [Header("Pass Power Multiplier")]
    [SerializeField] private float passPowerMultiplier = 8f;

    public float MaxPlayerSpeed => maxPlayerSpeed;
    public float MaxAccelleration => maxAccelleration;

    public float MaxFriction => maxFriction;
    public float MaxPassAngle => maxPassAngle;

    public float BlockedPlayerTimer => blockedPlayerTimer;

    public float PassPowerMultiplier => passPowerMultiplier;

}
