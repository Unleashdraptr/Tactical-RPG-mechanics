using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int HP;
    public int Range;
    public int Attack;
    public string Name;
    public int strength;
    public int NumID;
    public bool TurnSpent;
    public bool Player;
    //Dice used, Move radius, Move direction, Status Inflict Type
    public int[] MoveSetup = { 0, 0, 0, 0, 0 };
    public bool MovePlayerMove;
                                  //N, NE, E, SE, S, SW, W, NW
    public int[] movedirections = { 0, 0,  0, 0,  0, 0,  0, 0 };
}
