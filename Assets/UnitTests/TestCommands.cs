using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestCommands
    {
        // A Test behaves as an ordinary method
        [Test]
        public void TestCommandsSimplePasses()
        {
            Simulator sim  = new GameObject("Simulator").AddComponent<Simulator>();

            Robot robot = new GameObject("Robot").AddComponent<Robot>();

            Table table = new GameObject("Table").AddComponent<Table>();

            table.Generate(5);

            sim.RunCommands("PLACE 1,2,EAST\nMOVE\nMOVE\nLEFT\nMOVE\nREPORT", robot, table);

            LogAssert.Expect(LogType.Log, "3,3,North");
        }

        // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
        // `yield return null;` to skip a frame.
        [UnityTest]
        public IEnumerator TestCommandsWithEnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}
