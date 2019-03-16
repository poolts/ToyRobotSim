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

    Table.Cell m_currentCell;

    public Table.Cell CurrentCell 
    { 
        get
        { 
            return m_currentCell;
        } 
        set
        {
            m_currentCell = value;

            transform.position = value.WorldPosition;
        }
    }

    public bool IsPlaced { get; private set; }

    public void Place(Table.Cell cell, Direction facing)
    {
        CurrentCell = cell;

        CurrentlyFacing = facing;

        IsPlaced = true; 
    }

    public void Left()
    {
        if(IsPlaced)
        {
            CurrentlyFacing = CurrentlyFacing == Direction.North ? Direction.West : CurrentlyFacing - 1;
        }
    }

    public void Right()
    {
        if(IsPlaced)
        {
            CurrentlyFacing = CurrentlyFacing == Direction.West ? Direction.North : CurrentlyFacing + 1;
        }
    }

    public void Report()
    {        
        if(IsPlaced)
        {
            Debug.Log(CurrentCell + "," + CurrentlyFacing);
        }
    }
}
