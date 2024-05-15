using UnityEngine;

public class Transportation : Square
{
    public enum Type
    {
        Uninitialized,
        Railways,
        Airways,
        Motorboat,
    }

    private long transportAmount;

    [SerializeField]
    private Type type;

    private void Awake()
    {
        base.Setup();

        if (type == Type.Uninitialized)
        {
            Debug.LogError("Object type not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }

        base.squareName.text = type.ToString();
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        switch (type)
        {
            case Type.Railways:
                Debug.Log("Railways 5000 Transport cost from the player.");

                eventMessage = "You have to pay Railways of Rs. 5000";

                transportAmount = 5000;
                player.DebitAmount(transportAmount);
                GameManager.instance.GetBanker().CreditAmount(transportAmount);
                break;

            case Type.Airways:
                Debug.Log("Airways 25000 Transport cost from the player.");

                eventMessage = "You have to pay Airways of Rs. 25000";

                transportAmount = 25000;
                player.DebitAmount(transportAmount);
                GameManager.instance.GetBanker().CreditAmount(transportAmount);
                break;

            case Type.Motorboat:
                Debug.Log("Motorboat 2000 Transport cost from the player.");

                eventMessage = "You have to pay Motorboat of Rs. 2000";

                transportAmount = 2000;
                player.DebitAmount(transportAmount);
                GameManager.instance.GetBanker().CreditAmount(transportAmount);
                break;

            case Type.Uninitialized:
            default:
                Debug.LogError("Something went wrong with Tax Tile.");
                break;
        }

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
