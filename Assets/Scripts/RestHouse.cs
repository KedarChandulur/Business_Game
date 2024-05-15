using UnityEngine;

public class RestHouse : Square
{
    private static readonly long rentAmount = 8000;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Debug.Log("Rest house  rent of 8000 from the player.");
        player.DebitAmount(rentAmount);
        GameManager.instance.GetBanker().CreditAmount(rentAmount);

        eventMessage = "You have to pay rest house  rent of Rs. 8000";

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
