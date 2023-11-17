using NewspaperSellerModels;
using NewspaperSellerTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
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

        private void SimulationTable_Load(object sender, EventArgs e)
        {
            Calculations.fillNewsdayTable();
            Calculations.fillDemandTable();
            Calculations.fillSystemTable();
            decimal totalSales = 0;
            decimal totalLost = 0;
            decimal totalScrap = 0;
            decimal totalNet = 0;
            if (Calculations.systemTable.SimulationTable != null)
            {
                dataGridView1.Rows.Clear();
                for (int i = 0; i < Calculations.systemTable.SimulationTable.Count; i++)
                {
                    totalSales += Calculations.systemTable.SimulationTable[i].SalesProfit;
                    totalLost += Calculations.systemTable.SimulationTable[i].LostProfit;
                    totalScrap += Calculations.systemTable.SimulationTable[i].ScrapProfit;
                    totalNet += Calculations.systemTable.SimulationTable[i].DailyNetProfit;
                    dataGridView1.Rows.Add(createRows(Calculations.systemTable.SimulationTable[i]));
                }
                Object[] lastRow = { "Total", "", "", "", "", totalSales, totalLost, totalScrap, totalNet };
                dataGridView1.Rows.Add(lastRow);
                Calculations.systemTable.PerformanceMeasures.TotalCost = (decimal)(ExtractData.dataModel.numOfNewspapers * 33) / 100 * ExtractData.dataModel.numOfRecords;
                Calculations.systemTable.PerformanceMeasures.TotalSalesProfit = totalSales;
                Calculations.systemTable.PerformanceMeasures.TotalLostProfit = totalLost;
                Calculations.systemTable.PerformanceMeasures.TotalScrapProfit = totalScrap;
                Calculations.systemTable.PerformanceMeasures.TotalNetProfit = totalNet;
            }
        }

        private Object[] createRows(SimulationCase rowData)
        {
            Object[] row = { rowData.DayNo, rowData.RandomNewsDayType, rowData.NewsDayType,
                             rowData.RandomDemand, rowData.Demand, rowData.SalesProfit,
                             rowData.LostProfit, rowData.ScrapProfit,rowData.DailyNetProfit};
            return row;
        }

        private void test_button_Click(object sender, EventArgs e)
        {
            string testingResult = TestingManager.Test(Calculations.systemTable, Constants.FileNames.TestCase3);
            MessageBox.Show(testingResult);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
