using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Arguments
{
    public class RangeArgument : AbstractArgument<double>
    {
        public RangeArgument(string prompt, string description, double defaultValue)
            : this(prompt, description, defaultValue, defaultValue - 10, defaultValue +10)
        {

        }

        public RangeArgument(string prompt, string description, double defaultValue, double minValue, double maxValue)
            : base(prompt, description, defaultValue)
        {
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public double MinValue { get; set; }
        public double MaxValue { get; set; }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}
