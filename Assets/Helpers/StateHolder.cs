using System.Threading;
using UnityEngine;

public class StateHolder : MonoBehaviour
{
    [SerializeField] PlayerUnit[] players;
    public PlayerUnit[] Players => players;

    private GameValues gamevalues;

    private PlayerUnit currentlyControlled;
    public PlayerUnit CurrentlyControlled
    { 
        get => currentlyControlled;
        set => currentlyControlled = value;
    }

    private PlayerUnit currentBallHolder;
    public PlayerUnit CurrentBallHolder
    {
        get => currentBallHolder;
        set => currentBallHolder = value;
    }

    private PlayerUnit currentBlockedPlayer;
    public PlayerUnit CurrentBlockedPlayer
    {
        get => currentBlockedPlayer; 
        set => currentBlockedPlayer = value;
    }
    
    private float blockedPlayerTimer;
    public float BlockedPlayerTimer => blockedPlayerTimer;



    private BallState currentBallState;
    public BallState CurrentBallState
    { 
        get => currentBallState;
        set => currentBallState = value;
    }

    // Actions
    private bool passRequested = false;
    public bool PassRequested 
    { 
        get => passRequested;
        set => passRequested = value;
    }

    private bool shootRequested = false;
    public bool ShootRequested
    {
        get => shootRequested;
        set => shootRequested = value;
    }

    private Vector2 passDirection;
    public Vector2 PassDirection
    {
        get => passDirection;
        set => passDirection = value;
    }

    private Vector2 shootDirection;
    public Vector2 ShootDirection
    {
        get => shootDirection;
        set => shootDirection = value;
    }

    private float passPower;
    public float PassPower
    {
        get => passPower;
        set => passPower = value;
    }

    private float shootPower;
    public float ShootPower
    {
        get => shootPower;
        set => shootPower = value;
    }

    private void Awake()
    {
        gamevalues = FindAnyObjectByType<GameValues>();
        currentBallState = BallState.Free;
        blockedPlayerTimer = gamevalues.BlockedPlayerTimer;
    }

    public void DecrementBlockedTimer(float decrement) 
    {
        blockedPlayerTimer -= decrement;
    }

    public void ResetBlockedTimer()
    {
        blockedPlayerTimer = gamevalues.BlockedPlayerTimer;
    }

    public void ToggleShoot()
    {
        shootRequested = !shootRequested;
    }

    public void TogglePass()
    {
        passRequested = !passRequested;
    }

}
