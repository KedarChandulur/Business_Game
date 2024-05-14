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
                Debug.Log("Electricity utility from the player.");

                if(player.NumberOfProperitiesOwned() > 0)
                { 
                    utilityAmount = 10000 * player.NumberOfProperitiesOwned(); 
                }
                else
                {
                    utilityAmount = 0;
                }

                Debug.LogError("Debitting utilityAmount 10000: " + utilityAmount);

                player.DebitAmount(utilityAmount);
                GameManager.instance.GetBanker().CreditAmount(utilityAmount);
                break;

            case Type.WaterWorks:
                Debug.Log("Water works utility from the player.");

                if (player.NumberOfProperitiesOwned() > 0)
                {
                    utilityAmount = 3500 * player.NumberOfProperitiesOwned();
                }
                else
                {
                    utilityAmount = 0;
                }

                Debug.LogError("Debitting utilityAmount 3500: " + utilityAmount);

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
