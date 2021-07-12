using Hackuble.Commands;

namespace Hackuble.Examples
{
    public class AddCubeDef : AbstractCommand
    {
        public override string Name => "Add Cube";

        public override string Author => "Hackathon21";

        public override string Description => "Add a cuboid to the scene";

        public override string CommandLineName => "cubedef";

        public override string Accent => "#FF96AD";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterTextArgument("Color", "The color of the cube in Hex Format", "#FF96AD");
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
}