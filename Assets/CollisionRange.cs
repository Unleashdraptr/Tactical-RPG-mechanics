using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionRange : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Tile" && collision.gameObject.GetComponent<CanWalkTo>().IsTaken == false)
        {
            collision.gameObject.GetComponent<CanWalkTo>().CanMoveTo = true;
        }
    }
}
