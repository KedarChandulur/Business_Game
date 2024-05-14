using UnityEngine;

public class PlayerJailData
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
    public static System.Action<System.Int64, ushort> OnPlayerAssetMoneyChanged;
    
    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 100000;
    private long assetMoney = 0;

    private int playerBoardPosition;

    public PlayerJailData playerJailData = new PlayerJailData();
    private ushort properitiesOwned = 0;

    [SerializeField]
    private ushort playerID = 0;

    private void Awake()
    {
        playerBoardPosition = 1;
    }

    void Start()
    {
        account.Initialize(initialAmount);
        OnPlayerMoneyChanged?.Invoke(initialAmount, playerID);
        OnPlayerAssetMoneyChanged?.Invoke(assetMoney, playerID);
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

    public System.Int64 MoneyAvailable()
    {
        return account.MoneyAvailable();
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

    public void IncrementProperitiesOwned()
    {
        ++properitiesOwned;
    }

    public ushort NumberOfProperitiesOwned()
    {
        return properitiesOwned;
    }

    public long GetAssetMoney()
    {
        return assetMoney;
    }
    
    public void IncrementAssetMoney(long amount)
    {
        assetMoney += amount;
        OnPlayerAssetMoneyChanged?.Invoke(assetMoney, playerID);
    }

    public long GetFinalAmount()
    {
        return account.MoneyAvailable() + assetMoney;
    }
}