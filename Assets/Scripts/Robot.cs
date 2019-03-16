using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum Facing
    {
        North,
        East,
        South,
        West
    }

    public Facing CurrentlyFacing { get; set; }

    public bool IsPlaced { get; set; }

    public System.Action Moved { get; set; }

    public System.Action Reported { get; set; }

    public void Move()
    {
        if(Moved != null)
        {
            Moved();
        }
    }

    public void Left()
    {
        CurrentlyFacing--;
    }

    public void Right()
    {
        CurrentlyFacing++;
    }

    public void Report()
    {
        if(Reported != null)
        {
            Reported();
        }
    }
}
