using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Square[] allSquares;

    private void Awake()
    {
        allSquares = new Square[36];

        if(this.transform.childCount != 4)
        {
            Debug.LogError("Error: Board is not setup correctly.");
            Utilities.QuitPlayModeInEditor();
            return;
        }

        Transform transform_Temporary;
        Square square_Temporary;

        for (int i = 0; i < 4; i++) 
        {
            transform_Temporary = this.transform.GetChild(i);

            foreach (Transform value in transform_Temporary)
            {
                if(!value.TryGetComponent<Square>(out square_Temporary))
                {
                    Debug.LogError("Error: No Square class found.");
                    Utilities.QuitPlayModeInEditor();
                    break;

                }

                allSquares[square_Temporary.GetObjectID() - 1] = square_Temporary;
            }
        }
    }

    void Start()
    {
        Dice.OnDiceRolled += OnDiceRolled;
    }

    private void OnDestroy()
    {
        Dice.OnDiceRolled -= OnDiceRolled;
    }

    private void OnDiceRolled(int diceValue)
    {
        Debug.Log("Dice Value from BoardManager: " + diceValue);

        if(!GameManager.instance.GetCurrentPlayer(out Player player))
        {
            Debug.LogError("Couldn't get the current player from the Game Manager.");
            return;
        }

        int playersCurrentPosition = player.GetCurrentPosition();

        if (playersCurrentPosition + diceValue > 36)
        {
            int blocksRemaining = 36 - playersCurrentPosition;

            int blocksFromStart = diceValue - blocksRemaining;

            player.UpdateCurrentPositionDirectly(blocksFromStart);

            // Give 250000 to player.
            Square startSquare = allSquares[0];
            startSquare.ProcessPlayer(diceValue, player);
        }
        else
        {
            playersCurrentPosition += diceValue;

            player.UpdateCurrentPositionDirectly(playersCurrentPosition);
        }

        Square playersCurrSquare = allSquares[playersCurrentPosition];

        playersCurrSquare.ProcessPlayer(diceValue, player);

        player.HandleInput(diceValue);
    }
}