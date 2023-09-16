using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    GameObject player1, player2;

    private void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1");
        player2 = GameObject.FindGameObjectWithTag("Player2");

    }

    public void RespawnBothPlayers()
    {
        Debug.Log("Respawning both players");
        player1.GetComponent<PlayerInfo>().Respawn();
        player2.GetComponent<PlayerInfo>().Respawn();
    }
}
