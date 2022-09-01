using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveLibrary
{
    //Move will be set out along these arrays

    //Name that will be searched to get the data as each player loads
    public static int[] MoveID = { 1, 2, 3 };
    //Move Position relative to the player
    public static float[] XOffset = { 0, 0, 0 };
    public static float[] YOffset = { 1, 0, 1 };
    //Where the move can hit 
    public static int[] CollisionLength = { 1, 3, 3 };
    public static int[] CollisionWidth = { 1, 3, 1 };
    //How many instances of damage it can deal
    public static int[] DiceRolls = { 1, 1, 2 };
}
