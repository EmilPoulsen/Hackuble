using System.Collections.Generic;
using System.Text;

namespace CompileBlazorInBlazor.Demo
{
    public abstract class RunClass2
    {
        public abstract void RunCommand(Context context);
    }

    public class Context
    {
        public List<Cube> Cubes { get; private set; }
        public Context()
        {
            this.Cubes = new List<Cube>();
        }

        public void AddCube(double x, double y, double z, double width, double depth, double height)
        {
            Cube cube = new Cube(x, y, z, width, depth, height);
            this.Cubes.Add(cube);
        }
    }

    public class Cube
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Width { get; set; }
        public double Depth { get; set; }
        public double Height { get; set; }

        public Cube(double x, double y, double z, double width, double depth, double height)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Width = width;
            this.Depth = depth;
            this.Height = height;
        }
    }
}