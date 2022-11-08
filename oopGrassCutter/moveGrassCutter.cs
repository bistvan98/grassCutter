using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class moveGrassCutter
    {
        /*
         * This part of the code contains the functions that calls the function that moves the grass cutter with the correct input or moves the grass cutter if it only moves one area unit.
         * The logic behind these functions are the same, comments will be only at the first and the fifth function.
        */

        // Moves the grass cutter up.
        public static (char[,], int, int, int) moveUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    // If the part where the grass cutter would move is an already cut grass part, then the noGrass variables value will be increased by one.
                    if (garden[x - 1, y] == '-')
                    {
                        noGrass++;
                    }
                    // If the next part contains uncut grass, then the noGrass variables value will be zero (it breaks the "can't find any uncut grass" chain).
                    else if (garden[x - 1, y] == 'G')
                    {
                        noGrass = 0;
                    }

                    // Changes the part where the grass cutter were, and the part where it will be.
                    garden[x, y] = '-';
                    x--;
                    garden[x, y] = '^';

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    char[] way = new char[2] { 'x', 'x' }; // To move up by two area unit, the grass cutter need to change it's row coordinate two times.
                    (garden, x, y) = moveCutter(garden, x, y, way, 0, 2); // Calling the mover function.

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter down.
        public static (char[,], int, int, int) moveDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    if (garden[x + 1, y] == '-')
                    {
                        noGrass++;
                    }
                    else if (garden[x + 1, y] == 'G')
                    {
                        noGrass = 0;
                    }

                    garden[x, y] = '-';
                    x++;
                    garden[x, y] = 'V';

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    char[] way = new char[2] { 'x', 'x' };
                    (garden, x, y) = moveCutter(garden, x, y, way, 1, 2);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the left.
        public static (char[,], int, int, int) moveLeft(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    if (garden[x, y - 1] == '-')
                    {
                        noGrass++;
                    }
                    else if (garden[x, y - 1] == 'G')
                    {
                        noGrass = 0;
                    }

                    garden[x, y] = '-';
                    y--;
                    garden[x, y] = '<';

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    char[] way = new char[2] { 'y', 'y' };
                    (garden, x, y) = moveCutter(garden, x, y, way, 2, 2);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the right.
        public static (char[,], int, int, int) moveRight(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    if (garden[x, y + 1] == '-')
                    {
                        noGrass++;
                    }
                    else if (garden[x, y + 1] == 'G')
                    {
                        noGrass = 0;
                    }

                    garden[x, y] = '-';
                    y++;
                    garden[x, y] = '>';

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    char[] way = new char[2] { 'y', 'y' };
                    (garden, x, y) = moveCutter(garden, x, y, way, 0, 2);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to bottom right.
        public static (char[,], int, int, int) moveRightDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            noGrass = 0;
            int length;

            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Right down 1 runs."); // Writes on the console that it found an uncut grass at the right down direction.
                    System.Threading.Thread.Sleep(500);

                    length = 2;

                    // Checks if theres a free way to the right.
                    if (checkingEnvironment.checkRightGrass(garden, x, y, 1) || checkingEnvironment.checkRightFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    // If the right way is blocked then it will check the part down to the grass cutter. It should be clear.
                    else if (checkingEnvironment.checkDownGrass(garden, x, y, 1) || checkingEnvironment.checkDownFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }

                    break;

                // Two radius move.
                case 2:
                    Console.WriteLine("Right down 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 4;

                    // There are five different paths to reach the uncut grass at the top corner, if it finds one free path, it will call the mover function with the correct coordinate changeing order.
                    if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'y', 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'x', 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    // There are two more possible place for an uncut grass next to the top corner with three - three possible ways
                    else if (garden[x, y + 2] != 'X' && garden[x + 1, y + 2] == 'G')
                    {
                        length = 3;

                        if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-')
                        {
                            char[] way = new char[3] { 'y', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                        else if (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                        else if (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                    }
                    else if (garden[x + 2, y] != 'X' && garden[x + 2, y + 1] == 'G')
                    {
                        length = 3;

                        if (garden[x + 1, y] == '-' && garden[x + 2, y] == '-')
                        {
                            char[] way = new char[3] { 'x', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                        else if (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                        else if (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                        }
                    }

                    break;
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to top right.
        public static (char[,], int, int, int) moveRightUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            noGrass = 0;
            int length;

            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Top right 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 2;

                    if (checkingEnvironment.checkRightGrass(garden, x, y, 1) || checkingEnvironment.checkRightFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (checkingEnvironment.checkUpGrass(garden, x, y, 1) || checkingEnvironment.checkUpFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }

                    break;

                // Two radius move.
                case 2:
                    Console.WriteLine("Top right 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 4;

                    if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'y', 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'x', 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (garden[x, y + 2] != 'X' && garden[x - 1, y + 2] == 'G')
                    {
                        length = 3;

                        if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-')
                        {
                            char[] way = new char[3] { 'y', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                        else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                        else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                    }
                    else if (garden[x - 2, y] != 'X' && garden[x - 2, y + 1] == 'G')
                    {
                        length = 3;

                        if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-')
                        {
                            char[] way = new char[3] { 'x', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                        else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                        else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                        }
                    }

                    break;
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to bottom left.
        public static (char[,], int, int, int) moveLeftDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            noGrass = 0;
            int length;

            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Bottom left 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 2;

                    if (checkingEnvironment.checkLeftGrass(garden, x, y, 1) || checkingEnvironment.checkLeftFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else
                    {
                        char[] way = new char[2] { 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }

                    break;

                // Two radius move.
                case 2:
                    Console.WriteLine("Bottom left 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 4;

                    if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'y', 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'x', 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                    }
                    else if (garden[x, y - 2] != 'X' && garden[x + 1, y - 2] == 'G')
                    {
                        length = 3;

                        if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-')
                        {
                            char[] way = new char[3] { 'y', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                        else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                        else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                    }
                    else if (garden[x + 2, y] != 'X' && garden[x + 2, y - 1] == 'G')
                    {
                        length = 3;

                        if (garden[x + 1, y] == '-' && garden[x + 2, y] == '-')
                        {
                            char[] way = new char[3] { 'x', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                        else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                        else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 3, length);
                        }
                    }

                    break;
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to top left.
        public static (char[,], int, int, int) moveLeftUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            noGrass = 0;
            int length;

            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Top left 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 2;

                    if (checkingEnvironment.checkLeftGrass(garden, x, y, 1) || checkingEnvironment.checkLeftFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else
                    {
                        char[] way = new char[2] { 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }

                    break;

                // Two radius move.
                case 2:
                    Console.WriteLine("Top left 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 4;

                    if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'y', 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'x', 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'x', 'y', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        char[] way = new char[4] { 'y', 'x', 'x', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                    }
                    else if (garden[x, y - 2] != 'X' && garden[x - 1, y - 2] == 'G')
                    {
                        length = 3;

                        if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-')
                        {
                            char[] way = new char[3] { 'y', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                        else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                        else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                    }
                    else if (garden[x - 2, y] != 'X' && garden[x - 2, y - 1] == 'G')
                    {
                        length = 3;

                        if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-')
                        {
                            char[] way = new char[3] { 'x', 'x', 'y' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                        else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'y', 'x', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                        else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-')
                        {
                            char[] way = new char[3] { 'x', 'y', 'x' };
                            (garden, x, y) = moveCutter(garden, x, y, way, 2, length);
                        }
                    }

                    break;
            }

            return (garden, x, y, noGrass);
        }

        // Function that moves the grass cutter.
        public static (char[,], int, int) moveCutter(char[,] garden, int x, int y, char[] way, int direction, int length)
        {
            switch (direction)
            {
                // Top right.
                case 0:
                    for (int i = 0; i < length; i++)
                    {
                        if (way[i] == 'x')
                        {
                            garden[x, y] = '-';
                            x--;
                            garden[x, y] = '^';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y++;
                            garden[x, y] = '>';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Right down.
                case 1:
                    for (int i = 0; i < length; i++)
                    {
                        if (way[i] == 'x')
                        {
                            garden[x, y] = '-';
                            x++;
                            garden[x, y] = 'V';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y++;
                            garden[x, y] = '>';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Top left.
                case 2:
                    for (int i = 0; i < length; i++)
                    {
                        if (way[i] == 'x')
                        {
                            garden[x, y] = '-';
                            x--;
                            garden[x, y] = '^';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y--;
                            garden[x, y] = '<';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Bottom left.
                case 3:
                    for (int i = 0; i < length; i++)
                    {
                        if (way[i] == 'x')
                        {
                            garden[x, y] = '-';
                            x++;
                            garden[x, y] = 'V';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y--;
                            garden[x, y] = '<';
                            gardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;
            }

            return (garden, x, y);
        }
    }
}
