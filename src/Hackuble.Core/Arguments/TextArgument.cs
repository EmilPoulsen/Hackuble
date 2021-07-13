using System;
using System.Collections.Generic;
using System.Text;

namespace Hackuble.Arguments
{
    public class TextArgument : AbstractArgument<string>
    {

        public TextArgument(string prompt, string description, string defaultValue)
            : base(prompt, description, defaultValue)
        {

        }

        public override void RenderArgumentInput()
        {
            throw new NotImplementedException();
        }
    }
}
