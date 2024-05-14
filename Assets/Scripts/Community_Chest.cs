using UnityEngine;

public class Community_Chest : Square
{
    private void Awake()
    {
        base.Setup();
    }

    public enum Type
    {
        Uninitialized,
        Collect400000, // Bank error in your favor. Collect Rs. 400000.
        Get100000, // You inherit Rs. 100000 from a relative.
        Pay500000, // Doctor's fee. Pay Rs. 500000.
        Pay100000, // Pay school fees. Pay Rs. 100000.
        Collect1000000, // Life insurance matures. Collect Rs. 1000000.
        Collect200000, // Income tax refund. Collect Rs. 200000.
    }

    private long amount;

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Collect400000:
                Debug.Log("Collecting 400000 from bank by the player.");
                amount = 400000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Get100000:
                Debug.Log("Collecting 100000 from relative by the player.");
                amount = 100000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Pay500000:
                Debug.Log("Paying bank of 500000 by the player.");
                amount = 500000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Pay100000:
                Debug.Log("Paying bank of 100000 by the player.");
                amount = 100000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Collect1000000:
                Debug.Log("Collecting 1000000 from bank by the player.");
                amount = 1000000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Collect200000:
                Debug.Log("Collecting 200000 from bank by the player.");
                amount = 200000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            default:
                Debug.LogError("Something went wrong with Community Chest Challenge.");
                break;
        }
    }

    public override void SetType(uint index, uint objectID)
    {
        base.SetType(index);

        switch (base.SquareTypeEnum)
        {
            case SquareType.Corned:
                break;
            case SquareType.Non_Corned:
                break;
            case SquareType.UnInitialized:
            default:
                Debug.LogError("Something went wrong with instantiation.");
                Utilities.QuitPlayModeInEditor();
                break;
        }
    }
}
