using Hackuble.Commands;

namespace Hackuble.Examples
{
    public class AddSphere : AbstractCommand
    {
        public override string Name => "Add Sphere";

        public override string Author => "Hackathon21";

        public override string Description => "Add a sphere to the scene";

        public override string CommandLineName => "sphere";

        public override string Accent => "#FF96AD";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
            dataAccess.RegisterNumberArgument("Radius", "The size of the sphere in X direction", 20.0);
            dataAccess.RegisterIntArgument("U", "The size of the sphere in Y direction", 32);
            dataAccess.RegisterIntArgument("V", "The size of the sphere in Z direction", 32);
            dataAccess.RegisterTextArgument("Color", "The color of the sphere in Hex Format", "#FF96AD");
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