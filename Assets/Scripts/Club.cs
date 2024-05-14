using UnityEngine;

public class Club : Square
{
    private static readonly long entryAmount = 350000;

    private void Awake()
    {
        base.Setup();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        Debug.Log("Club amount of 350000 to the player.");
        player.DebitAmount(entryAmount);
        GameManager.instance.GetBanker().CreditAmount(entryAmount);
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
