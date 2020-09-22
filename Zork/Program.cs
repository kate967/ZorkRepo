using System;
using System.Numerics;

namespace Zork
{
    class Program
    {
        private static string[] Rooms = {"Forest", "West of House", "Behind House", "Clearing", "Canyon View" };
        private static int playerPos = 1;

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");
            Console.WriteLine(Rooms[playerPos]);

            Commands command = Commands.UNKOWN;
            while (command != Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                string outputString = Rooms[playerPos];

                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing.";
                        break;
                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command))
                            outputString = "You moved " + command + ".\n" + Rooms[playerPos];
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
                case Commands.SOUTH:
                    return false;
                case Commands.EAST:
                    if((playerPos + 1) > 4)
                    {
                        return false;
                    }
                    else 
                    {
                        playerPos++;
                        return true;
                    }
                case Commands.WEST:
                    if ((playerPos - 1) < 0)
                    {
                        return false;
                    }
                    else
                    {
                        playerPos--;
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