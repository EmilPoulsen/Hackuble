using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor.Demo
{
    public class Cube
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Height { get; set; }
        public string Material { get; set; }

        public Cube(double x, double y, double z, double width, double depth, double height, string color)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Width = width;
            this.Depth = depth;
            this.Height = height;
            this.Material = color;
        }
    }
}
