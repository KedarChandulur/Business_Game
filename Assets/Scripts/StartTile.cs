using UnityEngine;

public class StartTile : Square
{
    private static readonly long startAmount = 250000;

    public override void ProcessPlayer(int diceValue, Player player)
    {
        // Get Money.
        Debug.Log("Start crossed of 250000 amount given to the player.");
        player.CreditAmount(startAmount);
        GameManager.instance.GetBanker().DebitAmount(startAmount);
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