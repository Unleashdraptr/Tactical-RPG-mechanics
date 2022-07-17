using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform[] TilePositions;
    GameObject Tiles;
    public bool OnTarget;
    [Range(1,9)]
    public int Range;
    public GameObject[] Players;
    Camera cameras;
    public bool Moving = false;
    public bool Moved = false;
    int Num;
    // Start is called before the first frame update
    void Start()
    {
        Num = GameObject.Find("UI").GetComponent<Button>().Num;
        cameras = GameObject.Find("Main Camera").GetComponent<Camera>();
        Tiles = GameObject.Find("Tiles");
        TilePositions = Tiles.GetComponentsInChildren<Transform>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Moving == true && Moved == false)
        {
            if (Input.GetMouseButton(0))
            {
                Ray ray = cameras.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (hitInfo.collider.gameObject.tag == "Tile")
                    {
                        Debug.Log("Move");
                        if (hitInfo.collider.GetComponent<CanWalkTo>().CanMoveTo == true && hitInfo.collider.GetComponent<CanWalkTo>().IsTaken == false)
                        {
                            Debug.Log("Can Move to");
                            int ChildNum = 0;
                            for (int i = 0; i < Tiles.transform.childCount; i++)
                            {
                                Debug.Log("Searching for Child");
                                if (Tiles.transform.GetChild(i).transform.position == hitInfo.collider.transform.position)
                                {
                                    Debug.Log("Found Child");
                                    ChildNum = i;
                                }
                            }
                            Vector3 Pos = new Vector3(TilePositions[ChildNum+1].position.x, 1, TilePositions[ChildNum+1].position.z);
                            Players[Num-1].transform.SetPositionAndRotation(Pos, transform.rotation);                       
                        }

                    }
                }
            }
        }
    }
}
