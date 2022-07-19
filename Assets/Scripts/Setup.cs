using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public int[] PlayerPositions = { 3, 4, 5 };
    public int[] EnemyPositions = { 45, 46, 47 };
    public int PlayerNumber;
    public GameObject Enemy;
    public GameObject Player;
    public Transform[] TilePositions;
    public GameObject Tiles;
    public GameObject EnemyStorage;
    public GameObject PlayerStorage;
    // Start is called before the first frame update
    void Start()
    {
        //Amont of enemies to spawn
        int Enemies = Random.Range(1, 3);

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
            GameObject.Find("UI").GetComponent<Button>().OldPos[i] = Pos;
        }
        for (int i = 0; i < Enemies; i++)
        {
            //Places the enemies
            Instantiate(Enemy, TilePositions[EnemyPositions[i]].position, Quaternion.identity, EnemyStorage.transform);
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
