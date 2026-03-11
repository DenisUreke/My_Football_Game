using UnityEngine;

public class ControlManager : MonoBehaviour
{

    public PlayerInputReader inputReader;
    public DistanceCalculator distanceCalculator;

    [Header("Players")]
    [SerializeField] PlayerUnit[] players;
    [SerializeField] BallControl ball;

    private PlayerUnit currentlyControlled;
    private PlayerUnit ballholder;
    private Vector2 playerMovementInput;


    private void Awake()
    {
        inputReader = FindAnyObjectByType<PlayerInputReader>();
        distanceCalculator = new DistanceCalculator();
    }

    private void Start()
    {
        if (players == null || players.Length == 0)
        {
            Debug.LogError("No players assigned to ControlManager.");
            return;
        }
        currentlyControlled = players[0];
    }
    private void Update()
    {
        if (inputReader == null || players == null || players.Length == 0)
            return;

        foreach (var player in players)
        {
            if (player == currentlyControlled)
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
        currentlyControlled.playermovement.SetMoveInput(playerMovementInput);
    }

    private void SwitchPlayer()
    {
        currentlyControlled = distanceCalculator.GetClosestPlayer(ball, players, currentlyControlled);
    }
    private void Shoot()
    {
        Debug.Log("Shoot");
    }
    private void Pass()
    {
        Debug.Log("Pass");
    }

}
