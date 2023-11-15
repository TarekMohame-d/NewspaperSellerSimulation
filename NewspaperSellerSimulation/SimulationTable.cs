using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewspaperSellerSimulation
{
    public partial class SimulationTable : Form
    {
        public SimulationTable()
        {
            InitializeComponent();
        }

        private void SimulationTable_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void back_button_Click(object sender, EventArgs e)
        {
            this.Hide();
            new ShowDataForm().Show();
        }
    }
}
