using Hackuble.Commands;
using Hackuble.Win.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hackuble.Win
{
    public partial class Main : Form
    {

        private int childFormNumber = 0;
        private bool viewportOpen = false;

        DataAccess currentDataAccess;
        AbstractCommand currentCommand;

        public Main()
        {
            Hackuble.compileService = new CompileHelper();
            Hackuble.commandService = new CommandHelper();
            Hackuble.loadService = new LoaderHelper(Hackuble.compileService);
            Hackuble.CurrentContext = new Context();
            InitializeComponent();

            Hackuble.commandService.CommandAdded += CommandService_CommandAdded;
        }

        private void CommandService_CommandAdded(object sender, CommandAddedEventArgs e)
        {
            ToolStripButton t = new ToolStripButton(e.Command.Name, null, null, e.Command.CommandLineName);
            t.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStrip1.Items.Add(t);
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new ScriptEditor();
            childForm.MdiParent = this;
            childForm.Text = "New Command | Script Editor | " + childFormNumber++;
            childForm.Show();
        }

        private async void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "DLL Files (*.dll)|*.dll|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
                using (Stream m = File.OpenRead(FileName))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        await m.CopyToAsync(ms);
                        var loadedCommands = Hackuble.loadService.Load(ms);
                        foreach (var loadedCommand in loadedCommands)
                        {
                            Hackuble.commandService.AddCommand(loadedCommand);
                        }
                    }
                }
            }
        }

        void ClearExistingCommands()
        {
            Hackuble.commandService.Commands.Clear();
            //StateHasChanged();
        }

        DataAccess PresentCommandUserInterface(DataAccess dataAccess)
        {
            //System.Diagnostics.Trace.WriteLine($"Number of inputs: {dataAccess.Arguments.Count}");

            //foreach (var argument in dataAccess.Arguments)
            //{

            //}
            //LaunchCommandRunner();
            if (currentCommand != null)
            {
                ArgumentsDialog a = new ArgumentsDialog(currentCommand);
                if (a.ShowDialog() == DialogResult.OK)
                {

                }
            }
            //foreach (var argument in command.Arguments)
            //{
            //    string name = argument.Prompt;
            //    string description = argument.Prompt;



            //}


            return null;
        }

        public void ProceedRunCommand()
        {
            var command = currentCommand;
            var dataAccess = currentDataAccess;

            Hackuble.commandService.RunCommand(command, Hackuble.CurrentContext, dataAccess);

            //System.Diagnostics.Trace.WriteLine($"Running command {command.Name}");
            //CloseCommandModal();

            currentCommand = null;
            currentDataAccess = null;
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (!viewportOpen)
            {
                Form childForm = new ThreeD_Viewport();
                childForm.MdiParent = this;
                childForm.Show();
                viewportOpen = true;
            }
        }

        private void manageCommandsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearExistingCommands();
        }
    }
}
