using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Arguments
{
    public class IntegerArgument : AbstractArgument<int>
    {

        public IntegerArgument(string prompt, string description, int defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}
