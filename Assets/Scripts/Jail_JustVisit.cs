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
