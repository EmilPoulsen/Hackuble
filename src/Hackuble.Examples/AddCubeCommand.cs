using Hackuble.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Examples
{
    public class AddCubeCommand : AbstractCommand
    {
        public override string Name => "Add cube";

        public override string CommandLineName => "cube";

        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            context.AddCube(20, 20, 20, 0, 0, 0, "#FF96AD");
            return CommandStatus.Success;
        }
    }
}
