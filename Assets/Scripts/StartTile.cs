using UnityEngine;

public class StartTile : Square
{
    private static readonly long startAmount = 25000;

    private void Awake()
    {
        base.Setup();
        GameManager.OnPlayerInit += OnPlayerInit;
    }

    private void OnDestroy()
    {
        GameManager.OnPlayerInit -= OnPlayerInit;
    }

    public void OnPlayerInit(Player player)
    {
        HighlightPlayersPosition(player);
    }


    public override void ProcessPlayer(int diceValue, Player player)
    {
        // Get Money.
        Debug.Log("Start crossed of 25000 amount given to the player.");
        player.CreditAmount(startAmount);
        GameManager.instance.GetBanker().DebitAmount(startAmount);

        eventMessage = "Start crossed, you receive Rs. 25000";

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}