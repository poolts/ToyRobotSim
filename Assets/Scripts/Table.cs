using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField]
    Transform m_robotTrans = null;

    [SerializeField]
    GameObject m_cellPrefab = null;

    Transform[,] m_cells;

    public void Start()
    {
        Generate(5);

        Place(2, 2, 1);
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

    public void Place(int x, int y, int f)
    {
        // If the cell is valid (i.e. not off the table then place the robot)
        if(IsValidCell(x, y))
        {
            m_robotTrans.transform.position = m_cells[x, y].position;
        }
    }

    public bool IsValidCell(int x, int y)
    {
        return x >= 0 && x < m_cells.GetLength(0) && y >= 0 && y < m_cells.GetLength(1);
    }
}