using System;
using System.Numerics;

namespace Zork
{
    class Program
    {
        private static readonly string[,] Rooms = {
            {"Rocky Trail", "South of House", "Canyon View"},
            {"Forest", "West of House", "Behind House"},
            {"Dense Woods", "North of House", "Clearing" }
        };
        private static (int row, int col) playerPos = (1, 1);

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            Console.WriteLine(Rooms[playerPos.row, playerPos.col]);

            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString = Rooms[playerPos.row, playerPos.col];

                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing.";
                        break;
                    case Commands.LOOK:
                        if (playerPos.row == 2 && playerPos.col == 1)
                            outputString = "A rubber mat saying 'Welcome to Zork!' lies by the door." + "\n" + Rooms[playerPos.row, playerPos.col];
                        else if (playerPos.row == 1 && playerPos.col == 0)
                            outputString = "This is an open field west of a white house, with a boarded front door." + "\n" + Rooms[playerPos.row, playerPos.col];
                        else
                            outputString = "Nothing to look at here.";
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command))
                            outputString = "You moved " + command + ".\n" + Rooms[playerPos.row, playerPos.col] + "\n" + playerPos.row + "," + playerPos.col;
                        else
                            outputString = "The way is shut!";
                        break;
                    default:
                        outputString = "Unknown Command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {
            switch (command)
            {
                case Commands.NORTH:
                    if ((playerPos.row + 1) > 2)
                    {
                        return false;
                    }
                    else
                    {
                        playerPos.row++;
                        return true;
                    }
                case Commands.SOUTH:
                    if((playerPos.row - 1) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        playerPos.row--;
                        return true;
                    }
                case Commands.EAST:
                    if((playerPos.col + 1) > 2)
                    {
                        return false;
                    }
                    else 
                    {
                        playerPos.col++;
                        return true;
                    }
                case Commands.WEST:
                    if ((playerPos.col - 1) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        playerPos.col--;
                        return true;
                    }
                default:
                    return false;
            }
        }

        private static Commands ToCommand(string commandString)
        {
            if (Enum.TryParse<Commands>(commandString, true, out Commands result))
            {
                return result;
            }
            else
            {
                return Commands.UNKOWN;
            }
        }
    }
}