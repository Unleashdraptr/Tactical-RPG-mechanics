using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Transform[] TilePositions;
    GameObject Tiles;
    public bool IsTarget;
    public Transform TargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        Tiles = GameObject.Find("Tiles");
        TilePositions = Tiles.GetComponentsInChildren<Transform>();
        TargetPosition = transform;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 Pos = new Vector3(TargetPosition.position.x, 2, TargetPosition.position.z);
        transform.SetPositionAndRotation(Pos, transform.rotation);
    }
}
