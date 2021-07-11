using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor.Demo
{
    public class Sphere
    {
        public double Radius { get; set; }
        public int U { get; set; }
        public int V { get; set; }
        public string Material { get; set; }

        public Sphere(double r, int u, int v, string color)
        {
            this.Radius = r;
            this.U = u;
            this.V = v;
            this.Material = color;
        }

    }
}
