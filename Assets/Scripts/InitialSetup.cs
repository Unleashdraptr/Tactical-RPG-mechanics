using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSetup : MonoBehaviour
{
    public int[] PlayerPositions = { 3, 4, 5 };
    public int[] EnemyPositions = { 7,8,9,10 };
    public int PlayerNumber;
    public GameObject Enemy;
    public GameObject Player;
    public Transform[] TilePositions;
    public GameObject Tiles;
    public GameObject EnemyStorage;
    public GameObject PlayerStorage;
    public GameObject Health;
    public GameObject HealthStorage;
    // Start is called before the first frame update
    public void SetupGame()
    {
        //Amont of enemies to spawn
        int Enemies = Random.Range(1, 4);

        //All playable locations
        TilePositions = Tiles.GetComponentsInChildren<Transform>();
        for (int i = 0; i < PlayerNumber; i++)
        {
            //Places the player and tells the game where the old position is
            Vector3 Pos = new Vector3(TilePositions[PlayerPositions[i]].position.x, 2, TilePositions[PlayerPositions[i]].position.z);
            Instantiate(Player, Pos, Quaternion.identity, PlayerStorage.transform);
            Tiles.transform.GetChild(PlayerPositions[i]-1).GetComponent<CanWalkTo>().IsTaken = true;
            Tiles.transform.GetChild(PlayerPositions[i]-1).GetComponent<CanWalkTo>().TakenID = i + 1;
            PlayerStorage.transform.GetChild(i).GetComponent<Stats>().NumID = i + 1;
            PlayerStorage.transform.GetChild(i).GetComponent<Stats>().PlayerMoveID[0] = 1;
            PlayerStorage.transform.GetChild(i).GetComponent<Stats>().PlayerMoveID[1] = 2;
            PlayerStorage.transform.GetChild(i).GetComponent<Stats>().PlayerMoveID[2] = 3;
            GameObject.Find("UI").GetComponent<ButtonsAndUI>().OldPos[i] = TilePositions[PlayerPositions[i]];
            Instantiate(Health, Pos, Quaternion.identity, HealthStorage.transform);
            HealthStorage.transform.GetChild(i).GetComponentInChildren<LookAt>().TargetNum = i + 1;
            HealthStorage.transform.GetChild(i).GetComponentInChildren<LookAt>().Friendly = true;
        }
        for (int i = 0; i < Enemies; i++)
        {
            //Places the enemies
            Vector3 Pos = new Vector3(TilePositions[EnemyPositions[i]].position.x, 1, TilePositions[EnemyPositions[i]].position.z);
            Instantiate(Enemy, Pos, Quaternion.identity, EnemyStorage.transform);
            EnemyStorage.transform.GetChild(i).GetComponent<Stats>().NumID = i + 1;
            Instantiate(Health, Pos, Quaternion.identity, HealthStorage.transform);
            HealthStorage.transform.GetChild(i + PlayerNumber).GetComponentInChildren<LookAt>().TargetNum = i + 1;
            HealthStorage.transform.GetChild(i + PlayerNumber).GetComponentInChildren<LookAt>().Friendly = false;
            int ChildNum = 0;
            for (int j = 0; j < Tiles.transform.childCount; j++)
            {
                if (Tiles.transform.GetChild(j).transform.position == TilePositions[EnemyPositions[i]].position)
                {
                    ChildNum = j;
                }
            }
            //Sets where the enemy is as a place the players cannot walk to
            Tiles.transform.GetChild(ChildNum).GetComponent<CanWalkTo>().IsTaken = true;
        }

    }
}
