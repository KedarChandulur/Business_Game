using UnityEngine;
public class PlayerRollingDiceState : IPlayerState
{
    private Player playerRef;

    public PlayerRollingDiceState(Player player)
    {
        playerRef = player;
    }

    public void StartTurn()
    {
        Debug.Log("Start Turn Called for Player Rolling Dice State.");
    }

    public void HandleInput()
    {
        Debug.Log("Handle Input Called for Player Rolling Dice State.");
        //playerRef.;
    }

    public void EndTurn()
    {
        Debug.Log("End Turn Called for Player Rolling Dice State.");
    }
}
