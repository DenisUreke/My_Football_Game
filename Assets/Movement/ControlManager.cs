using UnityEngine;

public class ControlManager : MonoBehaviour
{

    public PlayerInputReader inputReader;
    public DistanceCalculator distanceCalculator;
    private StateHolder stateHolder;

    [SerializeField] BallControl ball;

    private Vector2 playerMovementInput;
    private Vector2 passDirection;

    private void Awake()
    {
        inputReader = FindAnyObjectByType<PlayerInputReader>();
        distanceCalculator = FindAnyObjectByType <DistanceCalculator>();
        stateHolder = FindAnyObjectByType<StateHolder>();
    }

    private void Start()
    {
        if (stateHolder.Players == null || stateHolder.Players.Length == 0)
        {
            Debug.LogError("No players assigned to ControlManager.");
            return;
        }
        stateHolder.CurrentlyControlled = stateHolder.Players[0];
    }
    private void Update()
    {
        if (inputReader == null || stateHolder.Players == null || stateHolder.Players.Length == 0)
            return;

        foreach (var player in stateHolder.Players)
        {
            if (player == stateHolder.CurrentlyControlled)
            {
                playerMovementInput = inputReader.RawMoveInput;
                PlayerMove();

                if (inputReader.ConsumeSwitchPlayerRequest())
                {
                    SwitchPlayer();
                }
                if (inputReader.ConsumeShootRequest())
                {
                    Shoot();
                }
                if (inputReader.ConsumePassRequest())
                {
                    Pass();
                }
            }
        }
    }

    private void PlayerMove()
    {
        stateHolder.CurrentlyControlled.playermovement.SetMoveInput(playerMovementInput);
    }

    private void SwitchPlayer()
    {
        PlayerUnit previousPlayer = stateHolder.CurrentlyControlled;

        PlayerUnit nextPlayer = distanceCalculator.GetClosestPlayer(
            ball,
            stateHolder.Players,
            stateHolder.CurrentlyControlled
        );

        if (nextPlayer == null)
            return;

        previousPlayer.playermovement.SetMoveInput(Vector2.zero);
        stateHolder.CurrentlyControlled = nextPlayer;
    }
    private void Shoot()
    {
        Debug.Log("Shoot");
    }
    private void Pass()
    {
        passDirection = distanceCalculator.GetClosestPassDirection(stateHolder.Players, stateHolder.CurrentlyControlled);
        ball.RequestPass(stateHolder.PassPower, stateHolder.CurrentlyControlled);
    }

    public void SetCurrentlyControlledPlayer(PlayerUnit player)
    {
        stateHolder.CurrentlyControlled = player;
    }

}
