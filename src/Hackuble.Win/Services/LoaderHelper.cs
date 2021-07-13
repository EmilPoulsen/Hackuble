using Hackuble.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hackuble.Win.Services
{
    public class LoaderHelper
    {
        private CompileHelper _compileHelper;

        public LoaderHelper(CompileHelper compileHelper)
        {
            _compileHelper = compileHelper;
        }

        public IEnumerable<AbstractCommand> Load(MemoryStream ms)
        {
            List<AbstractCommand> foundCommands = new List<AbstractCommand>();

            var assembly = System.Reflection.Assembly.Load(ms.ToArray());
            System.Diagnostics.Trace.WriteLine(assembly.FullName);

            foreach (var type in assembly.GetTypes())
            {
                System.Diagnostics.Trace.WriteLine(type.FullName);

                if (typeof(AbstractCommand).IsAssignableFrom(type))
                {
                    System.Diagnostics.Trace.WriteLine($"Loading {type.FullName}");
                    var command = _compileHelper.CreateRunClass(type);
                    foundCommands.Add(command);
                }
            }
            return foundCommands;
        }
    }
}
