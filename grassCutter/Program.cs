using System;

namespace grassCutter
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] garden = new char[10, 10]; // Matrix that simulates the garden
            bool finished = false; // A boolean variable to decide if the grass cutter is finished or not
            int x = 8, // Starting X coordinate
                y = 1, // Starting Y coordinate
                way = 1, // Default preferred way
                noGrass = 0, // Variable to count how many already cut grass field the grass cutter touched in a row
                routeChangeNumbers = 0; // A variable to count the forced preferred way changes

            fillGarden(garden);

            /* Legend:
             * G = uncut grass
             * X = fence
             * O = obstacle
             * - = cut grass */

            do
            {
                drawGarden(garden);
                (x, y, finished, way, noGrass) = choosePath(garden, x, y, finished, way, noGrass);
                Console.WriteLine("No grass field counted: " + noGrass);

                // The grass cutter is forced to change it's preferred way if it does not find any uncut grass 30 times in a row
                if(noGrass == 30)
                {
                    bool newWay = false; // Variable to make sure, that the new randomly generated way will not be the same as the current
                    do
                    {
                        Random rand = new Random();
                        int number = rand.Next(0, 4);

                        if(number != way)
                        {
                            Console.WriteLine("Using new path to search for more grass. Old way: " + way + " New way: " + number);
                            way = number;
                            newWay = true;
                            noGrass = 0;
                            routeChangeNumbers++; // Forced route changes variable value increased by one
                        }
                    } while (!newWay);
                }

                System.Threading.Thread.Sleep(1000);

                // If there were 5 forced route changes, then the grass cutter will stop suspecting that every uncut grass were cut already
                // (can't find an uncut grass in a while)
                if(routeChangeNumbers == 5)
                {
                    finished = true;
                }
            } while (!finished);
        }

        // Fills the matrix simulating the garden with elements
        public static void fillGarden(char[,] garden)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // The edge of the garden is always a part of the fence
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        garden[i, j] = 'X';
                    }
                    else
                    {
                        Random rand = new Random();
                        // 0, 2, 3, 4, 5, 6, 7, 8, 9 = grass, 1 = obstacle
                        int number = rand.Next(0, 10);
                        if (number == 1)
                        {
                            garden[i, j] = 'O';
                        }
                        else
                        {
                            garden[i, j] = 'G';
                        }
                    }
                }
            }

            // Default grass cutter position
            garden[8, 1] = '>';
        }

        // Shows the current state of the garden on the console
        public static void drawGarden(char[,] garden)
        {
            Console.Clear();
            int k = 0;

            foreach (var item in garden)
            {
                k = k + 1;
                if (k == 10)
                {
                    k = 0;
                    Console.WriteLine(item.ToString());
                }
                else
                {
                    Console.Write(item.ToString());
                }
            }

            System.Threading.Thread.Sleep(350);
        }

        // The grass cutter selects the next preferred way to go
        public static (int, int, bool, int, int) choosePath(char[,] garden, int x, int y, bool finished, int way, int noGrass)
        {
            switch (way)
            {
                // Focus on left
                case 0:
                    // Down
                    if (checkDownGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Left
                    else if (checkLeftGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Up
                    else if (checkUpGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Right
                    else if (checkRightGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                        way = 1;
                        System.Threading.Thread.Sleep(350);
                    }
                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part
                    else
                    {
                        // Left
                        if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Up
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                            way = 2;
                            System.Threading.Thread.Sleep(350);
                        }
                        // Down
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Right
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                            way = 1;
                            System.Threading.Thread.Sleep(350);
                        }
                    }
                    break;
                // Focus on right
                case 1:
                    // Down
                    if (checkDownGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Right
                    else if (checkRightGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Up
                    else if (checkUpGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Left
                    else if (checkLeftGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                        way = 0;
                        System.Threading.Thread.Sleep(350);
                    }
                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part
                    else
                    {
                        // Right
                        if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Down
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                            way = 3;
                            System.Threading.Thread.Sleep(350);
                        }
                        // Left
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                            way = 0;
                            System.Threading.Thread.Sleep(350);
                        }
                        // Up
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                    }
                    break;
                // Focus on up
                case 2:
                    // Right
                    if (checkRightGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Left
                    else if (checkLeftGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Up
                    else if (checkUpGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Down
                    else if (checkDownGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part
                    else
                    {
                        // Up
                        if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Right
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                            way = 1;
                            System.Threading.Thread.Sleep(350);
                        }
                        // Left
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Down
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                    }
                    break;
                // Focus on down
                case 3:
                    // Right
                    if (checkRightGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Left
                    if (checkLeftGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Down
                    else if (checkDownGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // Up
                    else if (checkUpGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                        System.Threading.Thread.Sleep(350);
                    }
                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part
                    else
                    {
                        // Down
                        if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Left
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass);
                            way = 0;
                            System.Threading.Thread.Sleep(350);
                        }
                        // Up
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                        // Right
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass);
                            System.Threading.Thread.Sleep(350);
                        }
                    }
                    break;
            }
            return (x, y, finished, way, noGrass);
        }

        // Checks, if the place in front of the grass cutter contains uncut grass or not
        public static bool checkUpGrass(char[,] garden, int x, int y)
        {
            if (garden[x - 1, y] == 'G')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place in front of the grass cutter contains already cut grass or not
        public static bool checkUpFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x - 1, y] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place behind the grass cutter contains uncut grass or not
        public static bool checkDownGrass(char[,] garden, int x, int y)
        {
            if (garden[x + 1, y] == 'G')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place behind the grass cutter contains already cut grass or not
        public static bool checkDownFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x + 1, y] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place left to the grass cutter contains uncut grass or not
        public static bool checkLeftGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y - 1] == 'G')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place left to the grass cutter contains already cut grass or not
        public static bool checkLeftFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y - 1] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place right to the grass cutter contains uncut grass or not
        public static bool checkRightGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y + 1] == 'G')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Checks, if the place right to the grass cutter contains already cut grass or not
        public static bool checkRightFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y + 1] == '-')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Moves the grass cutter up
        public static (char[,], int, int, int) moveUp(char[,] garden, int x, int y, int noGrass)
        {
            // If the part where the grass cutter would move is an already cut grass part, then the noGrass variables value will be increased by one
            if (garden[x - 1, y] == '-')
            {
                noGrass++;
            }
            // If the next part contains uncut grass, then the noGrass variables value will be zero (it breaks the "can't find any uncut grass" chain)
            else if (garden[x - 1, y] == 'G')
            {
                noGrass = 0;
            }

            // Changes the part where the grass cutter were, and the part where it will be
            garden[x, y] = '-';
            x = x - 1;
            garden[x, y] = '^';

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter down
        public static (char[,], int, int, int) moveDown(char[,] garden, int x, int y, int noGrass)
        {
            // If the part where the grass cutter would move is an already cut grass part, then the noGrass variables value will be increased by one
            if (garden[x + 1, y] == '-')
            {
                noGrass++;
            }
            // If the next part contains uncut grass, then the noGrass variables value will be zero (it breaks the "can't find any uncut grass" chain)
            else if (garden[x + 1, y] == 'G')
            {
                noGrass = 0;
            }

            // Changes the part where the grass cutter were, and the part where it will be
            garden[x, y] = '-';
            x = x + 1;
            garden[x, y] = 'V';

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the left
        public static (char[,], int, int, int) moveLeft(char[,] garden, int x, int y, int noGrass)
        {
            // If the part where the grass cutter would move is an already cut grass part, then the noGrass variables value will be increased by one
            if (garden[x, y - 1] == '-')
            {
                noGrass++;
            }
            // If the next part contains uncut grass, then the noGrass variables value will be zero (it breaks the "can't find any uncut grass" chain)
            else if (garden[x, y - 1] == 'G')
            {
                noGrass = 0;
            }

            // Changes the part where the grass cutter were, and the part where it will be
            garden[x, y] = '-';
            y = y - 1;
            garden[x, y] = '<';

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the right
        public static (char[,], int, int, int) moveRight(char[,] garden, int x, int y, int noGrass)
        {
            // If the part where the grass cutter would move is an already cut grass part, then the noGrass variables value will be increased by one
            if (garden[x, y + 1] == '-')
            {
                noGrass++;
            }
            // If the next part contains uncut grass, then the noGrass variables value will be zero (it breaks the "can't find any uncut grass" chain)
            else if (garden[x, y + 1] == 'G')
            {
                noGrass = 0;
            }

            // Changes the part where the grass cutter were, and the part where it will be
            garden[x, y] = '-';
            y = y + 1;
            garden[x, y] = '>';

            return (garden, x, y, noGrass);
        }
    }
}