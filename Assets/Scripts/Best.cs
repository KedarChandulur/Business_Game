using UnityEngine;

public class Best : Square
{
    private long amountClaimed = 135000;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        // Claim 2500 even if you don't own anything, if you own 2500 will be multipled by no of properities.

        Debug.Log("Claim 2500 even if you don't own anything, if you own 2500 will be multipled by no of properities.");

        if(player.NumberOfProperitiesOwned() > 0)
        {
            amountClaimed = 2500 * player.NumberOfProperitiesOwned();
        }
        else
        {
            amountClaimed = 2500;
        }

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
