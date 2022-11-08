using oopGrassCutter;
using System;
using System.Security.Cryptography.X509Certificates;

namespace probamatrix
{
    class Program
    {
        static void Main(string[] args)
        {
            bool finished = false; // A boolean variable to decide if the grass cutter is finished or not.

            GrassCutter cutter = new GrassCutter(1, 1, 0, 0, 1); // Object that represents the grass cutter.
            GardenMap garden = new GardenMap(); // Object that represents the garden.
            (cutter.x, cutter.y) = garden.fillGarden(); // Fills the garden with elements.

            /* Legend:
             * G = uncut grass
             * X = fence
             * O = obstacle
             * - = cut grass */

            // Main iteration.
            do
            {
                GardenMap.drawGarden(GardenMap.garden);
                (cutter.x, cutter.y, cutter.way, cutter.noGrass) = choosePath(GardenMap.garden, cutter.x, cutter.y, cutter.way, cutter.noGrass);
                Console.WriteLine("No grass field counted: " + cutter.noGrass);

                // The grass cutter is forced to change it's preferred way if it does not find any uncut grass 30 times in a row.
                if (cutter.noGrass == 30)
                {
                    bool newWay = false; // Variable to make sure, that the new randomly generated way will not be the same as the current.
                    do
                    {
                        Random rand = new Random();
                        int number = rand.Next(0, 4);

                        if (number != cutter.way)
                        {
                            Console.WriteLine("Using new path to search for more grass. Old way: " + cutter.way + " New way: " + number);
                            cutter.way = number;
                            newWay = true;
                            cutter.noGrass = 0;
                            cutter.routeChangeNumber++; // Forced route changes variable value increased by one.
                        }
                    } while (!newWay);
                }

                System.Threading.Thread.Sleep(500);

                // If there were 5 forced route changes, then the grass cutter will stop suspecting that every uncut grass were cut already
                // (can't find an uncut grass in a while).
                if (cutter.routeChangeNumber == 5)
                {
                    finished = true;
                }
            } while (!finished);
        }

        public static (int, int, int, int) choosePath(char[,] garden, int x, int y, int way, int noGrass)
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
                        if (CheckingEnvironment.checkDownGrass(garden, x, y, i)) // Checks the first prioritised path.
                        {
                            Console.WriteLine("Down " + i); // The program writes to the console the choosen path and it's radius.
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, i); // Calling the right function to move the grass cutter to the right way.
                            System.Threading.Thread.Sleep(150);

                            didTask = true; // Variable, which tells that the grass cutter found uncut grass and a way to it.
                            break; // Breaks the for iteration, no need to check with a higher radius this time.
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftGrass(garden, x, y, i)) // If the first prioritised path isn't appropriate, then it will check the second.
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right.
                        else if (CheckingEnvironment.checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);
                            way = 1;

                            didTask = true;
                            break;
                        }
                        // Bottom left.
                        else if (CheckingEnvironment.checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom right.
                        else if (CheckingEnvironment.checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top left.
                        else if (CheckingEnvironment.checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top right.
                        else if (CheckingEnvironment.checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass in it's two radius environment, then it will search for an already cut part (1 radius).
                    if (!didTask)
                    {
                        // Left.
                        if (CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 2;
                        }
                        // Down.
                        else if (CheckingEnvironment.checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 1;
                        }
                    }
                    break;

                // Focus on right.
                case 1:
                    for (int i = 1; i < 3; i++)
                    {
                        // Down.
                        if (CheckingEnvironment.checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Right.
                        else if (CheckingEnvironment.checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);
                            way = 0;

                            didTask = true;
                            break;
                        }
                        // Bottom left.
                        else if (CheckingEnvironment.checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom right.
                        else if (CheckingEnvironment.checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top left.
                        else if (CheckingEnvironment.checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top right.
                        else if (CheckingEnvironment.checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if (!didTask)
                    {
                        // Right.
                        if (CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Down.
                        else if (CheckingEnvironment.checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 3;
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 0;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Focus on up.
                case 2:
                    for (int i = 1; i < 3; i++)
                    {
                        // Right.
                        if (CheckingEnvironment.checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Down.
                        else if (CheckingEnvironment.checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom left.
                        else if (CheckingEnvironment.checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom right.
                        else if (CheckingEnvironment.checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top left.
                        else if (CheckingEnvironment.checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top right.
                        else if (CheckingEnvironment.checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if (!didTask)
                    {
                        // Up.
                        if (CheckingEnvironment.checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, 1);
                            way = 1;
                            System.Threading.Thread.Sleep(150);
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Down.
                        else if (CheckingEnvironment.checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;

                // Focus on down.
                case 3:
                    for (int i = 1; i < 3; i++)
                    {
                        // Right.
                        if (CheckingEnvironment.checkRightGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Left.
                        if (CheckingEnvironment.checkLeftGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Down.
                        else if (CheckingEnvironment.checkDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Down " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Up " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom left..
                        else if (CheckingEnvironment.checkLeftDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Bottom right.
                        else if (CheckingEnvironment.checkRightDownGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Bottom right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightDown(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top left.
                        else if (CheckingEnvironment.checkLeftUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top left " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeftUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                        // Top right.
                        else if (CheckingEnvironment.checkRightUpGrass(garden, x, y, i))
                        {
                            Console.WriteLine("Top right " + i);
                            System.Threading.Thread.Sleep(150);

                            (garden, x, y, noGrass) = MoveGrassCutter.moveRightUp(garden, x, y, noGrass, i);
                            System.Threading.Thread.Sleep(150);

                            didTask = true;
                            break;
                        }
                    }

                    // If the grass cutter can't find an uncut grass near it's position then it will search for an already cut part.
                    if (!didTask)
                    {
                        // Down.
                        if (CheckingEnvironment.checkDownFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveDown(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Left.
                        else if (CheckingEnvironment.checkLeftFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveLeft(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                            way = 0;
                        }
                        // Up.
                        else if (CheckingEnvironment.checkUpFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveUp(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                        // Right.
                        else if (CheckingEnvironment.checkRightFinishedGrass(garden, x, y))
                        {
                            (garden, x, y, noGrass) = MoveGrassCutter.moveRight(garden, x, y, noGrass, 1);
                            System.Threading.Thread.Sleep(150);
                        }
                    }
                    break;
            }

            return (x, y, way, noGrass);
        }
    }
}