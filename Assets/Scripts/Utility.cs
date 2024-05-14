using UnityEngine;

public class Utility : Square
{
    public enum Type
    {
        Uninitialized,
        ElectricityCompany,
        WaterWorks
    }

    private long utilityAmount;

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
            case Type.ElectricityCompany:
                Debug.Log("Electricity utility of 1000000 from the player.");
                utilityAmount = 100000;
                player.DebitAmount(utilityAmount);
                GameManager.instance.GetBanker().CreditAmount(utilityAmount);
                break;

            case Type.WaterWorks:
                Debug.Log("Water works utility of 350000 from the player.");
                utilityAmount = 350000;
                player.DebitAmount(utilityAmount);
                GameManager.instance.GetBanker().CreditAmount(utilityAmount);
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
