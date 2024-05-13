using UnityEngine;

public class Banker : MonoBehaviour
{
    public static System.Action<System.Int64> MoneyChanged;

    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 590601202000;

    void Start()
    {
        account.Initialize(initialAmount);
        MoneyChanged?.Invoke(initialAmount);
    }
}
