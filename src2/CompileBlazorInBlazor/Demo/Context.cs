using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor.Demo
{
    public class Context
    {
        public List<Cube> Cubes { get; private set; }
        public List<Sphere> Spheres { get; private set; }
        public Context()
        {
            this.Cubes = new List<Cube>();
            this.Spheres = new List<Sphere>();
        }

        public void AddCube(double x, double y, double z, double width, double depth, double height, string color)
        {
            Cube cube = new Cube(x, y, z, width, depth, height, color);
            this.Cubes.Add(cube);
        }

        public void AddSphere(double r, int u, int v, string color)
        {
            Sphere sphere = new Sphere(r, u, v, color);
            this.Spheres.Add(sphere);
        }
    }
}
