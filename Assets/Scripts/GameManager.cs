using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private readonly Player[] players = new Player[4];

    private Banker banker;

    public static System.Action<Color> OnNextPlayerTurn;
    public static System.Action<Player> OnPlayerInit;
    public static System.Action<Player> OnPlayerWin;

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
        }
        
        OnPlayerInit?.Invoke(player);
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

    public Player[] GetAllPlayers()
    {
        return players;
    }

    public void SwitchToNextPlayer()
    {
        bool allPlayersInJail = true;
        bool gameOver = false;

        // Check if all the players are in jail.
        foreach(Player playerRef in players)
        {
            if (!playerRef.playerJailData.IsInJail())
            {
                allPlayersInJail = false;
            }
            else
            {
                playerRef.playerJailData.UpdateTurnsMissedCount();
            }

            if(playerRef.MoneyAvailable() < 0)
            {
                gameOver = true;
                break;
            }
        }

        if (!gameOver)
        {
            if (allPlayersInJail)
            {
                Debug.LogError("All player in jail releasing all of them.");

                foreach (Player playerRef in players)
                {
                    playerRef.playerJailData.SetPlayerJailState(false);
                }
            }
            else
            {
                // First increment as we are going to next player.
                if (++currentPlayer > 4)
                {
                    currentPlayer = 1;
                }

                while (true)
                {
                    if (players[currentPlayer - 1].playerJailData.IsInJail())
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
        else
        {
            Player player = null;

            foreach (Player playerRef in players)
            {
                if(player)
                {
                    if(playerRef.GetFinalAmount() > player.GetFinalAmount())
                    {
                        player = playerRef;
                    }
                }
                else
                {
                    player = playerRef;
                }
            }

            OnPlayerWin?.Invoke(player);
        }
    }
}
