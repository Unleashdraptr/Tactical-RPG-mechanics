using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public Transform[] Players;

    public GameObject Tiles;
    public GameObject PlayerStorage;
    public bool CalledForRange;
    public Vector3[] OldPos;
    public int TargetNum;
    public GameObject collisions;
    public GameObject UIcontrol;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        Players =  PlayerStorage.GetComponentsInChildren<Transform>();
    }
    public void OnMoveButton()
    {
        UIcontrol.GetComponent<UIControl>().OnTarget = true;
        if (CalledForRange == false)
        {
            collisions.gameObject.SetActive(false);
            CalledForRange = true;
        }
        Players[TargetNum - 1].GetComponentInChildren<Movement>().IsMoving = true;
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
            if(Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().TakenID == TargetNum)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
        GameObject.Find("Move").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
    }
    public void OnConfirmButton()
    {
        Players[TargetNum].GetComponentInChildren<Movement>().IsMoving = true;
        GameObject.Find("Moved").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
    public void OnBattleButton()
    {

    }

    public void OnWaitButton()
    {
        Players[TargetNum].GetComponent<Stats>().TurnSpent = true;
        OldPos[TargetNum - 1] = Players[TargetNum].transform.position;
        Players[TargetNum].GetComponentInChildren<Movement>().IsMoving = false;
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        UIcontrol.GetComponent<UIControl>().OnTarget = false;
    }
    public void MoveConfirmCancelButton()
    {
        Players[TargetNum].GetComponentInChildren<Movement>().IsMoving = true;
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        { 
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
    }
    public void MovingCancelButton()
    {
        //Sets up the aproppiate buttons for use
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);

        //Removes the presets to say which character should be moving
        Players[TargetNum].GetComponentInChildren<Movement>().IsMoving = false;
        Players[TargetNum].GetComponentInChildren<Movement>().transform.SetPositionAndRotation(OldPos[TargetNum-1], Players[TargetNum - 1].GetComponentInChildren<Transform>().rotation);
        //Resets all tiles that can be moved to and what players the scripts will use
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
        UIcontrol.GetComponent<UIControl>().OnTarget = false;
    }
}
