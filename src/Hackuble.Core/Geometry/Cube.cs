using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hackuble.Geometry
{
    public class Cube
    {
        /// <summary>
        /// The X coordinate of the center of the cube
        /// </summary>
        public double X { get; set; }
        /// <summary>
        /// The Y coordinate of the center of the cube
        /// </summary>
        public double Y { get; set; }
        /// <summary>
        /// The Z coordinate of the center of the cube
        /// </summary>
        public double Z { get; set; }
        /// <summary>
        /// The width of the cube
        /// </summary>
        public double Width { get; set; }
        /// <summary>
        /// The depth of the cube
        /// </summary>
        public double Depth { get; set; }
        /// <summary>
        /// The height of the cube
        /// </summary>
        public double Height { get; set; }
        /// <summary>
        /// The material of the cube
        /// </summary>
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
