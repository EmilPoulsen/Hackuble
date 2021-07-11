namespace CompileBlazorInBlazor.Demo.Examples
{
    public class CreateBuildingCommand : AbstractCommand
    {
        public override string Name => "Create building";

        public override string Author => "Emil Poulsen";

        public override string Description => "Add a building to the scene";

        public override string CommandLineName => "addbuilding";

        public override string Accent => "#D4C966";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("BaseX", "Base X dimension", 20);
            dataAccess.RegisterNumberArgument("BaseY", "Base X dimension", 20);
            dataAccess.RegisterNumberArgument("Fl-Fl", "Floor to floor", 3);
            dataAccess.RegisterIntArgument("NumFloors ", "Number of floors", 3);
        }

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double baseX = 20;
            double baseY = 20;
            double flfl = 3;
            int numStories = 20;

            if (!dataAccess.GetData<double>(0, ref baseX))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<double>(1, ref baseY))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<double>(2, ref flfl))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<int>(3, ref numStories))
            {
                return CommandStatus.Failure;
            }

            int numStoriesInt = numStories;

            double currElev = flfl;
            for (int i = 0; i < numStoriesInt; i++)
            {
                context.AddCube(baseX, baseY, 0.5, 0, 0, currElev, "#0390fc");
                currElev += flfl;
            }

            return CommandStatus.Success;
        }
    }
}