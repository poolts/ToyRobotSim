using System.Collections;
using UnityEngine;

namespace RobotSimulation
{
    /// <summary>
    /// A robot can be placed and moved around a table.
    /// </summary>
    public class Robot : MonoBehaviour
    {
        public enum Facing
        {
            North,
            East,
            South,
            West
        }

        [SerializeField] private float m_timeToMove = 1f, m_timeToTurn = 1f;

        public Facing CurrentlyFacing { get; set; }

        public Table.Cell CurrentCell { get; set; }

        public bool IsPlaced { get; private set; }

        /// <summary>
        /// Places the robot on the cell.
        /// </summary>
        /// <param name="cell">The cell to place the robot on.</param>
        /// <param name="facing">The direction the robot should face.</param>
        public void Place(Table.Cell cell, Facing facing)
        {
            CurrentCell = cell;

            CurrentlyFacing = facing;

            IsPlaced = true;

            transform.position = cell.WorldPosition + new Vector3(0f, 0.25f, 0f);

            transform.gameObject.SetActive(true);
        }

        public IEnumerator Turn(Facing toFace)
        {
            if (IsPlaced)
            {
                yield return RotateToFace(toFace);
            }
            else
            {
                Debug.LogError("Robot cannot turn until it has been placed on the table");
            }
        }

        /// <summary>
        /// Turns the robot to the left.
        /// </summary>
        public IEnumerator Left()
        {
            yield return Turn(CurrentlyFacing == Facing.North ? Facing.West : CurrentlyFacing - 1);
        }

        /// <summary>
        /// Turns the robot to the right.
        /// </summary>
        public IEnumerator Right()
        {
            yield return Turn(CurrentlyFacing == Facing.West ? Facing.North : CurrentlyFacing + 1);
        }

        /// <summary>
        /// Reports the report's current cell (X and Y coordinates) and the direction it is currently facing.
        /// </summary>
        public void Report()
        {
            if (IsPlaced)
            {
                Debug.Log(CurrentCell + "," + CurrentlyFacing);
            }
            else
            {
                Debug.LogError("Robot cannot report until it has been placed on the table");
            }
        }

        public IEnumerator Move(Table.Cell cell)
        {
            // Set the robot's world position to the cell's world position.
            // Add an offset to ensure it's standing on the cell.
            var from = transform.position;
            var to = cell.WorldPosition + new Vector3(0f, 0.25f, 0f);

            var timer = 0f;

            while (timer < m_timeToMove)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(from, to, timer / m_timeToMove);
                yield return null;
            }

            transform.position = to;

            CurrentCell = cell;

            yield return null;
        }

        public IEnumerator RotateToFace(Facing toFace)
        {
            var yRotation = toFace switch
            {
                Facing.North => 0f,
                Facing.East => 90f,
                Facing.South => 180f,
                Facing.West => 270f,
                _ => 0f
            };

            var from = transform.rotation;
            var to = Quaternion.Euler(from.eulerAngles.x, yRotation, from.eulerAngles.z);
            var timer = 0f;

            while (timer < m_timeToMove)
            {
                timer += Time.deltaTime;
                transform.rotation = Quaternion.Lerp(from, to, timer / m_timeToMove);
                yield return null;
            }

            transform.rotation = to;

            CurrentlyFacing = toFace;
        }
    }
}