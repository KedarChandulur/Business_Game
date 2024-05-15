using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Square : MonoBehaviour
{
    protected Image backgroundImage;
    protected TextMeshProUGUI squareName;

    protected Image playerHighlightSquare1;
    protected Image playerHighlightSquare2;
    protected Image playerHighlightSquare3;
    protected Image playerHighlightSquare4;

    public static System.Action<string, Color> OnPlayerProcessed;
    protected string eventMessage = string.Empty;

    [SerializeField]
    protected uint objectID = 0;

    public void Setup()
    {
        if (!TryGetComponent<Image>(out backgroundImage))
        {
            Debug.LogError("Error setting the Background image.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            backgroundImage.color = Color.black;
        }

        if (this.transform.childCount != 1)
        {
            Debug.LogError("Object not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }

        Transform child = this.transform.GetChild(0);

        if (!child.TryGetComponent<Image>(out Image temporaryImage))
        {
            Debug.LogError("Object not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            this.ResetImageColor(temporaryImage);
        }

        if (child.childCount != 5)
        {
            Debug.LogError("Object not set correctly.");
            Utilities.QuitPlayModeInEditor();
        }

        if (!child.transform.GetChild(0).TryGetComponent<TextMeshProUGUI>(out squareName))
        {
            Debug.LogError("Error setting the Child Text.");
            Utilities.QuitPlayModeInEditor();
        }

        if (!child.transform.GetChild(1).TryGetComponent<Image>(out playerHighlightSquare1))
        {
            Debug.LogError("Error setting the player square 1 image Text.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            this.ResetImageColor(playerHighlightSquare1);
        }

        if (!child.transform.GetChild(2).TryGetComponent<Image>(out playerHighlightSquare2))
        {
            Debug.LogError("Error setting the player square 2 image Text.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            this.ResetImageColor(playerHighlightSquare2);
        }

        if (!child.transform.GetChild(3).TryGetComponent<Image>(out playerHighlightSquare3))
        {
            Debug.LogError("Error setting the player square 3 image Text.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            this.ResetImageColor(playerHighlightSquare3);
        }

        if (!child.transform.GetChild(4).TryGetComponent<Image>(out playerHighlightSquare4))
        {
            Debug.LogError("Error setting the player square 4 image Text.");
            Utilities.QuitPlayModeInEditor();
        }
        else
        {
            this.ResetImageColor(playerHighlightSquare4);
        }
    }

    public uint GetObjectID() 
    { 
        return objectID;
    }

    public void HighlightPlayersPosition(Player player)
    {
        switch(player.GetPlayerID())
        {
            case 1:
                playerHighlightSquare1.color = player.GetPlayerColor();
                break;
            case 2:
                playerHighlightSquare2.color = player.GetPlayerColor();
                break;
            case 3:
                playerHighlightSquare3.color = player.GetPlayerColor();
                break;
            case 4:
                playerHighlightSquare4.color = player.GetPlayerColor();
                break;
            default:
                Debug.LogError("Unknown player id: " + player.GetPlayerID());
                break;
        }
    }

    public void ResetSquare(Player player)
    {
        switch (player.GetPlayerID())
        {
            case 1:
                this.ResetImageColor(playerHighlightSquare1);
                break;
            case 2:
                this.ResetImageColor(playerHighlightSquare2);
                break;
            case 3:
                this.ResetImageColor(playerHighlightSquare3);
                break;
            case 4:
                this.ResetImageColor(playerHighlightSquare4);
                break;
            default:
                Debug.LogError("Unknown player id: " + player.GetPlayerID());
                break;
        }
    }

    public void ResetImageColor(Image image)
    {
        image.color = Color.grey;
    }

    public abstract void ProcessPlayer(int diceValue, Player player);
}