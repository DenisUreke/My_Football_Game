using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Technical Attributes")]
    [SerializeField] private float firstTouch = 50f;
    [SerializeField] private float ballControl = 50f;
    [SerializeField] private float dribbling = 50f;
    [SerializeField] private float passing = 50f;
    [SerializeField] private float shooting = 50f;

    [Header("Physical Attributes")]
    [SerializeField] private float speed = 50f;
    [SerializeField] private float acceleration = 50f;

    [Header("Goalkeeping Attributes")]

    public float FirstTouch => firstTouch;
    public float BallControl => ballControl;
    public float Dribbling => dribbling;
    public float Passing => passing;
    public float Shooting => shooting;
    public float Speed => speed;
    public float Acceleration => acceleration;
}