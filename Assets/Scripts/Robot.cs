using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A robot can be placed and moved around a table.
/// </summary>
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

            // Rotate the robot's rotation dependant on the direction value set.
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

            // Set the robot's world position to the cell's world position.
            // Add an offset to ensure it's standing on the cell.
            transform.position = value.WorldPosition + new Vector3(0f, 0.25f, 0f);
        }
    }

    public bool IsPlaced { get; private set; }

    /// <summary>
    /// Places the robot on the cell.
    /// </summary>
    /// <param name="cell">The cell to place the robot on.</param>
    /// <param name="facing">The direction the robot should face.</param>
    public void Place(Table.Cell cell, Direction facing)
    {
        CurrentCell = cell;

        CurrentlyFacing = facing;

        IsPlaced = true;

        transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// Turns the robot to the left.
    /// </summary>
    public void Left()
    {
        if(IsPlaced)
        {
            // Wrap around to the west if the robot is currently facing north.
            CurrentlyFacing = CurrentlyFacing == Direction.North ? Direction.West : CurrentlyFacing - 1;
        }
        else
        {
            Debug.LogError("Robot cannot turn left until it has been placed on the table");
        }
    }

    /// <summary>
    /// Turns the robot to the right.
    /// </summary>
    public void Right()
    {
        if(IsPlaced)
        {
            // Wrap around to the north if the robot is currently facing west.
            CurrentlyFacing = CurrentlyFacing == Direction.West ? Direction.North : CurrentlyFacing + 1;
        }
        else
        {
            Debug.LogError("Robot cannot turn right until it has been placed on the table");            
        }
    }

    /// <summary>
    /// Reports the report's current cell (X and Y coordinates) and the direction it is currently facing.
    /// </summary>
    public void Report()
    {        
        if(IsPlaced)
        {
            Debug.Log(CurrentCell + "," + CurrentlyFacing);
        }
        else
        {
            Debug.LogError("Robot cannot report until it has been placed on the table");
        }
    }
}
