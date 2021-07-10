using CompileBlazorInBlazor.Demo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompileBlazorInBlazor
{
    public class CommandService
    {
        public List<RunClass> Commands { get; set; }

        public CommandService()
        {
            this.Commands = new List<RunClass>();
        }

        public void AddCommand(RunClass command)
        {
            this.Commands.Add(command);
        }

        public string GetSummary()
        {
            return $"Number of commands: {this.Commands.Count}";
        }
    }
}
