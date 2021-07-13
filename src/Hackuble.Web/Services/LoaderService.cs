using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Hackuble.Commands;

namespace Hackuble.Web
{
    public class LoaderService
    {
        private CompileService _compileService;

        public LoaderService(CompileService compileService)
        {
            _compileService = compileService;
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
                    var command = _compileService.CreateRunClass(type);
                    foundCommands.Add(command);
                }
            }
            return foundCommands;
        }
    }
}
