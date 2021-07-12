using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Arguments
{
    public class NumberArgument : AbstractArgument<double>
    {

        public NumberArgument(string prompt, string description, double defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}
