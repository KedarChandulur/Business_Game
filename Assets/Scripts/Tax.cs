using UnityEngine;

public class Tax : Square
{
    public enum Type
    {
        Uninitialized,
        IncomeTax,
        LuxuryTax,
    }

    private static readonly long taxAmount = 200000;

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
            case Type.LuxuryTax:
                Debug.Log("Tax of 200000 to the player.");
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
