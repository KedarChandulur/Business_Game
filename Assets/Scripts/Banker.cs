using UnityEngine;

public class Banker : MonoBehaviour
{
    public static System.Action<System.Int64> OnBankerMoneyChanged;

    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 590601202000;

    void Start()
    {
        account.Initialize(initialAmount);
        OnBankerMoneyChanged?.Invoke(initialAmount);
        GameManager.instance.InitializeBanker(this);
    }

    public void DebitAmount(System.Int64 debitValue)
    {
        account.DebitAmount(debitValue);
        OnBankerMoneyChanged?.Invoke(account.MoneyAvailable());
    }

    public void CreditAmount(System.Int64 creditValue)
    {
        account.CreditAmount(creditValue);
        OnBankerMoneyChanged?.Invoke(account.MoneyAvailable());
    }

    //public System.Int64 MoneyAvailable()
    //{
    //    return account.MoneyAvailable();
    //}
}
