using UnityEngine;

public class Community_Chest : Square
{
    public enum Type
    {
        Uninitialized,
        Collect200, // Bank error in your favor. Collect Rs. 200.
        Get100, // You inherit Rs. 100 from a relative.
        Pay50, // Doctor's fee. Pay Rs. 50.
        Pay100, // Pay school fees. Pay Rs. 100.
        Collect100, // Life insurance matures. Collect Rs. 100.
        Collect20, // Income tax refund. Collect Rs. 20.
    }

    public override void WhatToDo(uint diceValue)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Collect200:
                break;
            case Type.Get100:
                break;
            case Type.Pay50:
                break;
            case Type.Pay100:
                break;
            case Type.Collect100:
                break;
            case Type.Collect20:
                break;
            default:
                Debug.LogError("Something went wrong with Community Chest Challenge.");
                break;
        }
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
