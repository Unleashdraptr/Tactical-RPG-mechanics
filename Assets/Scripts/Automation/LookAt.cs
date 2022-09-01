using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LookAt : MonoBehaviour
{
    public int TargetNum;
    public bool Friendly;
    void Update()
    {
        transform.LookAt(GameObject.Find("Main Camera").transform);
        if (Friendly == true)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(0, 0, 255);
            for (int i = 0; i < GameObject.Find("Player").transform.childCount; i++)
            {
                if (GameObject.Find("Player").transform.GetChild(i).GetComponent<Stats>().NumID == TargetNum)
                {
                    Vector3 Pos = new Vector3(GameObject.Find("Player").transform.GetChild(i).transform.position.x, GameObject.Find("Player").transform.GetChild(i).transform.position.y + 2.5f, GameObject.Find("Player").transform.GetChild(i).transform.position.z);
                    transform.SetPositionAndRotation(Pos, transform.rotation);
                }
            }
        }
        if (Friendly == false)
        {
            transform.GetChild(0).GetComponent<Image>().color = new Color(255, 0, 0);
            for (int i = 0; i < GameObject.Find("Enemies").transform.childCount; i++)
            {
                if (GameObject.Find("Enemies").transform.GetChild(i).GetComponent<Stats>().NumID == TargetNum)
                {
                    Vector3 Pos = new Vector3(GameObject.Find("Enemies").transform.GetChild(i).transform.position.x, GameObject.Find("Enemies").transform.GetChild(i).transform.position.y + 2.5f, GameObject.Find("Enemies").transform.GetChild(i).transform.position.z);
                    transform.SetPositionAndRotation(Pos, transform.rotation);
                }
            }
        }

    }
}
