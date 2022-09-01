using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollision : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Tile")
        {
            collision.gameObject.GetComponent<CanWalkTo>().CanAttack = true;
        }
    }
}
