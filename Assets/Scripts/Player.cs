using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Color playerBG_Color;
    public static System.Action<System.Int64, ushort> OnPlayerMoneyChanged;
    private readonly Account account = new Account();
    private const System.Int64 initialAmount = 150000000;

    [SerializeField]
    private ushort playerID = 0;

    void Start()
    {
        account.Initialize(initialAmount);
        OnPlayerMoneyChanged?.Invoke(initialAmount, playerID);
    }

    public Color GetPlayerColor()
    {
        return playerBG_Color;
    }
}