using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DepreciationDBApp.Forms
{
    public partial class FrmAsignar : Form
    {
        public IAssetService assetService { get; set; }
        public IEmployeeServices employeeService { get; set; }
        public string Dni { get; set; }
        public FrmAsignar()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmAsignar_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = assetService.GetAll();
            dataGridView1.MultiSelect = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int contadorFilas = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            Employee employee = employeeService.FindByDni(Dni);

            List<int> assetsIds = new List<int>();
            List<Asset> assets = new List<Asset>();

            DateTime effectiveDate = dateTimePicker1.Value;

            if(contadorFilas > 1)
            {
                DataTable dt = new DataTable();
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                    dt.Columns.Add(column.Name, column.CellType);
                for (int i = 0; i < dataGridView1.SelectedRows.Count; i++)
                {
                    dt.Rows.Add();
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        //dt.Rows[i][j] = dataGridView1.SelectedRows[i].Cells[j].Value;
                        
                        //^^^^^^^^^^^
                    }
                    assetsIds.Add((int)dataGridView1.SelectedRows[i].Cells[0].Value);
                }
                foreach(int id in assetsIds)
                {
                    Asset asset = assetService.FindById(id);
                    if (asset.Status.Equals("Asignado"))
                    {
                        MessageBox.Show("El activo esta actualmente asignado");
                        return;
                    }
                    else
                    {
                        asset.Status = "Asignado";
                        assetService.Update(asset);
                        assets.Add(asset);
                        
                    }
                    
                }
                employeeService.SetAssetsToEmployee(employee, assets, effectiveDate);

                Dispose();
            }
            else
            {
                int assetId = (int)dataGridView1.CurrentRow.Cells[0].Value;
                Asset asset = assetService.FindById(assetId);
                if (asset.Status.Equals("Asignado"))
                {
                    MessageBox.Show("El activo esta actualmente asignado");
                    return;
                }
                else
                {
                    asset.Status = "Asignado";
                    assetService.Update(asset);
                }
                employeeService.SetAssetToEmployee(employee, asset, effectiveDate);
            }

            Dispose();
        }
    }
}
