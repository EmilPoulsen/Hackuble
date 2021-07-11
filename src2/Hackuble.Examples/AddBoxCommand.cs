namespace CompileBlazorInBlazor.Demo.Examples
{
    public class AddCube : AbstractCommand
    {
        public override string Name => "Add Cube";

        public override string Author => "Hackathon21";

        public override string Description => "Add a cuboid to the scene";

        public override string CommandLineName => "cube";

        public override string Accent => "#77F798";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("Size X", "The size of the cube in X direction", 20.0);
            dataAccess.RegisterNumberArgument("Size Y", "The size of the cube in Y direction", 20.0);
            dataAccess.RegisterNumberArgument("Size Z", "The size of the cube in Z direction", 20.0);
            dataAccess.RegisterTextArgument("Color", "The color of the cube in Hex Format", "#ff6700");
        }

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double x = -1;
            double y = -1;
            double z = -1;
            string c = "#ffffff";
            if (!dataAccess.GetData<double>(0, ref x))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<double>(1, ref y))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<double>(2, ref z))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<string>(3, ref c))
            {
                return CommandStatus.Failure;
            }

            context.AddCube(x, y, z, 0, 0, 0, c);
            return CommandStatus.Success;
        }
    }

    public class AddCubeDef : AbstractCommand
    {
        public override string Name => "Add Cube";

        public override string Author => "Hackathon21";

        public override string Description => "Add a cuboid to the scene";

        public override string CommandLineName => "cubedef";

        public override string Accent => "#ff6700";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterTextArgument("Color", "The color of the cube in Hex Format", "#ff6700");
        }

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            string c = "#ffffff";
            if (!dataAccess.GetData<string>(0, ref c))
            {
                return CommandStatus.Failure;
            }

            context.AddCube(20.0, 20.0, 20.0, 0, 0, 0, c);
            return CommandStatus.Success;
        }
    }

    //{"command":"cubedef",
    //"data":[
    //"#ff6700"
    //]
    //}

    public class AddSphere : AbstractCommand
    {
        public override string Name => "Add Sphere";

        public override string Author => "Hackathon21";

        public override string Description => "Add a sphere to the scene";

        public override string CommandLineName => "sphere";

        public override string Accent => "#D4C966";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("Radius", "The size of the sphere in X direction", 20.0);
            dataAccess.RegisterIntArgument("U", "The size of the sphere in Y direction", 32);
            dataAccess.RegisterIntArgument("V", "The size of the sphere in Z direction", 32);
            dataAccess.RegisterTextArgument("Color", "The color of the sphere in Hex Format", "#ff6700");
        }

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double r = -1;
            int u = -1;
            int v = -1;
            string c = "#ffffff";
            if (!dataAccess.GetData<double>(0, ref r))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<int>(1, ref u))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<int>(2, ref v))
            {
                return CommandStatus.Failure;
            }
            if (!dataAccess.GetData<string>(3, ref c))
            {
                return CommandStatus.Failure;
            }

            context.AddSphere(r, u, v, c);
            return CommandStatus.Success;
        }
    }
}