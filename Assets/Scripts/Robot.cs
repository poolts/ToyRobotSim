using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum Facing
    {
        North,
        South,
        East,
        West
    }

    public Facing CurrentlyFacing { get; set; }

    public bool IsPlaced { get; set; }

    public void Report()
    {

    }
}
