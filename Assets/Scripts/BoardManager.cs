using UnityEngine;
using UnityEngine.UIElements;

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

        for (int i = 0; i < 4; i++) 
        {
            transform_Temporary = this.transform.GetChild(i);

            foreach (Transform value in transform_Temporary)
            {
                if(!value.TryGetComponent<Square>(out Square square_Temporary))
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
        Chance.OnSpecialPlayerJump += OnSpecialPlayerJump;
    }

    private void OnDestroy()
    {
        Dice.OnDiceRolled -= OnDiceRolled;
        Chance.OnSpecialPlayerJump += OnSpecialPlayerJump;
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

        Debug.Log("Player: " + player.GetPlayerID() + " previous position: " + playersCurrentPosition);

        allSquares[playersCurrentPosition - 1].ResetSquare(player);

        if (playersCurrentPosition + diceValue > 36)
        {
            int blocksRemaining = 36 - playersCurrentPosition;

            int blocksFromStart = diceValue - blocksRemaining;

            player.UpdateCurrentPositionDirectly(blocksFromStart);

            playersCurrentPosition = blocksFromStart;

            // Give 250000 to player.
            Square startSquare = allSquares[0];
            startSquare.ProcessPlayer(diceValue, player);
        }
        else
        {
            playersCurrentPosition += diceValue;

            player.UpdateCurrentPositionDirectly(playersCurrentPosition);
        }

        Debug.Log("Player: " + player.GetPlayerID() + " current position: " + playersCurrentPosition);

        Square playersCurrSquare = allSquares[playersCurrentPosition - 1];

        playersCurrSquare.HighlightPlayersPosition(player);
        playersCurrSquare.ProcessPlayer(diceValue, player);

        // below does nothing rn.
        player.HandleInput();
        // below does nothing rn.
        player.EndTurn();

        GameManager.instance.SwitchToNextPlayer();
    }

    public void OnSpecialPlayerJump(Player player)
    {
        int playersCurrentPosition = player.GetCurrentPosition();

        Square playersCurrSquare = allSquares[playersCurrentPosition - 1];

        playersCurrSquare.HighlightPlayersPosition(player);
        playersCurrSquare.ProcessPlayer(0, player);
    }
}