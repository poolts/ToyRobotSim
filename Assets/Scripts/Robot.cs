using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A robot can be placed and moved around a table.
/// </summary>
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

    Table.Cell m_currentCell;

    public Table.Cell CurrentCell { get; set; }

    public bool IsPlaced { get; private set; }

    /// <summary>
    /// Places the robot on the cell.
    /// </summary>
    /// <param name="cell">The cell to place the robot on.</param>
    /// <param name="facing">The direction the robot should face.</param>
    public void Place(Table.Cell cell, Facing facing)
    {
        CurrentCell = cell;

        CurrentlyFacing = facing;

        IsPlaced = true;

        transform.position = cell.WorldPosition + new Vector3(0f, 0.25f, 0f);

        transform.gameObject.SetActive(true);
    }

    /// <summary>
    /// Turns the robot to the left.
    /// </summary>
    public IEnumerator Left()
    {
        if(IsPlaced)
        {
            // Wrap around to the west if the robot is currently facing north.
            Facing toFace = CurrentlyFacing == Facing.North ? Facing.West : CurrentlyFacing - 1;

            yield return RotateToFacing(toFace);
        }
        else
        {
            Debug.LogError("Robot cannot turn left until it has been placed on the table");
        }
    }

    /// <summary>
    /// Turns the robot to the right.
    /// </summary>
    public IEnumerator Right()
    {
        if(IsPlaced)
        {
            // Wrap around to the north if the robot is currently facing west.
            Facing toFace = CurrentlyFacing == Facing.West ? Facing.North : CurrentlyFacing + 1;

            yield return RotateToFacing(toFace);
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

    public IEnumerator Move(Table.Cell cell)
    {
        // Set the robot's world position to the cell's world position.
        // Add an offset to ensure it's standing on the cell.
        Vector3 from = transform.position;
        Vector3 to = cell.WorldPosition + new Vector3(0f, 0.25f, 0f);

        float timer = 0f;
        float duration = 2f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            transform.position = Vector3.Lerp(from, to, timer / duration);
            yield return null;
        }

        transform.position = to;

        CurrentCell = cell;

        yield return null;
    }

    public IEnumerator RotateToFacing(Facing toFace)
    {
            float yRotation = 0f;

            switch(toFace)
            {                
                case Facing.North:
                yRotation = 0f;
                break;

                case Facing.East:
                yRotation = 90f;
                break;

                case Facing.South:
                yRotation = 180f;
                break;

                case Facing.West:
                yRotation = 270f;
                break;
            }

        Quaternion from = transform.rotation;
        Quaternion to = Quaternion.Euler(from.eulerAngles.x, yRotation, from.eulerAngles.z);
        float timer = 0f;
        float duration = 2f;

        while(timer < duration)
        {
            timer += Time.deltaTime;
            transform.rotation = Quaternion.Lerp(from, to, timer / duration);
            yield return null;
        }

        transform.rotation = to;

        CurrentlyFacing = toFace;
    }
}
