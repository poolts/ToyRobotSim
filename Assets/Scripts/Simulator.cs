using System.Collections;
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
       if(methodName == "PLACE")
       {
            int x = int.Parse(args[0]);
            int y = int.Parse(args[1]);

            Robot.Direction direction = (Robot.Direction)System.Enum.Parse(typeof(Robot.Direction), args[2], true);

            if(m_table.IsValidCell(x, y))
            {
                m_robot.Place(m_table.GetCell(x, y), direction);
            }
       }
       else if(m_robot.IsPlaced)
       {
            if(methodName == "MOVE")
            {
                Table.Cell neighbouringCell = m_table.GetCellInDirection(m_robot.CurrentCell, m_robot.CurrentlyFacing);

                if(m_table.IsValidCell(neighbouringCell))
                {
                    m_robot.CurrentCell = neighbouringCell; 
                }         
            }
            else if(methodName == "LEFT")
            {
                m_robot.Left();
            }
            else if(methodName == "RIGHT")
            {
                m_robot.Right();
            }
            else if(methodName == "REPORT")
            {
                 m_robot.Report();
            }
       }
    }
}
