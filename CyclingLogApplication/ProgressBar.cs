using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyclingLogApplication
{
    public partial class ProgressBar : Form
    {
        public ProgressBar()
        {
            InitializeComponent();
            
        }

        private void ProgressBar_Load(object sender, EventArgs e)
        {
            Action action = () => label1.Text = "MyText";
            label1.Invoke(action);
        }
    }
}
