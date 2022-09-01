using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameTurn {Setup, PlayerTurn, EnemyTurn, EnvironmentChange, Win, Lose }
public class TurnController : MonoBehaviour
{
    public GameTurn state;
    public Transform[] Players;
    public GameObject PlayerStorage;
    void Start()
    {
        state = GameTurn.Setup;
        if(state == GameTurn.Setup)
        {
            GetComponent<InitialSetup>().SetupGame();
            state = GameTurn.PlayerTurn;
            Players = PlayerStorage.GetComponentsInChildren<Transform>();
        }
    }
    void Update()
    {
        int PlayerTurnsTaken = 0;
        int AmountOfPlayers = PlayerStorage.transform.childCount;
        for(int i = 0; i < AmountOfPlayers; i ++)
        {
            if(PlayerStorage.transform.GetChild(i).GetComponent<Stats>().TurnSpent == true)
            {
                PlayerTurnsTaken += 1;
            }
        }
        if(PlayerTurnsTaken == Players.Length-1)
        {
            state = GameTurn.EnemyTurn;
        }


        if(state == GameTurn.EnemyTurn)
        {
            //AI stuff
        }
        if (state == GameTurn.EnvironmentChange)
        {
            //Moving environmental factors if they exist
        }

    }
}
