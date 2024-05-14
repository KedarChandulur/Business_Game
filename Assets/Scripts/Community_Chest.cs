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
        Pay10000, // Bank error in banks favor you pay Rs. 10000.
        Pay1000, // You inherit Rs. 1000 which you received from relatives.
        Pay5000, // Doctor's fee. Pay Rs. 5000.
        Pay30000, // Pay school fees. Pay Rs. 30000.
        Collect40000, // Life insurance matures. Collect Rs. 40000.
        Collect27000, // Income tax refund. Collect Rs. 27000.
    }

    private long amount;

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Pay10000:
                Debug.Log("Bank error in banks favor you pay Rs. 10000.");
                amount = 10000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Pay1000:
                Debug.Log("You inherit Rs. 1000 which you received from relatives.");
                amount = 1000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Pay5000:
                Debug.Log("Doctor's fee. Pay Rs. 5000.");
                amount = 5000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Pay30000:
                Debug.Log("Pay school fees. Pay Rs. 30000.");
                amount = 30000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Collect40000:
                Debug.Log("Life insurance matures. Collect Rs. 40000.");
                amount = 40000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Collect27000:
                Debug.Log("Income tax refund. Collect Rs. 27000.");
                amount = 27000;
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
