using UnityEngine;

public class Player : MonoBehaviour
{
    public static System.Action<System.Int64, ushort> MoneyChanged;
    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 150000000;

    [SerializeField]
    private ushort playerID = 0;

    void Start()
    {
        account.Initialize(initialAmount);
        MoneyChanged?.Invoke(initialAmount, playerID);
    }
}