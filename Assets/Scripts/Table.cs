using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    Robot m_robot = null;

    [SerializeField]
    GameObject m_cellPrefab = null;

    Transform[,] m_cells;

    public struct CellLocation
    {
        public int X;
        public int Y;

        public CellLocation(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public void Start()
    {
        Generate(5);

        Place(2, 2, Robot.Direction.South);

        Move();
    }

    public void Generate(int tableSize)
    {
        m_cells = new Transform[tableSize, tableSize];

        for(int i = 0; i < m_cells.GetLength(0); i++)
        {
            for(int j = 0; j < m_cells.GetLength(1); j++)
            {
                Transform cellTrans = GameObject.Instantiate(m_cellPrefab, new Vector3(j, 0, i), Quaternion.identity).transform;

                cellTrans.parent = transform;

                m_cells[i, j] = cellTrans;
            }
        }
    }

    public void Place(int x, int y, Robot.Direction facing)
    {
        // If the cell is valid (i.e. not off the table then place the robot)
        if(IsValidCell(x, y))
        {
            m_robot.transform.position = m_cells[x, y].position;

            m_robot.CurrentlyFacing = facing;

            m_robot.IsPlaced = true; 
        }
    }

    public void Move()
    {
        if(m_robot.IsPlaced)
        {
            CellLocation neighbouringCell = GetCellLocationInDirection(m_robot.CurrentCellLocation, m_robot.CurrentlyFacing);

            if(IsValidCell(neighbouringCell.X, neighbouringCell.Y))
            {
                m_robot.CurrentCellLocation = neighbouringCell; 
                m_robot.transform.position = m_cells[neighbouringCell.X, neighbouringCell.Y].position;
            }
        }
    }

    public CellLocation GetCellLocationInDirection(CellLocation currentLocation, Robot.Direction direction)
    {
        CellLocation newLocation = currentLocation;

        switch(direction)
        {
            case Robot.Direction.North:
            newLocation.Y++;
            break;

            case Robot.Direction.East:
            newLocation.X++;
            break;

            case Robot.Direction.South:
            newLocation.Y--;
            break;

            case Robot.Direction.West:
            newLocation.X--;
            break;
        }

        return newLocation;
    }

    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1);
    }
}