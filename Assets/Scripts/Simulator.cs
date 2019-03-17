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

        RunCommands(m_commandText.text, m_robot, m_table);
    }

    public void RunCommands(string commandText, Robot robot, Table table)
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

            RunCommand(methodName, args, robot, table);
        }
    }

    public void RunCommand(string methodName, string[] args, Robot robot, Table table)
    {
       if(methodName == "PLACE")
       {
            int x = int.Parse(args[0]);
            int y = int.Parse(args[1]);

            Robot.Direction direction = (Robot.Direction)System.Enum.Parse(typeof(Robot.Direction), args[2], true);

            if(table.IsValidCell(x, y))
            {
                robot.Place(table.GetCell(x, y), direction);
            }
       }
       else if(robot.IsPlaced)
       {
            if(methodName == "MOVE")
            {
                Table.Cell neighbouringCell = table.GetCellInDirection(robot.CurrentCell, robot.CurrentlyFacing);

                if(table.IsValidCell(neighbouringCell))
                {
                    robot.CurrentCell = neighbouringCell; 
                }         
            }
            else if(methodName == "LEFT")
            {
                robot.Left();
            }
            else if(methodName == "RIGHT")
            {
                robot.Right();
            }
            else if(methodName == "REPORT")
            {
                 robot.Report();
            }
       }
    }
}
