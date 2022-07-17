using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour
{
    Camera cameras;
    public bool OnTarget;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        cameras = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            Ray ray = cameras.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out RaycastHit hitInfo))
            {
                if(hitInfo.collider.gameObject.GetComponent<Movement>() != null && hitInfo.collider.GetComponent<PlayerOne>().Turn == false)
                {
                    mr = hitInfo.collider.GetComponent<MeshRenderer>();
                    GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
                    mr.material.EnableKeyword("_EMISSION");
                    int PRange = hitInfo.collider.GetComponent<PlayerOne>().Range;
                    GameObject.Find("UI").GetComponent<Button>().Num = hitInfo.collider.GetComponent<PlayerOne>().NumID;
                    Vector3 ColliderSize = new Vector3((PRange * 10) + 5, 1, (PRange * 10) + 5);
                    hitInfo.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    hitInfo.collider.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size = ColliderSize;

                }
            }
        }
    }
}
