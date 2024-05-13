using UnityEngine;

public class Chance : Square
{
    public enum Type
    {
        Uninitialized,
        Pay50, // Pay Rs. 50 to the bank.
        Collect200, // Advance to Delhi. If you pass Go, collect Rs. 200.
        Pay100, // Pay Rs. 100 for repairs to your property. If you don't have any properties that require repairs, you don't need to pay.
        Jail, // Go directly to Jail. Do not pass Go, do not collect Rs. 200.
        Collect100, // You have won a crossword competition. Collect Rs. 100.
        Collect50, // Receive interest on your savings. Collect Rs. 50.
    }
    
    public override void WhatToDo(uint diceValue)
    {
        Type type = (Type)diceValue;

        switch (type)
        {
            case Type.Pay50:
                break;
            case Type.Collect200:
                break;
            case Type.Pay100:
                break;
            case Type.Jail:
                break;
            case Type.Collect100:
                break;
            case Type.Collect50:
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
