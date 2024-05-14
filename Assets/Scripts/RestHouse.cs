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
