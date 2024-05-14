using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Color playerBG_Color;

    public static System.Action<System.Int64, ushort> OnPlayerMoneyChanged;

    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 150000000;

    private int playerBoardPosition;

    [SerializeField]
    private ushort playerID = 0;

    private IPlayerState currentState;

    private void Awake()
    {
        currentState = new PlayerIdleState(this);
        playerBoardPosition = 1;
    }

    void Start()
    {
        account.Initialize(initialAmount);
        OnPlayerMoneyChanged?.Invoke(initialAmount, playerID);
        GameManager.instance.AddAndInitializePlayerState(this, playerID);
    }

    public Color GetPlayerColor()
    {
        return playerBG_Color;
    }

    public void DebitAmount(System.Int64 debitValue)
    {
        account.DebitAmount(debitValue);
        OnPlayerMoneyChanged?.Invoke(account.MoneyAvailable(), playerID);
    }

    public void CreditAmount(System.Int64 creditValue)
    {
        account.CreditAmount(creditValue);
        OnPlayerMoneyChanged?.Invoke(account.MoneyAvailable(), playerID);
    }

    //public System.Int64 MoneyAvailable()
    //{
    //    return account.MoneyAvailable();
    //}

    public void ChangeState(IPlayerState newState)
    {
        currentState.EndTurn();
        currentState = newState;
        currentState.StartTurn();
    }

    //public void StartTurn()
    //{
    //    currentState.StartTurn();
    //}

    //public void EndTurn()
    //{
    //    currentState.EndTurn();
    //}

    public void HandleInput(int diceValue)
    {
        currentState.HandleInput();
    }

    public void UpdateCurrentPositionDirectly(int position)
    {
        playerBoardPosition = position;
    }

    public int GetCurrentPosition()
    {
        return playerBoardPosition;
    }
}