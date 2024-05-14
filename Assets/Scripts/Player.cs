using UnityEngine;

public class PlayerSpecificData
{
    ushort turnsMissed = 0;
    bool isInJail = false;

    public bool IsInJail()
    {
        return isInJail;
    }

    public void SetPlayerJailState(bool _isInJail = true)
    {
        isInJail = _isInJail;

        if(!isInJail)
        {
            turnsMissed = 0;
        }
    }
    
    public void UpdateTurnsMissedCount()
    {
        if(isInJail)
        {
            ++turnsMissed;

            if(turnsMissed > 8)
            {
                this.SetPlayerJailState(false);
            }
        }
        else
        {
            Debug.Log("Can't update count when not in jail.");
        }
    }
}

public class Player : MonoBehaviour
{
    [SerializeField]
    private Color playerBG_Color;

    public static System.Action<System.Int64, ushort> OnPlayerMoneyChanged;

    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 100000;

    private int playerBoardPosition;

    public PlayerSpecificData playerSpecificData = new PlayerSpecificData();

    [SerializeField]
    private ushort playerID = 0;

    private IPlayerState currentState;

    private void Awake()
    {
        currentState = new PlayerIdleState(this);
        playerBoardPosition = 1;
        //GameManager.instance.AddAndInitializePlayerState(this, playerID);
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

    public void EndTurn()
    {
        currentState.EndTurn();
    }

    public void HandleInput()
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

    public ushort GetPlayerID()
    {
        return playerID;
    }
}