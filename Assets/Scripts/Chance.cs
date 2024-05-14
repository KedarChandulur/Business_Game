using UnityEngine;

public class Chance : Square
{
    public enum Type
    {
        Uninitialized,
        Pay100000, // Pay Rs. 100000 to the bank.
        Collect200000, // Advance to Delhi. If you pass Go, collect Rs. 250000.
        Pay200000, // Pay Rs. 200000 for repairs to your property. If you don't have any properties that require repairs, you don't need to pay.
        Jail, // Go directly to Jail. Do not pass Go, do not collect Rs. 250000.
        Collect300000, // You have won a crossword competition. Collect Rs. 300000.
        Collect150000, // Receive interest on your savings. Collect Rs. 150000.
    }

    private long amount;

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Pay100000:
                Debug.Log("Paying bank of 100000 by the player.");
                amount = 100000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Collect200000:
                Debug.Log("Collecting 200000 from bank by the player.");
                amount = 200000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);

                if(player.GetCurrentPosition() == 21)
                {
                    Debug.Log("Player will cross start to go to delhi so rewarding player.");
                    long startAmount = 250000;
                    player.CreditAmount(startAmount);
                    GameManager.instance.GetBanker().DebitAmount(startAmount);
                }

                player.UpdateCurrentPositionDirectly(11);

                break;
            case Type.Pay200000:
                Debug.Log("Paying bank of 200000 by the player.");
                amount = 200000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Jail:
                Debug.Log("Paying bank of 1000000 by the player.");
                amount = 1000000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                player.UpdateCurrentPositionDirectly(10);
                break;
            case Type.Collect300000:
                Debug.Log("Collecting 300000 from bank by the player.");
                amount = 300000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Collect150000:
                Debug.Log("Collecting 150000 from bank by the player.");
                amount = 150000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
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

        switch (base._pSquareType)
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
