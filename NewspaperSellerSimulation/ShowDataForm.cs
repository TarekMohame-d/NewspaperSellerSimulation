using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NewspaperSellerModels;
using NewspaperSellerTesting;

namespace NewspaperSellerSimulation
{
    public partial class ShowDataForm : Form
    {
        public ShowDataForm()
        {
            InitializeComponent();
        }

        private void ShowDataForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void load_data_button_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ExtractData.extractFileData(openFileDialog.FileName);
                    ExtractData.fileName = openFileDialog.FileName.Split('\\').Last(); ;
                }
            }
            if (ExtractData.dataModel != null)
            {
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                // fill textboxs
                NumOfNewspapers_textBox.Text = ExtractData.dataModel.numOfNewspapers.ToString();
                NumOfRecords_textBox.Text = ExtractData.dataModel.numOfRecords.ToString();
                ScrapPrice_textBox.Text = ExtractData.dataModel.scrapPrice.ToString();
                PurchasePrice_textBox.Text = ExtractData.dataModel.purchasePrice.ToString();
                SellingPrice_textBox.Text = ExtractData.dataModel.sellingPrice.ToString();
                
                // fill datagridview
                Object[] row1 = { "Good", ExtractData.dataModel.typeOfDay.goodProbability };
                Object[] row2 = { "Fair", ExtractData.dataModel.typeOfDay.fairProbability };
                Object[] row3 = { "Poor", ExtractData.dataModel.typeOfDay.poorProbability };
                dataGridView1.Rows.Add(row1);
                dataGridView1.Rows.Add(row2);
                dataGridView1.Rows.Add(row3);
                for (int i = 0; i < ExtractData.dataModel.demandDistributionsData.Count; i++)
                {
                    int demand = ExtractData.dataModel.demandDistributionsData[i].demand;
                    decimal good = ExtractData.dataModel.demandDistributionsData[i].goodProbability;
                    decimal fair = ExtractData.dataModel.demandDistributionsData[i].fairProbability;
                    decimal poor = ExtractData.dataModel.demandDistributionsData[i].poorProbability;
                    dataGridView2.Rows.Add(createRows(demand, good, fair, poor));
                }
            }
        }

        private Object[] createRows(int demand, decimal good, decimal fair, decimal poor)
        {
            Object[] row = { demand, good, fair, poor };
            return row;
        }

        private void continue_button_Click(object sender, EventArgs e)
        {
            if (NumOfNewspapers_textBox.Text != "")
            {
                this.Hide();
                new SimulationTable().Show();
            }
            else
            {
                MessageBox.Show("Please load a file first...", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
