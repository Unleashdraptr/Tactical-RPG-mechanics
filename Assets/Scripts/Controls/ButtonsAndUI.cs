using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsAndUI : MonoBehaviour
{
    public Transform[] Players;
    public GameObject PlayerStorage;
    public GameObject Tiles;
    public GameObject collisions;
    public GameObject AttackCollisions;

    public Transform[] OldPos;
    public int TargetNum;
    public GameObject UIcontrol;

    public Material Attack;
    public Material Movement;

    public int MoveSelect = 1;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Players =  PlayerStorage.GetComponentsInChildren<Transform>();
    }
    private void Update()
    {
        if(Input.GetKeyDown("q"))
        {
            MoveSelect -= 1;
            if (MoveSelect < 1)
            {
                MoveSelect = 3;
            }
            GameObject.Find("Border Outline").GetComponent<MoveSelection>().UpdateColour(MoveSelect);
        }
        if (Input.GetKeyDown("e"))
        {
            MoveSelect += 1;
            if (MoveSelect > 3)
            {
                MoveSelect = 1;
            }
            GameObject.Find("Border Outline").GetComponent<MoveSelection>().UpdateColour(MoveSelect);
        }
    }


    public void OnMoveButton()
    {
        if (collisions.gameObject.activeSelf == true)
        {
            collisions.gameObject.SetActive(false);
        }
        if (Players[TargetNum].GetComponent<Stats>().TurnSpent == false)
        {
            UIcontrol.GetComponent<Raycast>().OnTarget = true;
            for (int i = 0; i < Tiles.transform.childCount; i++)
            {
                if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false || Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().TakenID == TargetNum)
                {
                    Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
                }
            }
            GameObject.Find("Move").transform.localScale = new Vector3(0, 0, 0);
            GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void OnConfirmButton()
    {
        MoveSetup();
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
        GameObject.Find("Moved").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
    public void OnBattleButton()
    {
        collisions.gameObject.SetActive(false);
        int Childnum = 0;
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).transform.position == Players[TargetNum].transform.position)
            {
                Childnum = i;
            }
            if(Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanAttack == true)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material = Attack;
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
    public void OnWaitButton()
    {
        Players[TargetNum].GetComponent<Stats>().TurnSpent = true;
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
        AttackCollisions.gameObject.SetActive(false);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            if (Tiles.transform.GetChild(i).transform == OldPos[TargetNum-1])
            {
                Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken = false;
                Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanAttack = false;
            }
            if (Tiles.transform.GetChild(i).transform.position.x == Players[TargetNum].transform.position.x && Tiles.transform.GetChild(i).transform.position.z == Players[TargetNum].transform.position.z)
            {
                Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken = true;
            }
        }
        Players[TargetNum].GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
        OldPos[TargetNum - 1] = Players[TargetNum].transform;
    }
    public void MoveConfirmCancelButton()
    {
        AttackCollisions.gameObject.SetActive(false);
        //Resets previous places where they player could attack
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanAttack = false;
        }
        UIcontrol.GetComponent<Raycast>().OnTarget = true;
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false || Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().TakenID == TargetNum)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material = Movement;
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
    public void MovingCancelButton()
    {
        //Sets up the aproppiate buttons for use
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);


        Debug.Log("Move");
        Players[TargetNum].GetComponent<CharacterMovement>().TargetPosition = OldPos[TargetNum-1];
        //Resets all tiles that can be moved to and what players the scripts will use
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        UIcontrol.GetComponent<Raycast>().OnTarget = false;
    }



    public void MoveSetup()
    {
        for (int i = 0; i < MoveLibrary.MoveID.Length; i++)
        {
            if (MoveLibrary.MoveID[i] == Players[TargetNum].GetComponent<Stats>().PlayerMoveID[MoveSelect - 1])
            {
                Debug.Log("Found move stats!");
                //Moves the attack detecting with the player, sets its limits and then sets it awake to collide with the tiles
                Vector3 Pos = new Vector3(Players[TargetNum].transform.position.x, 0, Players[TargetNum].transform.position.z);
                AttackCollisions.transform.SetPositionAndRotation(Pos, AttackCollisions.transform.rotation);
                //Sets the colliders size to fit the attack radius
                Vector3 ColliderOffset = new Vector3(MoveLibrary.XOffset[i] * 5, 0, -MoveLibrary.YOffset[i] * 5);
                Vector3 ColliderSize = new Vector3(MoveLibrary.CollisionLength[i] * 4, 1, MoveLibrary.CollisionWidth[i] * 4);
                AttackCollisions.GetComponent<BoxCollider>().center = ColliderOffset;
                AttackCollisions.GetComponent<BoxCollider>().size = ColliderSize;
                AttackCollisions.gameObject.SetActive(true);
            }
            Debug.Log(("Library", MoveLibrary.MoveID[i]));
            Debug.Log(("MoveSelect: ", Players[TargetNum].GetComponent<Stats>().PlayerMoveID[MoveSelect-1]));
        }
    }
}
