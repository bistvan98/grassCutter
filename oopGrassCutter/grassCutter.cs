using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace oopGrassCutter
{
    internal class grassCutter
    {
        public int x; // The grass cutters current x coordinate.
        public int y; // The grass cutters current y coordinate.
        public int noGrass; // Variable to count how many already cut grass field the grass cutter touched in a row.
        public int routeChangeNumber; // A variable to count the forced preferred way changes.
        public int way; // Variable that defines the preferred way.

        public grassCutter(int x, int y, int noGrass, int routeChangeNumber, int way)
        {
            this.x = x;
            this.y = y;
            this.noGrass = noGrass;
            this.routeChangeNumber = routeChangeNumber;
            this.way = way;
        }
    }
}
