using UnityEngine;

public class Best : Square
{
    private static readonly long amountClaimed = 135000;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        // If you own best, rent is Rs.1350
        // Mortgage - 4750

        Debug.Log("Best amount claim of 135000 to the player.");
        player.CreditAmount(amountClaimed);
        GameManager.instance.GetBanker().DebitAmount(amountClaimed);
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
