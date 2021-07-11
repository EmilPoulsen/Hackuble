namespace CompileBlazorInBlazor.Demo
{
    public class MyCommand : AbstractCommand
    {
        public override string Name => "Test Command 01";

        public override string Author => "Hackathon";

        public override string Description => "lol";

        public override string CommandLineName => "test";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("Size X", "The size of the cube in X direction", 20);
            dataAccess.RegisterNumberArgument("Size Y", "The size of the cube in Y direction", 20);
            dataAccess.RegisterNumberArgument("Size Z", "The size of the cube in Z direction", 20);
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

            context.AddCube(x, y, z, 20, 20, 20, c);
            return CommandStatus.Success;
        }
    }
}