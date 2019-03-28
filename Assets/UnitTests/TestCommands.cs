using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestCommands
    {/*
        Simulator m_simulator;

        Robot m_robot;

        Table m_table;

        // Initialises a test. Each time a test is run a simulator, robot and table are newly created.
        [SetUp]
        public void Init()
        {
            m_simulator = new GameObject("Simulator").AddComponent<Simulator>();

            m_robot = new GameObject("Robot").AddComponent<Robot>();

            m_table = new GameObject("Table").AddComponent<Table>();

            m_table.Generate(5, 5);   
        }

        [Test]
        public void Robot_Reports_Correctly_After_Runnning_Command_Set()
        {
            m_simulator.RunCommands("PLACE 0,0,EAST\nMOVE\nMOVE\nLEFT\nMOVE\nLEFT\nMOVE\nRIGHT\nRIGHT\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "2,1,East");
        }

        [Test]
        public void Robot_Reports_Correctly_After_Running_Multiple_Command_Sets()
        {
            m_simulator.RunCommands("PLACE 1,2,EAST\nMOVE\nMOVE\nLEFT\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "3,3,North");

            m_simulator.RunCommands("LEFT\nLEFT\nMOVE\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "3,1,South");
        }

        [Test]
        public void Robot_Reports_Correctly_After_Being_Placed_More_Than_Once()
        {
            m_simulator.RunCommands("PLACE 0,0,NORTH\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0,0,North");

            m_simulator.RunCommands("PLACE 2,1,EAST\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "2,1,East");
        }

        [Test]
        public void Robot_Turns_Full_Circle_Left()
        {
            m_simulator.RunCommands("PLACE 0,0,NORTH\nLEFT\nLEFT\nLEFT\nLEFT\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0,0,North");
        }

        [Test]
        public void Robot_Turns_Full_Circle_Right()
        {
            m_simulator.RunCommands("PLACE 0,0,NORTH\nRIGHT\nRIGHT\nRIGHT\nRIGHT\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0,0,North");
        }

        [Test]
        public void Robot_Moves_North_One_More_Than_Table_Length()
        {
            int highestCellYIndex = m_table.GetTableLength() - 1;

            m_simulator.RunCommands("PLACE 0," + highestCellYIndex + ",NORTH\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0," + highestCellYIndex + ",North");
        }

        [Test]
        public void Robot_Moves_East_One_More_Than_Table_Width()
        {
            int highestCellXIndex = m_table.GetTableWidth() - 1;

            m_simulator.RunCommands("PLACE " + highestCellXIndex + ",0,EAST\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, highestCellXIndex + ",0,East");
        }

        [Test]
        public void Robot_Moves_South_One_Less_Than_Table_Length()
        {
            m_simulator.RunCommands("PLACE 0,0,SOUTH\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0,0,South");
        }

        [Test]
        public void Robot_Moves_West_One_Less_Than_Table_Width()
        {
            m_simulator.RunCommands("PLACE 0,0,WEST\nMOVE\nREPORT", m_robot, m_table);

            LogAssert.Expect(LogType.Log, "0,0,West");
        }

        [Test]
        public void Robot_Left_Before_Place_Should_Be_Ignored_And_Log_Error()
        {
            Robot.Direction directionToFace = Robot.Direction.North;

            m_robot.Left();
            
            Assert.AreEqual(m_robot.CurrentlyFacing, directionToFace);

            LogAssert.Expect(LogType.Error, "Robot cannot turn left until it has been placed on the table");
        }

        [Test]
        public void Robot_Right_Before_Place_Should_Be_Ignored_And_Log_Error()
        {
            Robot.Direction directionToFace = Robot.Direction.North;

            m_robot.CurrentlyFacing = directionToFace;

            m_robot.Right();
            
            Assert.AreEqual(m_robot.CurrentlyFacing, directionToFace);

            LogAssert.Expect(LogType.Error, "Robot cannot turn right until it has been placed on the table");
        }

        [Test]
        public void Robot_Move_Before_Place_Should_Be_Ignored()
        {
            m_robot.CurrentCell = m_table.GetCell(0, 0);

            m_simulator.RunCommands("MOVE", m_robot, m_table);

            Assert.AreEqual(m_robot.CurrentCell, m_table.GetCell(0, 0));
        }

        [Test]
        public void Robot_Report_Before_Place_Should_Log_Error()
        {
            m_robot.Report();

            LogAssert.Expect(LogType.Error, "Robot cannot report until it has been placed on the table");
        }

        [Test]
        public void Simulator_Should_Not_Place_Robot_Invalid_Cell()
        {
            int cellX = -1;
            int cellY = -1;

            m_simulator.RunCommands("PLACE " + cellX + "," + cellY + ",NORTH", m_robot, m_table);

            LogAssert.Expect(LogType.Error, "Tried to place robot at " + cellX + "," + cellY + " which is invalid");
        }*/
    }
}