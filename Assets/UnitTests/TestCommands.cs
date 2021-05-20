using System;
using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

namespace RobotSimulation.Tests
{
    public class TestCommands
    {
        private Simulator _simulator;

        private Robot _robot;

        private Table _table;

        // Initialises a test. Each time a test is run a simulator, robot and table are newly created.
        [SetUp]
        public void Init()
        {
            _simulator = new GameObject("Simulator").AddComponent<Simulator>();

            _robot = new GameObject("Robot").AddComponent<Robot>();

            _table = new GameObject("Table").AddComponent<Table>();

            _table.Generate(5, 5);
        }

        [Test]
        public void Robot_Reports_Correctly_After_Runnning_Command_Set()
        {
            var commands = FormatCommands("PLACE 0,0", "EAST", "MOVE", "MOVE", "LEFT", "MOVE", "LEFT", "MOVE", "RIGHT",
                "RIGHT", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "2,1,East");
            });
        }

        [Test]
        public void Robot_Reports_Correctly_After_Running_Multiple_Command_Sets()
        {
            var commands = FormatCommands("PLACE 1,2", "EAST", "MOVE", "MOVE", "LEFT", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "3,3,North");
            });

            commands = FormatCommands("LEFT", "LEFT", "MOVE", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "3,1,South");
            });
        }

        [Test]
        public void Robot_Reports_Correctly_After_Being_Placed_More_Than_Once()
        {
            var commands = FormatCommands("PLACE 0,0", "NORTH", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0,0,North");
            });

            commands = FormatCommands("PLACE 2,1", "EAST", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "2,1,East");
            });
        }

        [Test]
        public void Robot_Turns_Full_Circle_Left()
        {
            var commands = FormatCommands("PLACE 0,0", "NORTH", "LEFT", "LEFT", "LEFT", "LEFT", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0,0,North");
            });
        }

        [Test]
        public void Robot_Turns_Full_Circle_Right()
        {
            var commands = FormatCommands("PLACE 0,0", "NORTH", "RIGHT", "RIGHT", "RIGHT", "RIGHT", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0,0,North");
            });
        }

        [Test]
        public void Robot_Moves_North_One_More_Than_Table_Length()
        {
            var highestCellYIndex = _table.GetTableLength() - 1;

            var commands = FormatCommands("PLACE 0," + highestCellYIndex, "NORTH", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0," + highestCellYIndex + ",North");
            });
        }

        [Test]
        public void Robot_Moves_East_One_More_Than_Table_Width()
        {
            var highestCellXIndex = _table.GetTableWidth() - 1;

            var commands = FormatCommands("PLACE " + highestCellXIndex + ",0", "EAST", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, highestCellXIndex + ",0,East");
            });
        }

        [Test]
        public void Robot_Moves_South_One_Less_Than_Table_Length()
        {
            var commands = FormatCommands("PLACE 0,0", "SOUTH", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0,0,South");
            });
        }

        [Test]
        public void Robot_Moves_West_One_Less_Than_Table_Width()
        {
            var commands = FormatCommands("PLACE 0,0", "WEST", "MOVE", "REPORT");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Log, "0,0,West");
            });
        }

        [UnityTest]
        public IEnumerator Robot_Left_Before_Place_Should_Be_Ignored_And_Log_Error()
        {
            const Robot.Facing directionToFace = Robot.Facing.North;

            yield return _robot.Left();

            Assert.AreEqual(_robot.CurrentlyFacing, directionToFace);

            LogAssert.Expect(LogType.Error, "Robot cannot turn until it has been placed on the table");
        }

        [UnityTest]
        public IEnumerator Robot_Right_Before_Place_Should_Be_Ignored_And_Log_Error()
        {
            const Robot.Facing directionToFace = Robot.Facing.North;

            _robot.CurrentlyFacing = directionToFace;

            yield return _robot.Right();

            Assert.AreEqual(_robot.CurrentlyFacing, directionToFace);

            LogAssert.Expect(LogType.Error, "Robot cannot turn until it has been placed on the table");
        }

        [UnityTest]
        public IEnumerator Robot_Move_Before_Place_Should_Be_Ignored()
        {
            _robot.CurrentCell = _table.GetCell(0, 0);

            _ = _simulator.RunCommands("MOVE", _robot, _table);
            
            yield return null;

            Assert.AreEqual(_robot.CurrentCell, _table.GetCell(0, 0));
        }

        [Test]
        public void Robot_Report_Before_Place_Should_Log_Error()
        {
            _robot.Report();

            LogAssert.Expect(LogType.Error, "Robot cannot report until it has been placed on the table");
        }

        [Test]
        public void Simulator_Should_Not_Place_Robot_Invalid_Cell()
        {
            const int cellX = -1;
            const int cellY = -1;

            var commands = FormatCommands($"PLACE {cellX},{cellY}", "NORTH");

            _ = _simulator.RunCommands(commands, _robot, _table, () =>
            {
                LogAssert.Expect(LogType.Error, $"Tried to place robot at {cellX},{cellY} which is invalid");
            });
        }

        public string FormatCommands(params string[] commands)
        {
            return string.Join(Environment.NewLine, commands);
        }
    }
}