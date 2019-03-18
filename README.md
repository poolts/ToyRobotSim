# ToyRobotSim

The project simulates a toy robot moving around a table.

## Getting Started

Clone the repository (should be on the master branch by default) and open the root folder in Unity.

Commands can be entered in the Commands.txt file.

- Each command must be on a new line.
- A space must be inserted between the command name and arguments.
- Arguments must be seperated by a comma with no spaces between arguments.

### Example

PLACE 1,2,NORTH\
MOVE\
LEFT\
RIGHT\
REPORT

## Testing
A series of editor mode unit tests have been included (they can be run in Unity's test runner).

The test source code can be found in ./UnitTests/TestCommands.cs