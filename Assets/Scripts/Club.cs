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
