using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Can Move here");
        if (collision.gameObject.tag == "Tile")
        {
            Debug.Log("Can Move here");
            collision.gameObject.GetComponent<CanWalkTo>().CanMoveTo = true;
        }
    }
}
