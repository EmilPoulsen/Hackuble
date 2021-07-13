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
    public partial class ThreeD_Viewport : Form
    {
        public ThreeD_Viewport()
        {
            InitializeComponent();
        }

        private void ThreeD_Viewport_Load(object sender, EventArgs e)
        {
            chromiumWebBrowser1.Load("https://dev.arpmdesignandresearch.com");
        }
    }
}
