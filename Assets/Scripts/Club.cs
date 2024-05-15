using UnityEngine;

public class Club : Square
{
    private static readonly long entryAmount = 3500;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Debug.Log("Club amount of 3500 from the player.");

        eventMessage = "You have paid Rs. 3500 for visiting a club.";

        player.DebitAmount(entryAmount);
        GameManager.instance.GetBanker().CreditAmount(entryAmount);

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
