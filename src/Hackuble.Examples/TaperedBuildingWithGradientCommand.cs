using Hackuble.Commands;

namespace Hackuble.Examples
{
    public class TaperedBuildingWithGradientCommand : AbstractCommand
    {
        public override string Name => "Tapered building.";

        public override string Author => "Emil Poulsen";

        public override string Description => "Create tapered building with gradient colors";

        public override string CommandLineName => "addTaperedBuilding";

        public override string Accent => "#F88F64";

        public override void RegisterInputArguments(DataAccess dataAccess)
        {
        }
        public override CommandStatus RunCommand(Context context, DataAccess dataAccess)
        {
            double baseX = 30; // Base width for the bottom
            double baseY = 20; // Base depth for the bottom
            double flfl = 3;   // Floor height
            int numStories = 20; // Total number of stories

            double currElev = flfl;
            double taperFactor = 0.9; // Controls the tapering rate

            // Define RGB values for start (base) and end (top) colors
            (int rStart, int gStart, int bStart) = (3, 144, 252);  // #0390fc
            (int rEnd, int gEnd, int bEnd) = (252, 186, 3);        // #fcba03

            for (int i = 0; i < numStories; i++)
            {
                // Tapering "legs" of the "A"
                double currentBaseX = baseX * System.Math.Pow(taperFactor, i); // Taper the width each floor
                double currentBaseY = baseY * System.Math.Pow(taperFactor, i); // Taper the width each floor

                double offsetX = (baseX - currentBaseX) / 2; // Shift each floor to center it

                // Calculate the color gradient
                double t = (double)i / (numStories - 1); // Normalized position (0 at base, 1 at top)
                int r = (int)((1 - t) * rStart + t * rEnd);
                int g = (int)((1 - t) * gStart + t * gEnd);
                int b = (int)((1 - t) * bStart + t * bEnd);
                string color = $"#{r:X2}{g:X2}{b:X2}"; // Convert to hex format

                context.AddCube(
                    offsetX,
                    0,
                    currElev,
                    currentBaseX,
                    currentBaseY,
                    2.9,
                    color
                );

                currElev += flfl;
            }

            return CommandStatus.Success;
        }

    }
}
