using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{

    private BallControl ballControl;
    private BallState currentBallState;

    // shooting and passing input handling
    private bool isShooting = false;
    private bool shotRequested = false;
    private bool isPassing = false;
    private bool passRequested = false;

    // switch player input handling
    private bool switchPlayerRequested = false;

    private float shootHoldTime = 0f;
    private float passHoldTime = 0f;

    public float ShootHoldTime => shootHoldTime;
    public float PassHoldTime => passHoldTime;


    private Vector2 rawMoveInput;
    public Vector2 RawMoveInput => rawMoveInput;

    private void Awake()
    {
        ballControl = FindAnyObjectByType<BallControl>();
    }

    private void Start()
    {
        currentBallState = ballControl.CurrentBallState;
    }

    private void Update()
    {
        if (isShooting)
        {
            shootHoldTime += Time.deltaTime;
        }
        if (isPassing)
        {
            passHoldTime += Time.deltaTime;
        }
    }

    private void OnMove(InputValue direction)
    {
        rawMoveInput = direction.Get<Vector2>();
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (currentBallState == BallState.Free)
        {
            return;
        }
        else if (context.started)
        {
            shootHoldTime = 0f;
            isShooting = true;
        }
        else if (context.canceled)
        {
            isShooting = false;
            shotRequested = true;
        }
    }

    public bool ConsumeShootRequest()
    {
        if (shotRequested)
        {
            shotRequested = false;
            return true;
        }
        return false;
    }

    private void OnPass(InputAction.CallbackContext context)
    {
        if (currentBallState == BallState.Free)
        {
            return;
        }
        else if (context.started)
        {
            passHoldTime = 0f;
            isPassing = true;
        }
        else if (context.canceled)
        {
            isPassing = false;
            passRequested = true;
        }
    }

    public bool ConsumePassRequest()
    {
        if (passRequested)
        {
            passRequested = false;
            return true;
        }
        return false;
    }

    private void OnSwitchPlayer()
    {
        if (currentBallState == BallState.Free || currentBallState == BallState.ComputerControlled)
        {
            switchPlayerRequested = true;
        }
    }

    public bool ConsumeSwitchPlayerRequest()
    {
        if (switchPlayerRequested)
        {
            switchPlayerRequested = false;
            return true;
        }
        return false;
    }
}