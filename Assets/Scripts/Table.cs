using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A table is a collection of cells (in a grid layout).
/// </summary>
public class Table : MonoBehaviour
{
    [SerializeField]
    GameObject m_cellPrefab = null;

    Cell[,] m_cells;

    /// <summary>
    /// Each cell contains an X and Y coordinate on the table (which is essentially an index into the 2D array)
    /// and a world position of where it exists in the scene.
    /// </summary>
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

        // Override the ToString method to have a neat format when logging a cell.
        public override string ToString()
        {
            return X + "," + Y;
        }
    }

    /// <summary>
    /// Generates the table by creating the cells and instanting the cell prefabs.
    /// </summary>
    /// <param name="tableWidth">The width of the table (the maximum X coordinate).</param>
    /// <param name="tableLength">The length of the table (the maximum Y coordinate).</param>
    public void Generate(int tableWidth, int tableLength)
    {
        m_cells = new Cell[tableWidth, tableLength];

        for(int i = 0; i < m_cells.GetLength(0); i++)
        {
            for(int j = 0; j < m_cells.GetLength(1); j++)
            {
                Vector3 cellPosition = new Vector3(i, 0, j);

                // Checks if the prefab exists. This is used to protect against null errors
                // when running the editor unit tests (as the prefab only exists at run-time).
                if(m_cellPrefab != null)
                {
                    Transform cellTrans = GameObject.Instantiate(m_cellPrefab, cellPosition, Quaternion.identity).transform;

                    cellTrans.parent = transform;
                }

                m_cells[i, j] = new Cell(i, j, cellPosition);
            }
        }
    }

    /// <summary>
    /// Gets a cell from the table.
    /// </summary>
    /// <param name="x">The X coordinate of the requested cell.</param>
    /// <param name="y">The Y coordinate of the requested cell.</param>
    /// <returns>The cell at the X and Y coordinates.</returns>
    public Cell GetCell(int x, int y)
    {
        return m_cells[x, y];
    }

    /// <summary>
    /// Gets the neighbouring cell in a given direction. Will return the current cell
    /// if the cell is off the table.
    /// </summary>
    /// <param name="currentCell">The cell we want to start at when looking for a neighbour.</param>
    /// <param name="direction">The direction we want to get a neighbour in.</param>
    /// <returns></returns>
    public Cell GetNeighbouringCellInDirection(Cell currentCell, Robot.Direction direction)
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

    /// <summary>
    /// Returns if a cell is valid. A cell is valid if it's X and Y coordinates are contained within the table.
    /// </summary>
    /// <param name="cell">The cell to validate.</param>
    /// <returns>If the cell is valid.</returns>
    public bool IsValidCell(Cell cell)
    {
        return IsValidCell(cell.X, cell.Y);
    }

    /// <summary>
    /// Returns if a cell is valid. A cell is valid if it's X and Y coordinates are contained within the table.
    /// </summary>
    /// <param name="x">The X coordinate of the cell to validate.</param>
    /// <param name="y">The Y coordinate of the cell to validate.</param>
    /// <returns>If the cell is valid.</returns>
    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1);
    }

    /// <summary>
    /// Gets the width of the table.
    /// </summary>
    /// <returns>The width of the table.</returns>
    public int GetTableWidth()
    {
        return m_cells.GetLength(0);
    }

    /// <summary>
    /// Gets the length of the table.
    /// </summary>
    /// <returns>The length of the table.</returns>
    public int GetTableLength()
    {
        return m_cells.GetLength(1);
    }
}