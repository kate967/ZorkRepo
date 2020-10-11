using System;
using System.Numerics;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace Zork
{
    class Program
    {
        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        private enum Fields
        {
            Name = 0,
            Description
        }

        private static Room[,] Rooms = {
            {new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View")},
            {new Room("Forest"), new Room("West of House"), new Room("Behind House")},
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };

        private static void InitializeRooms(string roomFilename) =>
            Rooms = JsonConvert.DeserializeObject<Room[,]>(File.ReadAllText(roomFilename));

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
            
            const string defaultRoomsFilename = "Rooms.json";
            string roomsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);
            InitializeRooms(roomsFilename);

            Room prevRoom = null;
            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);

                if (prevRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    prevRoom = CurrentRoom;
                }
                
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;
                    case Commands.LOOK:
                        Console.WriteLine (CurrentRoom.Description);
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

        private static Room CurrentRoom
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
                case Commands.NORTH when playerPos.row > 0:
                    playerPos.row--;
                    break;
                case Commands.SOUTH when playerPos.row < Rooms.GetLength(0) - 1 :
                    playerPos.row++;
                    break;
                case Commands.EAST when playerPos.col < Rooms.GetLength(0) - 1:
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