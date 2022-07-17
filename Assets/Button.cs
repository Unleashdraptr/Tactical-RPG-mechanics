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
    public int Num;
    public int[] AdditonalNum = { 0, 4, 8 };
    private void Start()
    {
        Players =  PlayerStorage.GetComponentsInChildren<Transform>();
    }
    public void OnMoveButton()
    {
        if (CalledForRange == false)
        {
            Players[Num - 1].GetComponentInChildren<BoxCollider>().gameObject.SetActive(false);
            CalledForRange = true;
        }
        Players[Num - 1].GetComponentInChildren<Movement>().Moving = true;
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            if (Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().CanMoveTo == true && Tiles.transform.GetChild(i).GetComponent<CanWalkTo>().IsTaken == false)
            {
                Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            }
        }
        GameObject.Find("Move").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
    }
    public void OnConfirmButton()
    {
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().Moved = true;
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().Moving = false;
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
        Players[Num + AdditonalNum[Num]].transform.GetChild(Num - 1).GetComponent<PlayerOne>().Turn = true;
        Players[Num + AdditonalNum[Num]].transform.position = OldPos[Num-1];
    }
    public void MoveConfirmCancelButton()
    {
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().Moved = false;
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().Moving = true;
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
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().Moving = false;
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
        Players[Num + AdditonalNum[Num]].GetComponentInChildren<Movement>().transform.SetPositionAndRotation(OldPos[Num-1], Players[Num - 1].GetComponentInChildren<Transform>().rotation);
        for (int i = 0; i < Tiles.transform.childCount; i++)
        {
            Tiles.transform.GetChild(i).GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
        }
    }
}
