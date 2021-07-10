using System.Text;

namespace CompileBlazorInBlazor.Demo
{
    public class RunClass
    {
        public string Run(string name, int count)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sb.AppendLine($"{i}) Hello, {name}!");
            }

            return sb.ToString();
        }
    }
}