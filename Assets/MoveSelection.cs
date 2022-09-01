using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSelection : MonoBehaviour
{
    Color32 Move1 = new Color32(255, 0, 0, 50);
    Color32 Move2 = new Color32(0, 255, 0, 50);
    Color32 Move3 = new Color32(0, 0, 255, 50);
    Color32 Colour;
    public void UpdateColour(int MoveNum)
    {
        if(MoveNum == 1)
        {
            Colour = Move1;
        }
        if (MoveNum == 2)
        {
            Colour = Move2;
        }
        if (MoveNum == 3)
        {
            Colour = Move3;
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Image>().color = Colour;
        }
    }
}
