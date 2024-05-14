using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private Player playerRef;

    public PlayerIdleState(Player player)
    {
        playerRef = player;
    }

    public void StartTurn()
    {
        Debug.Log("Start Turn Called for Player Idle State.");
    }

    public void HandleInput()
    {
        Debug.Log("Handle Input Called for Player Idle State.");
    }

    public void EndTurn()
    {
        Debug.Log("End Turn Called for Player Idle State.");
    }
}
