﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulator : MonoBehaviour
{
    [SerializeField]
    Table m_table;

    [SerializeField]
    Robot m_robot;

    [SerializeField]
    TextAsset m_commandText;

    public void Start()
    {
        m_table.Generate(5);

        RunCommands(m_commandText.text);
    }

    public void RunCommands(string commandText)
    {
        string[] commands = commandText.Split(new string[] {"\n"}, System.StringSplitOptions.None);

        foreach(string command in commands)
        {
            string[] line = command.Split();

            string methodName = "";
            string[] args = new string[] {};

            if(line.Length > 0)
            {
               methodName = line[0];

               if(line.Length > 1)
               {
                   args = line[1].Split(',');
               }
            }

            RunCommand(methodName, args);
        }
    }

    public void RunCommand(string methodName, string[] args)
    {
        switch(methodName)
        {
            case "PLACE":
                int x = int.Parse(args[0]);
                int y = int.Parse(args[1]);

                Robot.Direction direction = (Robot.Direction)System.Enum.Parse(typeof(Robot.Direction), args[2], true);

                if(m_table.IsValidCell(x, y))
                {
                    m_robot.CurrentCellLocation = new Table.CellLocation(x, y);

                    m_robot.transform.position = m_table.GetCellPosition(x, y);

                    m_robot.CurrentlyFacing = direction;

                    m_robot.IsPlaced = true; 
                }
            break;

            case "MOVE":
                if(m_robot.IsPlaced)
                {
                    Table.CellLocation neighbouringCell = m_table.GetCellLocationInDirection(m_robot.CurrentCellLocation, m_robot.CurrentlyFacing);

                    if(m_table.IsValidCell(neighbouringCell.X, neighbouringCell.Y))
                    {
                        m_robot.CurrentCellLocation = neighbouringCell; 
                        m_robot.transform.position =  m_table.GetCellPosition(neighbouringCell.X, neighbouringCell.Y);
                    }
                }
            break;

            case "LEFT":
                m_robot.Left();
            break;

            case "RIGHT":
                m_robot.Right();
            break;

            case "REPORT":
                m_robot.Report();
            break;
        }
    }
}
