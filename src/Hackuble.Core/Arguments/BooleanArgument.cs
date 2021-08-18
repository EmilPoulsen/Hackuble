using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Arguments
{
    public class BooleanArgument : AbstractArgument<bool>
    {
        public BooleanArgument(string prompt, string description, bool defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}
