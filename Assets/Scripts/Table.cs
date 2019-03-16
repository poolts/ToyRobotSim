using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    GameObject m_cellPrefab = null;

    Cell[,] m_cells;

    public struct Cell
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Vector3 WorldPosition { get; private set; }

        public Cell(int x, int y, Vector3 worldPos)
        {
            X = x;
            Y = y;
            WorldPosition = worldPos;
        }

        public override string ToString()
        {
            return X + "," + Y;
        }
    }

    public void Generate(int tableSize)
    {
        m_cells = new Cell[tableSize, tableSize];

        for(int i = 0; i < m_cells.GetLength(0); i++)
        {
            for(int j = 0; j < m_cells.GetLength(1); j++)
            {
                Transform cellTrans = GameObject.Instantiate(m_cellPrefab, new Vector3(j, 0, i), Quaternion.identity).transform;

                cellTrans.parent = transform;

                m_cells[i, j] = new Cell(i, j, cellTrans.position);
            }
        }
    }

    public Cell GetCell(int x, int y)
    {
        return m_cells[x, y];
    }

    public Cell GetCellInDirection(Cell currentCell, Robot.Direction direction)
    {
        Cell newCell = currentCell;

        if(direction == Robot.Direction.North && currentCell.Y < GetTableLength() - 2)
        {
            newCell = m_cells[currentCell.X, currentCell.Y + 1];
        }
        else if(direction == Robot.Direction.East && currentCell.X < GetTableWidth() - 2)
        {
            newCell = m_cells[currentCell.X + 1, currentCell.Y];
        }
        else if(direction == Robot.Direction.South && currentCell.Y > 0)
        {
            newCell = m_cells[currentCell.X, currentCell.Y - 1];
        }
        else if(direction == Robot.Direction.West && currentCell.X > 0)
        {
            newCell = m_cells[currentCell.X - 1, currentCell.Y];
        }

        return newCell;
    }

    public bool IsValidCell(Cell cell)
    {
        return IsValidCell(cell.X, cell.Y);
    }

    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1);
    }

    public int GetTableWidth()
    {
        return m_cells.GetLength(0);
    }

    public int GetTableLength()
    {
        return m_cells.GetLength(1);
    }
}