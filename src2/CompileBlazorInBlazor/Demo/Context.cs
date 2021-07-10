using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor.Demo
{
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
}
