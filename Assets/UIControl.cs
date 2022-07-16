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
                if(hitInfo.collider.gameObject.GetComponent<PlayerOne>() != null)
                {
                    mr = hitInfo.collider.GetComponent<MeshRenderer>();
                    OnTarget = true;
                    GameObject.Find("Move").transform.localScale = new Vector3(1, 1, 1);
                    mr.material.EnableKeyword("_EMISSION");
                    int PRange = hitInfo.collider.GetComponent<Movement>().Range;
                    Vector3 ColliderSize = new Vector3((PRange * 10) + 5, 1, (PRange * 10) + 5);
                    hitInfo.collider.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    hitInfo.collider.gameObject.transform.GetChild(0).GetComponent<BoxCollider>().size = ColliderSize;

                }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape) && OnTarget == true)
        {
            mr.material.DisableKeyword("_EMISSION");
            GameObject.Find("Battle Options").transform.localScale = new Vector3(0, 0, 0);
        }
    }
}
