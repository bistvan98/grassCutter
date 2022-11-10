using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class MoveGrassCutter
    {
        // Moves the grass cutter up.
        public static (char[,], int, int, int) moveUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    if (garden[x - 1, y] == '-')
                    {
                        noGrass++;
                    }
                    else if (garden[x - 1, y] == 'G')
                    {
                        noGrass = 0;
                    }

                    garden[x, y] = '-';
                    x--;
                    garden[x, y] = '^';

                    break;

                // Two radius move.
                case 2:
                    if (garden[x - 1, y] == '-')
                    {
                        char[] way = new char[2] { 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, 2);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                    }
                    else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                    }

                    break;
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

                    break;

                // Two radius move.
                case 2:
                    if (garden[x + 1, y] == '-')
                    {
                        char[] way = new char[2] { 'x', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, 2);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                    }
                    else if (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                    }

                    break;
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

                    break;

                // Two radius move.
                case 2:
                    if (garden[x, y - 1] == '-')
                    {
                        char[] way = new char[2] { 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 2, 2);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                    }
                    else if(garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y--;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                    }

                    break;
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

                    break;

                // Two radius move.
                case 2:
                    if (garden[x, y + 1] == '-')
                    {
                        char[] way = new char[2] { 'y', 'y' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, 2);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '>';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        garden[x, y] = '-';
                        x++;
                        garden[x, y] = 'V';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        y++;
                        garden[x, y] = '<';
                        GardenMap.drawGarden(garden);
                        System.Threading.Thread.Sleep(150);
                        garden[x, y] = '-';
                        x--;
                        garden[x, y] = '^';
                    }

                    break;
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
                    Console.WriteLine("Right down 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    length = 2;

                    if (CheckingEnvironment.checkRightGrass(garden, x, y, 1) || CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 1, length);
                    }
                    else if (CheckingEnvironment.checkDownGrass(garden, x, y, 1) || CheckingEnvironment.checkDownFinishedGrass(garden, x, y))
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

                    if (CheckingEnvironment.checkRightGrass(garden, x, y, 1) || CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                    {
                        char[] way = new char[2] { 'y', 'x' };
                        (garden, x, y) = moveCutter(garden, x, y, way, 0, length);
                    }
                    else if (CheckingEnvironment.checkUpGrass(garden, x, y, 1) || CheckingEnvironment.checkUpFinishedGrass(garden, x, y))
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

                    if (CheckingEnvironment.checkLeftGrass(garden, x, y, 1) || CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
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

                    if (CheckingEnvironment.checkLeftGrass(garden, x, y, 1) || CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
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
                            GardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y++;
                            garden[x, y] = '>';
                            GardenMap.drawGarden(garden);
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
                            GardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y++;
                            garden[x, y] = '>';
                            GardenMap.drawGarden(garden);
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
                            GardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y--;
                            garden[x, y] = '<';
                            GardenMap.drawGarden(garden);
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
                            GardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                        else
                        {
                            garden[x, y] = '-';
                            y--;
                            garden[x, y] = '<';
                            GardenMap.drawGarden(garden);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;
            }

            return (garden, x, y);
        }
    }
}
