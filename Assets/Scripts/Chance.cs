using UnityEngine;

public class Chance : Square
{
    public static System.Action<Player> OnSpecialPlayerJump;

    private void Awake()
    {
        base.Setup();
    }

    public enum Type
    {
        Uninitialized,
        Pay1000, // Pay Rs. 1000 to the bank.
        Collect25000, // Advance to Delhi. If you pass Go, collect Rs. 25000.
        Pay2000, // Pay Rs. 2000 for repairs to your property. If you don't have any properties that require repairs, you don't need to pay.
        Jail, // Go directly to Jail. Do not pass Go, do not collect Rs. 25000.
        Collect30000, // You have won a crossword competition. Collect Rs. 30000.
        Collect_Interest, // Receive interest on your savings. Collect Rs. 150000.
    }

    private long amount;

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Pay1000:
                Debug.Log("Pay Rs. 1000 to the bank.");
                amount = 1000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;

            case Type.Collect25000:
                Debug.Log("Advance to Delhi. If you pass Go, collect Rs. 25000.");
                amount = 25000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);

                if(player.GetCurrentPosition() == 21)
                {
                    Debug.Log("Player will cross start to go to delhi so rewarding player.");
                    long startAmount = 250000;
                    player.CreditAmount(startAmount);
                    GameManager.instance.GetBanker().DebitAmount(startAmount);
                }

                this.ResetSquare(player);
                player.UpdateCurrentPositionDirectly(11);
                OnSpecialPlayerJump?.Invoke(player);
                break;

            case Type.Pay2000:
                Debug.Log("Pay Rs. 2000 for repairs to your property. If you don't have any properties that require repairs, you don't need to pay.");
                if(player.NumberOfProperitiesOwned() > 0)
                {
                    amount = 2000 * player.NumberOfProperitiesOwned();
                }
                else
                {
                    amount = 0;
                }

                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;

            case Type.Jail:
                Debug.Log("Go directly to Jail. Do not pass Go, do not collect Rs. 25000.");
                this.ResetSquare(player);
                player.UpdateCurrentPositionDirectly(10);
                OnSpecialPlayerJump?.Invoke(player);
                break;

            case Type.Collect30000:
                Debug.Log("You have won a crossword competition. Collect Rs. 30000.");
                amount = 30000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;

            case Type.Collect_Interest:
                Debug.Log("Receive interest on your savings. Collect Rs. 150000.");

                long savingsInterest = player.MoneyAvailable() * 6 / 100;
                player.CreditAmount(savingsInterest);
                GameManager.instance.GetBanker().DebitAmount(savingsInterest);
                break;

            default:
                Debug.LogError("Something went wrong with Community Chest Challenge.");
                Utilities.QuitPlayModeInEditor();
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
