using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public GameObject AttackCollisions;
    public GameObject collisions;
    public Transform[] Players;
    public GameObject PlayerStoarge;
    public GameObject Tiles;

    public bool OnTarget;
    public int TargetNum;

    bool HasATargetSelected;
    MeshRenderer mr;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        HasATargetSelected = false;

        yield return new WaitForSeconds(0.5f);
        Players = PlayerStoarge.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && GetComponent<TurnController>().state == GameTurn.PlayerTurn)
        {
            Ray ray = GameObject.Find("Main Camera").GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit Hit))
            {
                if (Hit.collider.GetComponent<CharacterMovement>() != null && Hit.collider.GetComponent<Stats>().TurnSpent == false && Hit.collider.GetComponent<Stats>().Player == true && OnTarget == false)
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
                    GameObject.Find("UI").GetComponent<ButtonsAndUI>().TargetNum = TargetNum;

                    //Checks the range that the target can move to and see its limits
                    int Range = Players[TargetNum].GetComponent<Stats>().Range;
                    Vector3 Pos = new Vector3(Players[TargetNum].transform.position.x, 0, Players[TargetNum].transform.position.z);
                    Vector3 ColliderSize = new Vector3((Range - 1) * 9, 1, (Range - 1) * 9);
                    collisions.transform.SetPositionAndRotation(Pos, collisions.transform.rotation);
                    collisions.transform.eulerAngles = new Vector3(0, 45, 0);
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
                                Players[TargetNum].GetComponent<CharacterMovement>().TargetPosition = TargetPos;
                            }

                        }
                    }
                }
            }
        }
    }
}
