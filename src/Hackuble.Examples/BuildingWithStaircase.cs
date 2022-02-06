using System;
using System.Collections.Generic;
using System.Text;
using Hackuble.Commands;

namespace Hackuble.Examples
{
    public class BuildingWithStaircase : AbstractCommand
    {
        public override string Name => "Create Building with Staircase";

        public override string Author => "Filipe Brandão";

        public override string Description => "Place new building in the scene";

        public override string CommandLineName => "addBuildingWithStairCase";

        public override string Accent => "#988F64"; //"#FF96AD"

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("BaseX", "Length of the Base in X", 20);
            dataAccess.RegisterNumberArgument("BaseY", "Length of the Base in Y", 20);
            dataAccess.RegisterNumberArgument("FC", "Floor to ceiling height", 2.7);
            dataAccess.RegisterIntArgument("NumFloors ", "Number of Floors", 3);
            dataAccess.RegisterBooleanArgument("Stair", "Staircase", false);
        }
        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double baseX = 20;
            double baseY = 20;
            double fc = 3;
            int numFloors = 20;
            bool stairs = false;

            if (!dataAccess.GetData<double>(0, ref baseX))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<double>(1, ref baseY))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<double>(2, ref fc))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<int>(3, ref numFloors))
            {
                return CommandStatus.Failure;
            }

            if (!dataAccess.GetData<bool>(4, ref stairs))
            {
                return CommandStatus.Failure;
            }

            double currElev = 0;
            double slabT = 0.3; //slab thickness
            for (int i = 0; i < numFloors; i++)
            {
                context.AddCube(baseX, baseY, slabT, 0, 0, currElev + slabT, "#0390fc");
                //context.AddCube(0, 0, currElev + slabT, baseX, baseY, slabT, "#0390fc");
                currElev += fc;
            }

            if (stairs)
            {
                context.AddCube(6, 3, numFloors * (fc + slabT), 0, 0, numFloors * (fc + slabT) /2, "#0390fc");
                //context.AddCube(0, 0, numFloors * (fc + slabT) / 2, 6, 3, numFloors * (fc + slabT), "#0390fc");
            }

            return CommandStatus.Success;
        }
    }
}
