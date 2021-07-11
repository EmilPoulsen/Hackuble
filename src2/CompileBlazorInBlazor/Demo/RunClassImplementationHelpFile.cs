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
        }

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double x = -1;
            if (!dataAccess.GetData<double>(0, ref x))
            {
                return CommandStatus.Failure;
            }

            context.AddCube(x, 20, 20, 20, 20, 20);
            return CommandStatus.Success;
        }
    }
}