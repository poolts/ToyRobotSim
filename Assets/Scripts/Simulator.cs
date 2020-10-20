using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A simualator processes commands and controls the the robot and table accordingly. 
/// </summary>
public class Simulator : MonoBehaviour
{
    [SerializeField]
    private Table m_table = null;

    [SerializeField]
    private Robot m_robot = null;

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

                if (name == "PLACE" && command.Length > 1)
               {
                    string[] argsRawText = command[1].Split(',');

                    if(argsRawText.Length > 2)
                    {
                        args = new Dictionary<string, string>
                        {
                            { "X", argsRawText[0] },
                            { "Y", argsRawText[1] },
                            { "FACING", argsRawText[2] }
                        };
                    }
               }
            }

            if(!string.IsNullOrEmpty(name))
            {
                commands.Add(new Command(name, args));
            }
        }

        StartCoroutine(RunCommands(commands, robot, table));
    }

    public IEnumerator RunCommands(List<Command> commands, Robot robot, Table table)
    {
        foreach(Command c in commands)
        {
            yield return StartCoroutine(RunCommand(c, robot, table));
        }
    }

    /// <summary>
    /// Runs a provided command.
    /// </summary>
    /// <param name="methodName">The name of the method.</param>
    /// <param name="args">The arguments with the provided method.</param>
    /// <param name="robot">The robot in the simulation.</param>
    /// <param name="table">The table in the simulation.</param>
    public IEnumerator RunCommand(Command command, Robot robot, Table table)
    {
        if(command.Name == "PLACE")
        {
            int.TryParse(command.Arguments["X"], out int x);
            int.TryParse(command.Arguments["Y"], out int y);
            System.Enum.TryParse("FACING", true, out Robot.Facing direction);

            if (table.IsValidCell(x, y))
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
                    yield return StartCoroutine(robot.Move(neighbouringCell)); 
                }         
            }
            else if(command.Name == "LEFT")
            {
                yield return StartCoroutine(robot.Left());
            }
            else if(command.Name == "RIGHT")
            {
                yield return StartCoroutine(robot.Right());
            }
            else if(command.Name == "REPORT")
            {
                 robot.Report();
            }
        }
        else
        {
            Debug.LogError("Robot is trying to be controlled, but has not yet placed");
        }

        yield return null;
    }
}
