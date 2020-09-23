using System;
using System.Numerics;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Zork
{
    class Program
    {
        private static readonly string[,] Rooms = {
            {"Rocky Trail", "South of House", "Canyon View"},
            {"Forest", "West of House", "Behind House"},
            {"Dense Woods", "North of House", "Clearing" }
        };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST,
        };
        
        private static (int row, int col) playerPos = (1, 1);

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;
                    case Commands.LOOK:
                        Console.WriteLine ("A rubber mat saying 'Welcome to Zork!' lies by the door.");
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (!Move(command))
                            Console.WriteLine("The way is shut!");
                        break;
                    default:
                        Console.WriteLine("Unknown Command.");
                        break;
                }
            }
        }

        private static string CurrentRoom
        {
            get
            {
                return Rooms[playerPos.row, playerPos.col];
            }
        }

        private static bool IsDirection(Commands command) =>
            Directions.Contains(command);

        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction");

            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when playerPos.row < Rooms.GetLength(0) - 1:
                    playerPos.row++;
                    break;
                case Commands.SOUTH when playerPos.row > 0:
                    playerPos.row--;
                    break;
                case Commands.EAST when playerPos.col < Rooms.GetLength(1) - 1:
                    playerPos.col++;
                    break;
                case Commands.WEST when playerPos.col > 0:
                    playerPos.col--;
                    break;
                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        private static Commands ToCommand(string commandString) => 
            Enum.TryParse(commandString, true, out Commands result) ? result : Commands.UNKOWN;
    }
}