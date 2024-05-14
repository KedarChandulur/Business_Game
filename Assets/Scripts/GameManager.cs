using System;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private Player[] players = new Player[4];

    private Banker banker;

    private ushort currentPlayer = 0;

    // Add State manager here if needed.

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

            players[playerID].ChangeState(new PlayerRollingDiceState(player));
        }
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
}
