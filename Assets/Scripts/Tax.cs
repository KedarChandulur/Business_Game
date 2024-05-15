using UnityEngine;

public class Tax : Square
{
    public enum Type
    {
        Uninitialized,
        IncomeTax,
        LuxuryTax,
    }

    private long taxAmount = 0;

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
            case Type.IncomeTax:
                taxAmount = player.MoneyAvailable() * 6 / 100;

                Debug.Log("IncomeTax 6: " + taxAmount);

                eventMessage = "You have to pay Income tax of Rs. " + taxAmount;

                player.DebitAmount(taxAmount);
                GameManager.instance.GetBanker().CreditAmount(taxAmount);

                break;
            case Type.LuxuryTax:
                taxAmount = player.MoneyAvailable() * 10 / 100;

                Debug.Log("LuxuryTax 10: " + taxAmount);

                eventMessage = "You have to pay Luxury tax of Rs. " + taxAmount;

                player.DebitAmount(taxAmount);
                GameManager.instance.GetBanker().CreditAmount(taxAmount);

                break;
            case Type.Uninitialized:
            default:
                Debug.LogError("Something went wrong with Tax Tile.");
                break;
        }

        OnPlayerProcessed.Invoke(eventMessage, player.GetPlayerColor());
    }
}
