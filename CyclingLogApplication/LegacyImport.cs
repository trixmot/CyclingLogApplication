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
    public partial class LegacyImport : Form
    {
        static int legacyImportIndex = -1;


        public LegacyImport()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            SetLegacyIndexSelection(-1);
            //Close();
            this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }

        public static int GetLegacyIndexSelection()
        {
            return legacyImportIndex;
        }

        public static void SetLegacyIndexSelection(int index)
        {
            legacyImportIndex = index;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            SetLegacyIndexSelection(cbLegacyIndexSelection.SelectedIndex);
            this.Invoke(new MethodInvoker(delegate { this.Close(); }), null);
        }
    }
}
