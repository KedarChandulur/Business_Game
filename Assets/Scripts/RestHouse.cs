using UnityEngine;

public class RestHouse : Square
{
    public override void ProcessPlayer(int diceValue, Player player)
    {
        // Pay Money.

        //Type type = (Type)diceValue;

        //switch (type)
        //{
        //    case Type.Pay50:
        //        break;
        //    case Type.Collect200:
        //        break;
        //    case Type.Pay100:
        //        break;
        //    case Type.Jail:
        //        break;
        //    case Type.Collect100:
        //        break;
        //    case Type.Collect50:
        //        break;
        //    default:
        //        Debug.LogError("Something went wrong with Community Chest Challenge.");
        //        break;
        //}
    }

    public override void SetType(uint index, uint objectID)
    {
        base.SetType(index);

        switch (base._pSquareType)
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
