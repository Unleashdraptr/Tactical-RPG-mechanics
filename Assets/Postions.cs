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
        PlayerNumber = PlayerStorage.transform.childCount;
        int Enemies = Random.Range(1, 3);

        TilePositions = Tiles.GetComponentsInChildren<Transform>();
        for (int i = 0; i < PlayerNumber; i++)
        {
            Vector3 Pos = new Vector3(TilePositions[PlayerPositions[i]].position.x, 2, TilePositions[PlayerPositions[i]].position.z);
            PlayerStorage.transform.GetChild(i).transform.SetPositionAndRotation(Pos, PlayerStorage.transform.GetChild(0).transform.rotation);
            PlayerStorage.transform.GetChild(i).GetComponent<PlayerOne>().NumID = i + 1;
            GameObject.Find("UI").GetComponent<Button>().OldPos[i] = Pos;
        }
        for (int i = 0; i < Enemies; i++)
        {
            Instantiate(Enemy, TilePositions[EnemyPositions[i]].position, Quaternion.identity, EnemyStorage.transform);
            int ChildNum = 0;
            for (int j = 0; j < Tiles.transform.childCount; j++)
            {
                Debug.Log("Searching for Child");
                if (Tiles.transform.GetChild(j).transform.position == TilePositions[EnemyPositions[i]].position)
                {
                    Debug.Log("Found Child");
                    ChildNum = j;
                }
            }
            Tiles.transform.GetChild(ChildNum).GetComponent<CanWalkTo>().IsTaken = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
