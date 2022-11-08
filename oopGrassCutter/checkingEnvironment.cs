using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class CheckingEnvironment
    {
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

        // Checks, if the place bottom left to the grass cutter contains uncut grass or not.
        public static bool checkLeftDownGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check
                case 1:
                    /*
                     * If the part bottom left to the grass cutter contains uncut grass, and if theres a way there
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
                         * If the part in two radius left and down to the grass cutter aren't part of the fence, and the bottom left part in two radius is an uncut grass,
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
                        else if (garden[x, y - 2] != 'X' && garden[x + 1, y - 2] == 'G')
                        {
                            if (
                                (garden[x, y - 1] == '-' && garden[x, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                        else if (garden[x + 2, y] != 'X' && garden[x + 2, y - 1] == 'G')
                        {
                            if (
                                (garden[x + 1, y] == '-' && garden[x + 2, y] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-')
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

        // Checks, if the place bottom right to the grass cutter contains uncut grass or not.
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
                                (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 1, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                        else if (garden[x, y + 2] != 'X' && garden[x + 1, y + 2] == 'G')
                        {
                            if (
                                (garden[x, y + 1] == '-' && garden[x, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                        else if (garden[x + 2, y] != 'X' && garden[x + 2, y + 1] == 'G')
                        {
                            if (
                                (garden[x + 1, y] == '-' && garden[x + 2, y] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-') ||
                                (garden[x + 1, y] == '-' && garden[x + 1, y + 1] == '-')
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

        // Checks, if the place top left to the grass cutter contains uncut grass or not.
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
                        else if (garden[x, y - 2] != 'X' && garden[x - 1, y - 2] == 'G')
                        {
                            if (
                                (garden[x, y - 1] == '-' && garden[x, y - 2] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                        else if (garden[x - 2, y] != 'X' && garden[x - 2, y - 1] == 'G')
                        {
                            if (
                                (garden[x - 1, y] == '-' && garden[x - 2, y] == '-') ||
                                (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-')
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

        // Checks, if the place top right to the grass cutter contains uncut grass or not.
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
                        else if (garden[x, y + 2] != 'X' && garden[x - 1, y + 2] == 'G')
                        {
                            if (
                                (garden[x, y + 1] == '-' && garden[x, y + 2] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-')
                                )
                            {
                                return true;
                            }
                        }
                        else if (garden[x - 2, y] != 'X' && garden[x - 2, y + 1] == 'G')
                        {
                            if (
                                (garden[x - 1, y] == '-' && garden[x - 2, y] == '-') ||
                                (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-') ||
                                (garden[x - 1, y] == '-' && garden[x - 1, y + 1] == '-')
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
    }
}
