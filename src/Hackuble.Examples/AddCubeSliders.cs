using Hackuble.Commands;
using System;
namespace Hackuble.Examples
{
    public class AddCubeSliders : AbstractCommand
    {
        public override string Name => "Add Cube Slider";

        public override string Author => "Filipe Brandão";

        public override string Description => "Add a cuboid to the scene";

        public override string CommandLineName => "cube-slider";

        public override string Accent => "#FF96AD";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterRangeArgument("Size X", "The size of the cube in X direction", 20.0, 0.0, 40.0);
            dataAccess.RegisterRangeArgument("Size Y", "The size of the cube in Y direction", 20.0, 0.0, 40.0);
            dataAccess.RegisterRangeArgument("Size Z", "The size of the cube in Z direction", 20.0, 0.0, 40.0);
            dataAccess.RegisterTextArgument("Color", "The color of the cube in Hex Format", "#FF96AD");
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

            context.AddCube(0, 0, 0, x, y, z, c);
            return CommandStatus.Success;
        }
    }
}
