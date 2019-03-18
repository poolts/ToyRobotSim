using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simualator processes commands and controls the the robot and table accordingly. 
/// </summary>
public class Simulator : MonoBehaviour
{
    [SerializeField]
    Table m_table = null;

    [SerializeField]
    Robot m_robot = null;

    [SerializeField]
    TextAsset m_commandText = null;

    /// <summary>
    /// Generates a table and runs the commands found in the command text file.
    /// </summary>
    public void Start()
    {
        m_table.Generate(5, 5);

        RunCommands(m_commandText.text, m_robot, m_table);
    }

    /// <summary>
    /// Runs a set of commands provided.
    /// </summary>
    /// <param name="commandText">The set of commands presented as a string.</param>
    /// <param name="robot">The robot in the simulation.</param>
    /// <param name="table">The table in the simulation.</param>
    public void RunCommands(string commandText, Robot robot, Table table)
    {
        // Commands are split by line (as formatted in the text file).
        string[] commands = commandText.Split(new string[] {"\n"}, System.StringSplitOptions.None);

        // Iterate through each command and split it into the method name and an arguments provided
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
                    // If arguments are provided split them by a comma
                   args = line[1].Split(',');
               }
            }

            RunCommand(methodName, args, robot, table);
        }
    }

    /// <summary>
    /// Runs a provided command.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments with the provided method.</param>
    /// <param name="robot">The robot in the simulation.</param>
    /// <param name="table">The table in the simulation.</param>
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
            else
            {
                Debug.LogError("Tried to place robot at " + x + "," + y + " which is invalid");
            }
       }
       else if(robot.IsPlaced)
       {
            if(methodName == "MOVE")
            {
                Table.Cell neighbouringCell = table.GetNeighbouringCellInDirection(robot.CurrentCell, robot.CurrentlyFacing);

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
