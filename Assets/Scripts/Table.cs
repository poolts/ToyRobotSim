using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Table : MonoBehaviour
{
    Transform[,] m_cells;

    [SerializeField]
    GameObject m_cellPrefab = null;

    public void Start()
    {
        Generate(5);
    }

    public void Generate(int tableSize)
    {
        m_cells = new Transform[tableSize, tableSize];

        for(int i = 0; i < m_cells.GetLength(0); i++)
        {
            for(int j = 0; j < m_cells.GetLength(1); j++)
            {
                GameObject.Instantiate(m_cellPrefab, new Vector3(j * 1.2f, 0, i * 1.2f), Quaternion.identity);
            }
        }
    }
}
