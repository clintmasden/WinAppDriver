using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinAppDriver.GridPerformanceUi
{
    public partial class StartupForm : Form
    {
        public StartupForm()
        {
            InitializeComponent();
        }

        private void StartupForm_Load(object sender, EventArgs e)
        {
            PopulatePerformanceDataGridViewWithDummyData();
        }

        private void PopulatePerformanceDataGridViewWithDummyData()
        {
            PerformanceDataGridView.AutoGenerateColumns = true;
            PerformanceDataGridView.ColumnCount = 3;

            PerformanceDataGridView.Columns[0].Name = "Name";
            PerformanceDataGridView.Columns[0].HeaderText = "Name";

            PerformanceDataGridView.Columns[1].HeaderText = "Age";
            PerformanceDataGridView.Columns[1].Name = "Age";

            PerformanceDataGridView.Columns[2].Name = "Location";
            PerformanceDataGridView.Columns[2].HeaderText = "Location";

            for (int dummyDataIndex = 0; dummyDataIndex < 1000; dummyDataIndex++)
            {
                PerformanceDataGridView.Rows.Add($"Name+{dummyDataIndex}", $"Age+{dummyDataIndex}", $"Location+{dummyDataIndex}");
            }
        }
    }
}