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