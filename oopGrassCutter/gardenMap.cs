using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class gardenMap
    {
        public static char[,] garden { get; set; } = new char[10, 10]; // The garden will be a 10x10 sized character matrix.

        // Fills the matrix simulating the garden with elements.
        public (int, int) fillGarden()
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
    }
}
