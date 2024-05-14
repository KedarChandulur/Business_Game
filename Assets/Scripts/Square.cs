using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Square : MonoBehaviour
{
    public enum SquareType
    {
        UnInitialized,
        Non_Corned,
        Corned
    }

    protected Image backgroundImage;
    protected TextMeshProUGUI squareName;

    public SquareType _pSquareType { get; private set; }

    [SerializeField]
    protected uint objectID = 0;

    public void Setup()
    {
        if (!TryGetComponent<Image>(out backgroundImage))
        {
            Debug.LogError("Error setting the Background image.");
            Utilities.QuitPlayModeInEditor();
        }

        if (this.transform.childCount != 1)
        {
            Debug.LogError("Object not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }

        Transform child = this.transform.GetChild(0);

        if (child.childCount != 1)
        {
            Debug.LogError("Object not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }

        if (!child.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out squareName))
        {
            Debug.LogError("Error setting the Child Text.");
            Utilities.QuitPlayModeInEditor();
        }
    }

    protected void SetType(uint index)
    {
        this._pSquareType = index > 0 ? SquareType.Non_Corned : SquareType.Corned;
    }

    public uint GetObjectID() 
    { 
        return objectID;
    }

    public abstract void SetType(uint index, uint objectID);
    public abstract void ProcessPlayer(int diceValue, Player player);
}