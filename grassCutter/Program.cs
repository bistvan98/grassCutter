using System;

namespace grassCutter
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] garden = new char[10, 10]; // Matrix that simulates the garden.
            bool finished = false; // A boolean variable to decide if the grass cutter is finished or not.
            int x, // Starting X coordinate.
                y, // Starting Y coordinate.
                way = 1, // Default preferred way.
                noGrass = 0, // Variable to count how many already cut grass field the grass cutter touched in a row.
                routeChangeNumbers = 0; // A variable to count the forced preferred way changes.

            (x, y) = fillGarden(garden);

            /* Legend:
             * G = uncut grass
             * X = fence
             * O = obstacle
             * - = cut grass */

            // Main iteration.
            do
            {
                drawGarden(garden);
                (x, y, finished, way, noGrass) = choosePath(garden, x, y, finished, way, noGrass);
                Console.WriteLine("No grass field counted: " + noGrass);

                // The grass cutter is forced to change it's preferred way if it does not find any uncut grass 30 times in a row.
                if (noGrass == 30)
                {
                    bool newWay = false; // Variable to make sure, that the new randomly generated way will not be the same as the current.
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
                            routeChangeNumbers++; // Forced route changes variable value increased by one.
                        }
                    } while (!newWay);
                }

                System.Threading.Thread.Sleep(500);

                // If there were 5 forced route changes, then the grass cutter will stop suspecting that every uncut grass were cut already
                // (can't find an uncut grass in a while).
                if(routeChangeNumbers == 5)
                {
                    finished = true;
                }
            } while (!finished);
        }

        #region "Filling And Drawing The Garden"

        // Fills the matrix simulating the garden with elements.
        public static (int, int) fillGarden(char[,] garden)
        {
            Random rand = new Random();
            int number;
            int number2;

            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    // The edge of the garden is always a part of the fence.
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        garden[i, j] = 'X';
                    }
                    else
                    {                       
                        // 0, 2, 3, 4, 5, 6, 7, 8, 9 = grass, 1 = obstacle.
                        number = rand.Next(0, 10);
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

            // Default grass cutter position.
            number = rand.Next(1, 9);
            number2 = rand.Next(1, 9);

            garden[number, number2] = '>';

            return (number, number2);
        }

        // Shows the current state of the garden on the console.
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

        #endregion

        #region "Choosing A Path"

        // Functions to choose a path to go. Every preferred path parts logic are the same, with little changes (path choosing priorities). Comments only at the first.

        // The grass cutter selects the next preferred way to go.
        public static (int, int, bool, int, int) choosePath(char[,] garden, int x, int y, bool finished, int way, int noGrass)
        {
            bool didTask = false; // Checks whether the grass cutter has selected a route or not.

            switch (way)
            {
                // Focus on left.
                case 0:
                    // First the grass cutter checks it's one area unit environment. If it can't find any uncut grass, it will check it's two area unit environment.
                    for (int i = 1; i < 3; i++)
                    {
                        // Down.
                        if (checkDownGrass(garden, x, y, i)) // Checks the first prioritised path.
                        {
                            Console.WriteLine("Down " + i); // The program writes to the console the choosen path and it's radius.
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, i); // Calling the right function to move the grass cutter to the right way.
                            System.Threading.Thread.Sleep(150);

                            didTask = true; // Variable, which tells that the grass cutter found uncut grass and a way to it.
                            break; // Breaks the for iteration, no need to check with a higher radius this time.
                        }
                        // Left.
                        else if (checkLeftGrass(garden, x, y, i)) // If the first prioritised path isn't appropriate, then it will check the second.
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right.
                        else if (checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);
                            way = 1;

                            didTask = true;
                            break;
                        }
                        // Left down.
                        else if (checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right down.
                        else if (checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left up.
                        else if (checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right up.
                        else if (checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass in it's two radius environment, then it will search for an already cut part (1 radius).
                    if (!didTask)
                    {
                        // Left.
                        if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Up.
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 2;
                        }
                        // Down.
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 1;
                        }
                    }
                    break;

                // Focus on right.
                case 1:
                    for (int i = 1; i < 3; i++) {
                        // Down.
                        if (checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right.
                        else if (checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        else if (checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);
                            way = 0;

                            didTask = true;
                            break;
                        }
                        // Left down.
                        else if (checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right down.
                        else if (checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left up.
                        else if (checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right up.
                        else if (checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if(!didTask) 
                    {
                        // Right.
                        if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Down.
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 3;
                        }
                        // Left.
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 0;
                        }
                        // Up.
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Focus on up.
                case 2:
                    for (int i = 1; i < 3; i++) {
                        // Right.
                        if (checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        else if (checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Down.
                        else if (checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left down.
                        else if (checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right down.
                        else if (checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left up.
                        else if (checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right up.
                        else if (checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if(!didTask)
                    {
                        // Up.
                        if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                            way = 1;
                            System.Threading.Thread.Sleep(150);
                        }
                        // Left.
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Down.
                        else if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Focus on down.
                case 3:
                    for (int i = 1; i < 3; i++) {
                        // Right.
                        if (checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        if (checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Down.
                        else if (checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left down.
                        else if (checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right down.
                        else if (checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left up.
                        else if (checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right up.
                        else if (checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if(!didTask)
                    {
                        // Down.
                        if (checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Left.
                        else if (checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 0;
                        }
                        // Up.
                        else if (checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;
            }

            return (x, y, finished, way, noGrass);
        }

        #endregion

        #region "Checking Cut And Uncut Grass"

        #region "Checking Grass"

        /*
         * This part of the code contains the grass cutter environment checker functions. The logic behind these functions are the same,
         * the different functions only checks an other part (for example one checks the left and the other checks the right).
         * From the fifth function, the checking part is a little different, because the functions checks every possible way to the uncut grass,
         * because if the grass cutter can't reach it, then there is no reason to try to get there.
         * The rest functions after the fifth works with the same logic as the fifth. Comments will be only at the first and the fifth function.
        */

        // Checks, if the place in front of the grass cutter contains uncut grass or not.
        public static bool checkUpGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    // At one radius, the grass cutter only checks, that the part front of it contains uncut grass or not.
                    if (garden[x - 1, y] == 'G')
                    {
                        return true;
                    }

                    return false;

                // Two radius check
                case 2:
                    // Checks, if the next place front of the grass cutter is a part of the fence or not.
                    // If it is, then it won't check the part after it, because there can't be any grass.
                    if (garden[x - 1, y] != 'X')
                    {
                        /* 
                         * If the previously checked part isn't a fence part, then it will check that the next part front of the grass cutter is an already cut grass or not.
                         * If not, then it is 100% an obstacle. It can be only obstacle or already cut grass (the grass cutter checked that is that an uncut grass or not before).
                         * If it is an already cut grass, then it will check that thex part after it (front) is an uncut grass or not.
                         * If every branch of the if statements are true, then theres an uncut grass in two radius distance front of the grass cutter, and theres a way to it.
                         */
                        if (garden[x - 1, y] == '-' && garden[x - 2, y] == 'G')
                        {
                            return true;
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place down to the grass cutter contains uncut grass or not.
        public static bool checkDownGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x + 1, y] == 'G')
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x + 1, y] != 'X')
                    {
                        if (garden[x + 1, y] == '-' && garden[x + 2, y] == 'G')
                        {
                            return true;
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place left to the grass cutter contains uncut grass or not.
        public static bool checkLeftGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x, y - 1] == 'G')
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x, y - 1] != 'X')
                    {
                        if (garden[x, y - 1] == '-' && garden[x, y - 2] == 'G')
                        {
                            return true;
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place right to the grass cutter contains uncut grass or not.
        public static bool checkRightGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x, y + 1] == 'G')
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x, y + 1] != 'X')
                    {
                        if (garden[x, y + 1] == '-' && garden[x, y + 2] == 'G')
                        {
                            return true;
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place left down to the grass cutter contains uncut grass or not.
        public static bool checkLeftDownGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check
                case 1:
                    /*
                     * If the part left down to the grass cutter contains uncut grass, and if theres a way there
                     * (the part down  or the part left to the grass cutter is an already cut grass, there can't be uncut grass, it was checked before this)
                     * then theres a new uncut grass part in reach and theres a way to it.
                     */
                    if (garden[x + 1, y - 1] == 'G' && (checkDownFinishedGrass(garden, x, y) || checkLeftFinishedGrass(garden, x, y)))
                    {
                        return true;
                    }

                    return false;

                // Two radius check
                case 2:
                    // If the parts left and down to the grass cutter aren't parts of the fence, then it can check in a higer radius.
                    if (garden[x, y - 1] != 'X' && garden[x + 1, y] != 'X')
                    {
                        /*
                         * If the part in two radius left and down to the grass cutter aren't part of the fence, and the left down part in two radius is an uncut grass,
                         * then it can search for possible ways to it. The rest part can't be a fence.
                         */
                        if (garden[x, y - 2] != 'X' && garden[x + 2, y] != 'X' && garden[x + 2, y - 2] == 'G')
                        {
                            // Checks every possible way. If it finds one, then it will return with true.
                            if (
                                (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x + 1, y - 2] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y - 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place right down to the grass cutter contains uncut grass or not.
        public static bool checkRightDownGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x + 1, y + 1] == 'G' && ((checkDownGrass(garden, x, y, 1) || checkDownFinishedGrass(garden, x, y)) || (checkRightGrass(garden, x, y, 1) || checkRightFinishedGrass(garden, x, y))))
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x, y + 1] != 'X' && garden[x + 1, y] != 'X')
                    {
                        if (garden[x, y + 2] != 'X' && garden[x + 2, y] != 'X' && garden[x + 2, y + 2] == 'G')
                        {
                            if (
                                (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x + 1, y + 2] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y + 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-') ||
                                (garden[x, y +1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place left up to the grass cutter contains uncut grass or not.
        public static bool checkLeftUpGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x - 1, y - 1] == 'G' && (((checkUpGrass(garden, x, y, 1) || checkUpFinishedGrass(garden, x, y))) || (checkLeftGrass(garden, x, y, 1) || checkLeftFinishedGrass(garden, x, y))))
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x, y - 1] != 'X' && garden[x - 1, y] != 'X')
                    {
                        if (garden[x, y - 2] != 'X' && garden[x - 2, y] != 'X' && garden[x - 2, y - 2] == 'G')
                        {
                            if (
                                (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x - 1, y - 2] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y - 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                    }

                    return false;
            }

            return false;
        }

        // Checks, if the place right up to the grass cutter contains uncut grass or not.
        public static bool checkRightUpGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x - 1, y + 1] == 'G' && ((checkRightGrass(garden, x, y, 1) || checkRightFinishedGrass(garden, x, y)) || (checkUpGrass(garden, x, y, 1) || checkUpFinishedGrass(garden, x, y))))
                    {
                        return true;
                    }

                    return false;

                // Two radius check.
                case 2:
                    if (garden[x, y + 1] != 'X' && garden[x - 1, y] != 'X')
                    {
                        if (garden[x, y + 2] != 'X' && garden[x - 2, y] != 'X' && garden[x - 2, y + 2] == 'G')
                        {
                            if (
                                (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x - 1, y + 2] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y + 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                    }

                    return false;
            }

            return false;
        }

        #endregion

        #region "Checking Finished Grass"

        // Checks, if the place in front of the grass cutter contains already cut grass or not.
        public static bool checkUpFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x - 1, y] == '-')
            {
                return true;
            }

            return false;
        }

        // Checks, if the place behind the grass cutter contains already cut grass or not.
        public static bool checkDownFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x + 1, y] == '-')
            {
                return true;
            }

            return false;
        }

        // Checks, if the place left to the grass cutter contains already cut grass or not.
        public static bool checkLeftFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y - 1] == '-')
            {
                return true;
            }

            return false;
        }

        // Checks, if the place right to the grass cutter contains already cut grass or not.
        public static bool checkRightFinishedGrass(char[,] garden, int x, int y)
        {
            if (garden[x, y + 1] == '-')
            {
                return true;
            }

            return false;
        }

        #endregion

        #endregion

        #region "Moving The Grass Cutter"

        /*
         * This part of the code contains the functions that moves the grass cutter. The logic behind these functions are the same,
         * the different functions only moves the grass cutter to an other direction (for example one moves to the left and the other moves to the right).
         * From the fifth function, the moving part is a little different, because the functions checks every possible way to the uncut grass,
         * because there can be paths, that are blocked by some obstacles. The rest functions after the fifth works with the same logic as the fifth. Comments will be only at the first and the fifth function.
        */

        // Moves the grass cutter up.
        public static (char[,], int, int, int) moveUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
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
                    // Calling the move up by one area unit function. There are no obstacle or fence, it was checked before the moving.
                    (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                    drawGarden(garden); // Drawing the garden on the console to see the moves between the starting and final position.

                    System.Threading.Thread.Sleep(500);
                    (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1); // Moving up by one again.
                    drawGarden(garden); // Drawing again to get the fresh status of the garden.

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter down.
        public static (char[,], int, int, int) moveDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
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
                    (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    System.Threading.Thread.Sleep(150);
                    (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the left.
        public static (char[,], int, int, int) moveLeft(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
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
                    (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    System.Threading.Thread.Sleep(500);
                    (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to the right.
        public static (char[,], int, int, int) moveRight(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
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
                    (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    System.Threading.Thread.Sleep(500);
                    (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                    drawGarden(garden);

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to rigt down.
        public static (char[,], int, int, int) moveRightDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Right down 1 runs."); // Writes on the console that it found an uncut grass at the right down direction.
                    System.Threading.Thread.Sleep(500);

                    // Checks if theres a free way to the right.
                    if (checkRightGrass(garden, x, y, 1) || checkRightFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1); // If there's a clear way to the right, then it will move that way first.
                        drawGarden(garden); // Draws the current status of the garden on the console to follow the moves.

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1); // Moves down where's the uncut grass. It was checked before the move function.
                    }
                    // If the right way is blocked then it will check the part down to the grass cutter. It should be clear.
                    else if (checkDownGrass(garden, x, y, 1) || checkDownFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1); // Moves to down.
                        drawGarden(garden); // Draws the fresh garden status on the console.

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1); // Movves right where's the uncut grass is.
                    }

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    Console.WriteLine("Right down 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if(garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if(garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if(garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if(garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if(garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to right up.
        public static (char[,], int, int, int) moveRightUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Right up 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (checkRightGrass(garden, x, y, 1) || checkRightFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                    }
                    else if (checkUpGrass(garden, x, y, 1) || checkUpFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                    }

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    Console.WriteLine("Right up 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (garden[x, y + 1] == '-' && garden[x, y + 2] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 1, y + 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveRight(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to left down.
        public static (char[,], int, int, int) moveLeftDown(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch(radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Left down 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (checkLeftGrass(garden, x, y, 1) || checkLeftFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                    }
                    else
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                    }

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    Console.WriteLine("Left down 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 2, y] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveDown(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }

                    return (garden, x, y, noGrass);
            }

            return (garden, x, y, noGrass);
        }

        // Moves the grass cutter to left up.
        public static (char[,], int, int, int) moveLeftUp(char[,] garden, int x, int y, int noGrass, int radius)
        {
            switch (radius)
            {
                // One radius move.
                case 1:
                    Console.WriteLine("Left up 1 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (checkLeftGrass(garden, x, y, 1) || checkLeftFinishedGrass(garden, x, y))
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                    }
                    else
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                    }

                    return (garden, x, y, noGrass);

                // Two radius move.
                case 2:
                    Console.WriteLine("Left up 2 runs.");
                    System.Threading.Thread.Sleep(500);

                    if (garden[x, y - 1] == '-' && garden[x, y - 2] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);
                        System.Threading.Thread.Sleep(500);

                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);
                        System.Threading.Thread.Sleep(500);

                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);
                        System.Threading.Thread.Sleep(500);

                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 2, y] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                    {
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveUp(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        System.Threading.Thread.Sleep(500);
                        (garden, x, y, noGrass) = moveLeft(garden, x, y, noGrass, 1);
                        drawGarden(garden);

                        return (garden, x, y, noGrass);
                    }
                    return (garden,x, y, noGrass);
            }

                   return (garden, x, y, noGrass);
        }

        #endregion
    }
}