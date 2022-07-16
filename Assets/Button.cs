using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject Player3;
    public GameObject Tiles;
    public GameObject PlayerStorage;
    private void LateStart()
    {
        Player1 = PlayerStorage.transform.GetChild(0).GetComponent<GameObject>();
        //Player2 = PlayerStorage.transform.GetChild(1).GetComponent<GameObject>();
        //Player3 = PlayerStorage.transform.GetChild(2).GetComponent<GameObject>();
    }
    public void OnMoveButton()
    {
        Player1.GetComponentInChildren<BoxCollider>().gameObject.SetActive(false);
        Player1.GetComponentInChildren<Movement>().Moving = true;
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
        Player1.GetComponentInChildren<Movement>().Moved = true;
        Player1.GetComponentInChildren<Movement>().Moving = false;
        GameObject.Find("Moved").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
    }
    public void OnBattleButton()
    {

    }

    public void OnWaitButton()
    {

    }
    public void MoveConfirmCancelButton()
    {
        Player1.GetComponentInChildren<Movement>().Moved = false;
        Player1.GetComponentInChildren<Movement>().Moving = true;
        GameObject.Find("Moving").transform.localScale = new Vector3(1, 1, 1);
        GameObject.Find("Moved").transform.localScale = new Vector3(0, 0, 0);
    }
    public void MovingCancelButton()
    {
        Player1.GetComponentInChildren<Movement>().Moving = false;
        GameObject.Find("Moving").transform.localScale = new Vector3(0, 0, 0);
        GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
    }
}
