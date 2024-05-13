public class Account
{
    private System.Int64 moneyAvailable = 0;

    public void Initialize(System.Int64 value)
    {
        moneyAvailable = value;
    }

    public void DebitAmount(System.Int64 debitValue)
    {
        moneyAvailable -= debitValue;
    }

    public void CreditAmount(System.Int64 creditValue)
    {
        moneyAvailable += creditValue;
    }

    public System.Int64 MoneyAvailable() 
    { 
        return moneyAvailable;
    }
}
