﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RobotSimulation
{
    /// <summary>
    /// A simulator processes commands and controls the the robot and table accordingly. 
    /// </summary>
    public class Simulator : MonoBehaviour
    {
        [SerializeField] private Table _table = null;

        [SerializeField] private Robot _robot = null;

        [SerializeField] private TextAsset _commandText = null;

        public struct Command
        {
            public string Name;
            public Dictionary<string, string> Arguments;

            public Command(string name, Dictionary<string, string> args)
            {
                Name = name;
                Arguments = args;
            }
        }

        /// <summary>
        /// Generates a table and runs the commands found in the command text file.
        /// </summary>
        private void Start()
        {
            _table.Generate(5, 5);

            StartCoroutine(RunCommands(_commandText.text, _robot, _table));
        }

        /// <summary>
        /// Runs a set of commands provided.
        /// </summary>
        /// <param name="commandText">The set of commands presented as a string.</param>
        /// <param name="robot">The robot in the simulation.</param>
        /// <param name="table">The table in the simulation.</param>
        public IEnumerator RunCommands(string commandText, Robot robot, Table table, Action commandsComplete = null)
        {
            var commands = new List<Command>();

            // Commands are split by line (as formatted in the text file).
            var commandLines = commandText.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

            // Iterate through each command and split it into the method name and an arguments provided
            foreach (var line in commandLines)
            {
                var command = line.Split();

                var commandName = "";
                var args = new Dictionary<string, string>();

                if (command.Length > 0)
                {
                    commandName = command[0];

                    if (commandName == "PLACE" && command.Length > 1)
                    {
                        var argsRawText = command[1].Split(',');

                        if (argsRawText.Length > 2)
                        {
                            args = new Dictionary<string, string>
                            {
                                {"X", argsRawText[0]},
                                {"Y", argsRawText[1]},
                                {"FACING", argsRawText[2]}
                            };
                        }
                    }
                }

                if (!string.IsNullOrEmpty(commandName))
                {
                    commands.Add(new Command(commandName, args));
                }
            }

            yield return StartCoroutine(RunCommands(commands, robot, table));

            commandsComplete?.Invoke();
        }

        public IEnumerator RunCommands(List<Command> commands, Robot robot, Table table)
        {
            foreach (var command in commands)
            {
                yield return StartCoroutine(RunCommand(command, robot, table));
            }
        }

        /// <summary>
        /// Runs a provided command.
        /// </summary>
        /// <param name="command">The command to be ran</param>
        /// <param name="robot">The robot in the simulation.</param>
        /// <param name="table">The table in the simulation.</param>
        public IEnumerator RunCommand(Command command, Robot robot, Table table)
        {
            if (command.Name == "PLACE")
            {
                int.TryParse(command.Arguments["X"], out var x);
                int.TryParse(command.Arguments["Y"], out var y);
                Enum.TryParse("FACING", true, out Robot.Facing direction);

                if (table.IsValidCell(x, y))
                {
                    robot.Place(table.GetCell(x, y), direction);
                }
                else
                {
                    Debug.LogError($"Tried to place robot at {x},{y} which is invalid");
                }
            }
            else if (robot.IsPlaced)
            {
                switch (command.Name)
                {
                    case "MOVE":
                    {
                        var neighbouringCell =
                            table.GetNeighbouringCellInDirection(robot.CurrentCell, robot.CurrentlyFacing);

                        if (table.IsValidCell(neighbouringCell))
                        {
                            yield return StartCoroutine(robot.Move(neighbouringCell));
                        }

                        break;
                    }
                    case "LEFT":
                        yield return StartCoroutine(robot.Left());
                        break;
                    case "RIGHT":
                        yield return StartCoroutine(robot.Right());
                        break;
                    case "REPORT":
                        robot.Report();
                        break;
                }
            }
            else
            {
                Debug.LogError("Robot is trying to be controlled, but has not yet placed");
            }

            yield return null;
        }
    }
}