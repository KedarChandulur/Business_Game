using UnityEngine;

public class Jail_JustVisit : Square
{
    private static readonly long amount = 15000;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Debug.Log("Jail amount 15000 paid by the player.");
        player.DebitAmount(amount);
        GameManager.instance.GetBanker().CreditAmount(amount);

        player.playerJailData.SetPlayerJailState(true);

        eventMessage = "You have paid Rs. 15000 for Bail.";

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
