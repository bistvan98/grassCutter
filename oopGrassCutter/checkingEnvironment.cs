using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class CheckingEnvironment
    {
        // Checks, if the place in up from the grass cutter contains uncut grass or not.
        public static bool checkUpGrass(char[,] garden, int x, int y, int radius)
        {
            switch (radius)
            {
                // One radius check.
                case 1:
                    if (garden[x - 1, y] == 'G')
                    {
                        return true;
                    }

                    break;

                // Two radius check
                case 2:
                    if (garden[x - 1, y] != 'X')
                    {
                        if(garden[x - 2, y] == 'G')
                        {
                            if (garden[x - 1, y] == '-')
                            {
                                return true;
                            }
                            else if (garden[x, y - 1] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 2, y - 1] == '-')
                            {
                                return true;
                            }
                            else if (garden[x, y + 1] == '-' && garden[x - 1, y + 1] == '-' && garden[x - 2, y + 1] == '-')
                            {
                                return true;
                            }
                        }
                    }

                    break;
            }

            return false;
        }

        // Checks, if the place down from the grass cutter contains uncut grass or not.
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

                    break;

                // Two radius check.
                case 2:
                    if (garden[x + 1, y] != 'X')
                    {
                        if(garden[x + 2, y] == 'G')
                        {
                            if (garden[x + 1, y] == '-')
                            {
                                return true;
                            }
                            else if(garden[x, y - 1] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 2, y - 1] == '-')
                            {
                                return true;
                            }
                            else if(garden[x, y + 1] == '-' && garden[x + 1, y + 1] == '-' && garden[x + 2, y + 1] == '-')
                            {
                                return true;
                            }
                        }
                    }

                    break;
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

                    break;

                // Two radius check.
                case 2:
                    if (garden[x, y - 1] != 'X')
                    {
                        if(garden[x, y - 2] == 'G')
                        {
                            if (garden[x, y - 1] == '-')
                            {
                                return true;
                            }
                            else if(garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                            {
                                return true;
                            }
                            else if(garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                            {
                                return true;
                            }
                        }                     
                    }

                    break;
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

                    break;

                // Two radius check.
                case 2:
                    if (garden[x, y + 1] != 'X')
                    {
                        if (garden[x, y + 2] == 'G')
                        {
                            if(garden[x, y + 1] == '-')
                            {
                                return true;
                            }
                            else if(garden[x - 1, y] == '-' && garden[x - 1, y - 1] == '-' && garden[x - 1, y - 2] == '-')
                            {
                                return true;
                            }
                            else if(garden[x + 1, y] == '-' && garden[x + 1, y - 1] == '-' && garden[x + 1, y - 2] == '-')
                            {
                                return true;
                            }                          
                        }
                    }

                    break;
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
                    if (garden[x + 1, y - 1] == 'G' && (checkDownFinishedGrass(garden, x, y) || checkLeftFinishedGrass(garden, x, y)))
                    {
                        return true;
                    }

                    break;

                // Two radius check
                case 2:
                    if (garden[x, y - 1] != 'X' && garden[x + 1, y] != 'X')
                    {
                        if (garden[x, y - 2] != 'X' && garden[x + 2, y] != 'X' && garden[x + 2, y - 2] == 'G')
                        {
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

                    break;
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

                    break;

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

                    break;
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

                    break;

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

                    break;
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

                    break;

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

                    break;
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
