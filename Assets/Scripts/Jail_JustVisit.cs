using UnityEngine;

public class Jail_JustVisit : Square
{
    private static readonly long amount = 1000000;

    private void Start()
    {
        // Implement wait for 2/3 turns.
        Debug.LogError("Wait not implemented for Jail.");
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Debug.Log("Paying bank of 1000000 by the player.");
        player.DebitAmount(amount);
        GameManager.instance.GetBanker().CreditAmount(amount);

        // Implement wait for 2/3 turns.
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
