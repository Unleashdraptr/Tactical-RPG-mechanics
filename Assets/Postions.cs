using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Postions : MonoBehaviour
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
        int Enemies = Random.Range(1, 3);

        TilePositions = Tiles.GetComponentsInChildren<Transform>();
        for (int i = 0; i < PlayerNumber; i++)
        {
            Vector3 Pos = new Vector3(TilePositions[PlayerPositions[i] + 1].position.x, 2, TilePositions[PlayerPositions[i] + 1].position.z);
            Instantiate(Player, Pos, Quaternion.identity, PlayerStorage.transform.GetChild(i).transform);
            Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken = true;
        }
        for (int i = 0; i < Enemies; i++)
        {
            Instantiate(Enemy, TilePositions[EnemyPositions[i]+1].position, Quaternion.identity, EnemyStorage.transform);
            Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
