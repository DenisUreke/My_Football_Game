using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputReader : MonoBehaviour
{

    private BallControl ballControl;
    private StateHolder stateHolder;

    // shooting and passing input handling
    private bool isShooting = false;
    private bool shotRequested = false;
    private bool isPassing = false;
    private bool passRequested = false;

    // switch player input handling
    private bool switchPlayerRequested = false;

    private Vector2 rawMoveInput;
    public Vector2 RawMoveInput => rawMoveInput;

    private void Awake()
    {
        ballControl = FindAnyObjectByType<BallControl>();
        stateHolder = FindAnyObjectByType<StateHolder>();
    }

    private void Update()
    {
        if (isShooting)
        {
            stateHolder.ShootPower += Time.deltaTime;
        }
        if (isPassing)
        {
            stateHolder.PassPower += Time.deltaTime;
        }
    }

    private void OnMove(InputValue direction)
    {
        rawMoveInput = direction.Get<Vector2>();
    }

    private void OnShoot(InputValue value)
    {
        if (stateHolder.CurrentBallState == BallState.Free)
            return;

        if (value.isPressed)
        {
            stateHolder.ShootPower = 0f;
            isShooting = true;
        }
        else
        {
            isShooting = false;
            shotRequested = true;
        }
    }

    private void OnPass(InputValue value)
    {
        if (stateHolder.CurrentBallState == BallState.Free)
            return;

        if (value.isPressed)
        {
            stateHolder.PassPower = 0f; // reset value to start charging
            isPassing = true;
            Debug.Log($"Pass button pressed {stateHolder.PassPower}");
        }
        else
        {
            isPassing = false;
            passRequested = true;
            Debug.Log($"Pass requested {stateHolder.PassPower} seconds");
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
        if (stateHolder.CurrentBallState == BallState.Free || stateHolder.CurrentBallState == BallState.ComputerControlled)
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