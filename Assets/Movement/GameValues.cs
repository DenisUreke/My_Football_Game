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

    public float MaxPlayerSpeed => maxPlayerSpeed;
    public float MaxAccelleration => maxAccelleration;

    public float MaxFriction => maxFriction;

}
