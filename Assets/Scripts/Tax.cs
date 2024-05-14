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

                Debug.LogError("Debitting taxAmount 6: " + taxAmount);

                player.DebitAmount(taxAmount);
                GameManager.instance.GetBanker().CreditAmount(taxAmount);

                break;
            case Type.LuxuryTax:
                taxAmount = player.MoneyAvailable() * 10 / 100;

                Debug.LogError("Debitting taxAmount 10: " + taxAmount);

                player.DebitAmount(taxAmount);
                GameManager.instance.GetBanker().CreditAmount(taxAmount);

                break;
            case Type.Uninitialized:
            default:
                Debug.LogError("Something went wrong with Tax Tile.");
                break;
        }
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
