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
        Pay7000, // You inherit Rs. 1000 which you received from relatives.
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

                eventMessage = "Bank error in your favor, you receive Rs. 10000";

                amount = 10000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Pay7000:
                Debug.Log("Take Rs. 7000 cash from player with maximum cash.");

                eventMessage = "Take Rs. 7000 cash from player with maximum cash.";

                Player maxCashPlayer = null;

                foreach(Player playerRef in GameManager.instance.GetAllPlayers())
                {
                    if (playerRef.GetPlayerID() != player.GetPlayerID())
                    {
                        if(maxCashPlayer)
                        {
                            if(playerRef.MoneyAvailable() > maxCashPlayer.MoneyAvailable())
                            {
                                maxCashPlayer = playerRef;
                            }
                        }
                        else
                        {
                            maxCashPlayer = playerRef;
                        }
                    }
                }

                amount = 7000;
                player.CreditAmount(amount);
                maxCashPlayer.DebitAmount(amount);
                break;
            case Type.Pay5000:
                Debug.Log("Doctor's fee. Pay Rs. 5000.");

                eventMessage = "Doctor's fee. You have to pay Rs. 5000.";

                amount = 5000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Pay30000:
                Debug.Log("Pay school fees. Pay Rs. 30000.");

                eventMessage = "Pay school fees. Pay Rs. 30000.";

                amount = 30000;
                player.DebitAmount(amount);
                GameManager.instance.GetBanker().CreditAmount(amount);
                break;
            case Type.Collect40000:
                Debug.Log("Life insurance matures. Collect Rs. 40000.");

                eventMessage = "Life insurance matures. Collect Rs. 40000.";

                amount = 40000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            case Type.Collect27000:
                Debug.Log("Income tax refund. Collect Rs. 27000.");

                eventMessage = "Income tax refund. Collect Rs. 27000.";

                amount = 27000;
                player.CreditAmount(amount);
                GameManager.instance.GetBanker().DebitAmount(amount);
                break;
            default:
                Debug.LogError("Something went wrong with Community Chest Challenge.");
                break;
        }

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
