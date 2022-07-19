using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    Camera cameras;
    GameObject UI;
    public bool OnTarget;
    MeshRenderer mr;
    public Transform[] Players;
    public GameObject PlayerStoarge;
    public int TargetNum;
    public GameObject collisions;
    public GameObject AttackCollisions;
    bool HasATargetSelected;
    public GameObject Tiles;
    IEnumerator Start()
    {
        HasATargetSelected = false;

        //Grabs the camera for the Raycast and all players on the field
        cameras = GameObject.Find("Main Camera").GetComponent<Camera>();
        yield return new WaitForSeconds(0.5f);
        Players = PlayerStoarge.GetComponentsInChildren<Transform>();
    }
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = cameras.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit Hit))
            {
                if(Hit.collider.GetComponent<Movement>() != null && Hit.collider.GetComponent<Stats>().TurnSpent == false && Hit.collider.GetComponent<Stats>().Player == true && OnTarget == false)
                {
                    //Removes previously selected player's highlighter and viable positions
                    //only runs if there has been someone else selected
                    if (HasATargetSelected == true)
                    {
                        mr.material.DisableKeyword("_EMISSION");
                    }
                    HasATargetSelected = true;
                    collisions.gameObject.SetActive(false);
                    for (int i = 0; i < Tiles.transform.childCount; i++)
                    {
                        Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

                        Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo = false;
                    }

                    //Highlights the player that was picked
                    mr = Hit.collider.GetComponent<MeshRenderer>();
                    GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
                    mr.material.EnableKeyword("_EMISSION");

                    //Sets code to change logic to move and operate that target
                    TargetNum = Hit.collider.GetComponent<Stats>().NumID;
                    GameObject.Find("UI").GetComponent<Button>().TargetNum = TargetNum;

                    //Checks the range that the target can move to and see its limits
                    int Range = Players[TargetNum].GetComponent<Stats>().Range;
                    Vector3 Pos = new Vector3(Players[TargetNum].transform.position.x, 0, Players[TargetNum].transform.position.z);
                    collisions.transform.SetPositionAndRotation(Pos, collisions.transform.rotation);
                    collisions.transform.eulerAngles = new Vector3(0, 45, 0);
                    Vector3 ColliderSize = new Vector3((Range-1) * 10, 1, (Range - 1) * 10);
                    collisions.gameObject.SetActive(true);
                    collisions.GetComponent<BoxCollider>().size = ColliderSize;
                }

                if (Hit.collider.gameObject.tag == "Tile")
                {
                    if (OnTarget == true)
                    {
                        if ((Hit.collider.GetComponent<CanWalkTo>().CanMoveTo == true && Hit.collider.GetComponent<CanWalkTo>().IsTaken == false) || (Hit.collider.GetComponent<CanWalkTo>().IsTaken == true && Hit.collider.GetComponent<CanWalkTo>().TakenID == TargetNum))
                        {
                            if (Players[TargetNum].GetComponent<Stats>().TurnSpent == false)
                            {
                                //Gives the player the position of the selected tile and tells it to move
                                Transform TargetPos = Hit.collider.transform;
                                Players[TargetNum].GetComponent<Movement>().TargetPosition = TargetPos;

                                //Moves the attack detecting with the player, sets its limits and then sets it awake to collide with the tiles
                                Vector3 Pos = new Vector3(Players[TargetNum].transform.position.x, 0, Players[TargetNum].transform.position.z);
                                AttackCollisions.transform.SetPositionAndRotation(Pos, AttackCollisions.transform.rotation);

                                //Sets the colliders size to fit the attack radius
                                Vector3 ColliderOffset = new Vector3(Players[TargetNum].GetComponent<MoveLibrary>().XOffset * 5, 0, -Players[TargetNum].GetComponent<MoveLibrary>().YOffset * 5);
                                Vector3 ColliderSize = new Vector3(Players[TargetNum].GetComponent<MoveLibrary>().CollisionLength * 4, 1, Players[TargetNum].GetComponent<MoveLibrary>().CollisionWidth * 4);
                                AttackCollisions.GetComponent<BoxCollider>().center = ColliderOffset;
                                AttackCollisions.GetComponent<BoxCollider>().size = ColliderSize;
                                AttackCollisions.gameObject.SetActive(true);

                            }

                        }
                    }
                }
            }
        }
    }
}