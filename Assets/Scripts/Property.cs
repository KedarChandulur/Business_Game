using UnityEngine;

public class PropertyData
{
    public Player owner = null;
    public Player otherPlayer = null;
    public UnityEngine.UI.Image bg = null;
    public long buyAmount;
    public long rentAmount;
    public string propertyName;
    public bool isBuyable = true;
}

public class Property : Square
{
    public enum Type
    {
        Uninitialized,
        Mumbai,
        Ahmedabad,
        Indore,
        Jaipur,
        Delhi,
        Chandigarh,
        Shimla,
        Amritsar,
        Shrinagar,
        Agra,
        Kanpur,
        Patna,
        Darjeeling,
        Kolkata,
        Hyderabad,
        Chennai,
        Bangalore,
        Pune,
        Cochin,
        Goa
    }


    private readonly PropertyData propertyData = new PropertyData();

    public static System.Action<PropertyData> showBuyMenu;
    public static System.Action<PropertyData> showrentMenu;

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


        propertyData.propertyName = type.ToString();

        propertyData.buyAmount = GetBuyValue();
        propertyData.rentAmount = GetRentValue();
        propertyData.bg = base.backgroundImage;
    }

    public override void ProcessPlayer(int diceValue, Player player)
    {
        if (propertyData.isBuyable)
        {
            propertyData.owner = player;
            showBuyMenu?.Invoke(propertyData);
        }
        else
        {
            propertyData.otherPlayer = player;
            showrentMenu?.Invoke(propertyData);
        }
    }

    private long GetRentValue()
    {
        long value = 0;

        switch (type)
        {
            case Type.Mumbai:
                value = 15000;
                break;
            case Type.Delhi:
                value = 14500;
                break;
            case Type.Bangalore:
                value = 13500;
                break;
            case Type.Chennai:
                value = 12800;
                break;
            case Type.Hyderabad:
                value = 12000;
                break;
            case Type.Kolkata:
                value = 12000;
                break;
            case Type.Ahmedabad:
                value = 11800;
                break;
            case Type.Pune:
                value = 11500;
                break;
            case Type.Goa:
                value = 11000;
                break;
            case Type.Kanpur:
                value = 10000;
                break;
            case Type.Amritsar:
                value = 9800;
                break;
            case Type.Indore:
                value = 9600;
                break;
            case Type.Agra:
                value = 9300;
                break;
            case Type.Patna:
                value = 8400;
                break;
            case Type.Cochin:
                value = 7800;
                break;
            case Type.Shimla:
                value = 7200;
                break;
            case Type.Shrinagar:
                value = 6600;
                break;
            case Type.Jaipur:
                value = 6200;
                break;
            case Type.Chandigarh:
                value = 5800;
                break;
            case Type.Darjeeling:
                value = 5500;
                break;
            case Type.Uninitialized:
            default:
                break;
        }

        return value;
    }

    private long GetBuyValue()
    {
        long value = 0;

        switch (type)
        {
            case Type.Mumbai:
                value = 50000;
                break;
            case Type.Delhi:
                value = 48000;
                break;
            case Type.Bangalore:
                value = 45000;
                break;
            case Type.Chennai:
                value = 40000;
                break;
            case Type.Hyderabad:
                value = 36000;
                break;
            case Type.Kolkata:
                value = 35000;
                break;
            case Type.Ahmedabad:
                value = 35000;
                break;
            case Type.Pune:
                value = 34000;
                break;
            case Type.Goa:
                value = 34000;
                break;
            case Type.Kanpur:
                value = 33000;
                break;
            case Type.Amritsar:
                value = 32500;
                break;
            case Type.Indore:
                value = 32000;
                break;
            case Type.Agra:
                value = 32000;
                break;
            case Type.Patna:
                value = 32000;
                break;
            case Type.Cochin:
                value = 28000;
                break;
            case Type.Shimla:
                value = 30000;
                break;
            case Type.Shrinagar:
                value = 30000;
                break;
            case Type.Jaipur:
                value = 28000;
                break;
            case Type.Chandigarh:
                value = 25000;
                break;
            case Type.Darjeeling:
                value = 24000;
                break;
            case Type.Uninitialized:
            default:
                break;
        }

        return value;
    }
}
