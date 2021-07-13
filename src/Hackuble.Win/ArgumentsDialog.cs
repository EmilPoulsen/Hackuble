using Hackuble.Commands;
using ScintillaNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hackuble.Win
{
    public partial class ArgumentsDialog : Form
    {
        public ArgumentsDialog(AbstractCommand command)
        {
            InitializeComponent();
        }

        private void ArgumentsDialog_Load(object sender, EventArgs e)
        {
            // Configure the JSON lexer styles
            scintilla1.Styles[Style.Json.Default].ForeColor = Color.Silver;
            scintilla1.Styles[Style.Json.BlockComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Json.LineComment].ForeColor = Color.FromArgb(0, 128, 0); // Green
            scintilla1.Styles[Style.Json.Number].ForeColor = Color.Olive;
            scintilla1.Styles[Style.Json.PropertyName].ForeColor = Color.Blue;
            scintilla1.Styles[Style.Json.String].ForeColor = Color.FromArgb(163, 21, 21); // Red
            scintilla1.Styles[Style.Json.StringEol].BackColor = Color.Pink;
            scintilla1.Styles[Style.Json.Operator].ForeColor = Color.Purple;
            scintilla1.Lexer = Lexer.Json;

			InitCodeFolding();

		}

		private void InitCodeFolding()
		{

			scintilla1.SetFoldMarginColor(true, ScriptEditor.IntToColor(0xB7B7B7));
			scintilla1.SetFoldMarginHighlightColor(true, ScriptEditor.IntToColor(0xB7B7B7));

			// Enable code folding
			scintilla1.SetProperty("fold", "1");
			scintilla1.SetProperty("fold.compact", "1");

			// Configure a margin to display folding symbols
			scintilla1.Margins[3].Type = MarginType.Symbol;
			scintilla1.Margins[3].Mask = Marker.MaskFolders;
			scintilla1.Margins[3].Sensitive = true;
			scintilla1.Margins[3].Width = 20;

			// Set colors for all folding markers
			for (int i = 25; i <= 31; i++)
			{
				scintilla1.Markers[i].SetForeColor(ScriptEditor.IntToColor(0xB7B7B7)); // styles for [+] and [-]
				scintilla1.Markers[i].SetBackColor(ScriptEditor.IntToColor(0x2A211C)); // styles for [+] and [-]
			}

			// Configure folding markers with respective symbols
			scintilla1.Markers[Marker.Folder].Symbol = true ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
			scintilla1.Markers[Marker.FolderOpen].Symbol = true ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
			scintilla1.Markers[Marker.FolderEnd].Symbol = true ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
			scintilla1.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
			scintilla1.Markers[Marker.FolderOpenMid].Symbol = true ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
			scintilla1.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
			scintilla1.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

			// Enable automatic folding
			scintilla1.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);

		}
	}
}
