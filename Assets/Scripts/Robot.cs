using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    public enum Direction
    {
        North,
        East,
        South,
        West
    }

    Direction m_currentlyFacing;

    public Direction CurrentlyFacing
    {
        get
        {
            return m_currentlyFacing;
        }
        set
        {
            float yRotation = 0f;

            m_currentlyFacing = value;

            switch(value)
            {                
                case Direction.North:
                yRotation = 0f;
                break;

                case Direction.East:
                yRotation = 90f;
                break;

                case Direction.South:
                yRotation = 180f;
                break;

                case Direction.West:
                yRotation = 270f;
                break;
            }

            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, yRotation, transform.rotation.eulerAngles.z);
        }
    }

    public Table.CellLocation CurrentCellLocation { get; set; }

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
        Debug.Log(CurrentCellLocation);
    }
}
