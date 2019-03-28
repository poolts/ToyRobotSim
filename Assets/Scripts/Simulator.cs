using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public struct Command
    {
        public string Name;
        public Dictionary<string, string> Arguments;

        public Command(string name) : this (name, new Dictionary<string, string>())
        {
        }

        public Command(string name, Dictionary<string, string> args)
        {
            Name = name;
            Arguments = args;
        }
    }

    /// <summary>
    /// Generates a table and runs the commands found in the command text file.
    /// </summary>
    public void Start()
    {
        m_table.Generate(5, 5);

        List<Command> commands = new List<Command>()
        {
            new Command("PLACE",
                new Dictionary<string, string> {
                    { "X", "1" },
                    { "Y", "1" },
                    { "Z", "1" },
                    { "FACING", "North" }
                }
            ),
            new Command("REPORT")
        };

        RunCommands(commands, m_robot, m_table);
    }

    /// <summary>
    /// Runs a set of commands provided.
    /// </summary>
    /// <param name="commandText">The set of commands presented as a string.</param>
    /// <param name="robot">The robot in the simulation.</param>
    /// <param name="table">The table in the simulation.</param>
    /*public void RunCommands(string commandText, Robot robot, Table table)
    {
        List<Command> commands = new List<Command>();

        // Commands are split by line (as formatted in the text file).
        string[] commandLines = commandText.Split(new string[] {"\n"}, System.StringSplitOptions.None);

        // Iterate through each command and split it into the method name and an arguments provided
        foreach(string line in commandLines)
        {
            string[] command = line.Split();

            string name = "";
            Dictionary<string, string> args = new Dictionary<string, string>();

            if(command.Length > 0)
            {
               name = command[0];

               if(command.Length > 1)
               {
                    // If arguments are provided split them by a comma
                   args = command[1].Split(',').ToDictionary(v => v, v => item.Value);
               }
            }

            if(!string.IsNullOrEmpty(name))
            {
                commands.Add(new Command(name, args));
            }
        }

        RunCommands(commands, robot, table);
    }*/

    public void RunCommands(List<Command> commands, Robot robot, Table table)
    {
        foreach(Command c in commands)
        {
            RunCommand(c, robot, table);
        }
    }

    /// <summary>
    /// Runs a provided command.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments with the provided method.</param>
    /// <param name="robot">The robot in the simulation.</param>
    /// <param name="table">The table in the simulation.</param>
    public void RunCommand(Command command, Robot robot, Table table)
    {
       if(command.Name == "PLACE")
       {
            int x = -1;
            int y = -1;

            Robot.Direction direction = Robot.Direction.North;

            int.TryParse(command.Arguments["X"], out x);
            int.TryParse(command.Arguments["Y"], out y);
            System.Enum.TryParse("FACING", true, out direction);

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
            if(command.Name == "MOVE")
            {
                Table.Cell neighbouringCell = table.GetNeighbouringCellInDirection(robot.CurrentCell, robot.CurrentlyFacing);

                if(table.IsValidCell(neighbouringCell))
                {
                    robot.CurrentCell = neighbouringCell; 
                }         
            }
            else if(command.Name == "LEFT")
            {
                robot.Left();
            }
            else if(command.Name == "RIGHT")
            {
                robot.Right();
            }
            else if(command.Name == "REPORT")
            {
                 robot.Report();
            }
       }
    }
}
