using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private readonly Player[] players = new Player[4];

    private Banker banker;

    public static System.Action<Color> OnNextPlayerTurn;
    public static System.Action<Player> OnPlayerInit;

    private ushort currentPlayer = 0;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InstantiateGameManager()
    {
        if(instance == null)
        {
            GameObject gameManagerObject = new GameObject("GameManager");
            gameManagerObject.AddComponent<GameManager>();
            gameManagerObject.tag = "GameController";
            DontDestroyOnLoad(gameManagerObject);
        }
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            return;
        }

        currentPlayer = 1;
    }

    public void AddAndInitializePlayerState(Player player, ushort playerID)
    {
        players[playerID - 1] = player;

        if (playerID == 1)
        {
            currentPlayer = playerID;

            player.ChangeState(new PlayerRollingDiceState(player));

        }
        
        OnPlayerInit?.Invoke(player);

        //GameObject boardManager_GO = GameObject.FindGameObjectWithTag("BoardManager");

        //if(boardManager_GO == null)
        //{
        //    Debug.LogError("Board Manager_GO not found.");
        //    Utilities.QuitPlayModeInEditor();
        //    return;
        //}

        //if(!boardManager_GO.TryGetComponent<BoardManager>(out BoardManager boardManager))
        //{
        //    Debug.LogError("Board Manager not found.");
        //    Utilities.QuitPlayModeInEditor();
        //    return;
        //}

        //boardManager.UpdatePlayerSquareOnInit(player);
    }

    public void InitializeBanker(Banker _banker)
    {
        banker = _banker;
    }

    public bool GetCurrentPlayer(out Player outPlayer)
    {
        if(currentPlayer < 1 && currentPlayer > 4)
        {
            outPlayer = null;
        }
        else
        { 
            outPlayer = players[currentPlayer - 1];
        }

        return outPlayer != null;
    }

    public Banker GetBanker()
    {
        return banker;
    }

    public void SwitchToNextPlayer()
    {
        bool allPlayersInJail = true;

        // Check if all the players are in jail.
        foreach(Player playerRef in players)
        {
            if (!playerRef.playerSpecificData.IsInJail())
            {
                allPlayersInJail = false;
                //break;
            }
            else
            {
                playerRef.playerSpecificData.UpdateTurnsMissedCount();
            }
        }

        if(allPlayersInJail)
        {
            Debug.LogError("All player in jail releasing all of them.");

            foreach (Player playerRef in players)
            {
                playerRef.playerSpecificData.SetPlayerJailState(false);
            }
        }
        else
        {
            Debug.Log("Not all players are in jail not releasing the current.");

            // First increment as we are going to next player.
            if (++currentPlayer > 4)
            {
                currentPlayer = 1;
            }

            while (true)
            {
                if (players[currentPlayer - 1].playerSpecificData.IsInJail())
                {
                    if (++currentPlayer > 4)
                    {
                        currentPlayer = 1;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        OnNextPlayerTurn.Invoke(players[currentPlayer - 1].GetPlayerColor());
    }
}
