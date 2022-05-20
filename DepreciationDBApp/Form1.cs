using DepreciationDBApp.Applications.Interfaces;
using DepreciationDBApp.Domain.Entities;
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

namespace DepreciationDBApp.Forms
{
    public partial class Form1 : Form
    {
        private IAssetService assetService;
        private IEmployeeServices employeeServices;
        public IAssetEmployeeServices assetEmployeeServices { get; set; }
        public Form1(IAssetService assetService, IEmployeeServices employeeServices)
        {
            this.employeeServices = employeeServices;
            this.assetService = assetService;
            InitializeComponent();
        }

        private void btnAddAsset_Click(object sender, EventArgs e)
        {
            //FrmAgregar frmAgregar = new FrmAgregar();
            //frmAgregar.btnActualizar.Enabled = false;
            //frmAgregar.assetService = assetService;
            //frmAgregar.ShowDialog();

            Asset asset = new Asset()
            {
                Name = txtNombre.Text,
                Description = txtDescripcion.Text,
                Amount = nudValor.Value,
                AmountResidual = nudValorResidual.Value,
                Code = Guid.NewGuid().ToString(),
                IsActive = true,
                Status = cmbEstado.Text,
                Terms = (int)nudTerms.Value
            };

            assetService.Create(asset);

            ClearTexts();
            LoadDataGridView();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            dgvAsset.DataSource = assetService.GetAll();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                    {
                        LoadDataGridView();
                        return;
                    }

                    switch (comboBox1.SelectedIndex)
                    {
                        case 0:
                            if (!int.TryParse(txtBusqueda.Text, out int id))
                            {
                                MessageBox.Show("No escribio un numero", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            Asset asset = assetService.FindById(id);
                            List<Asset> list = new() { asset };
                            dgvAsset.DataSource = null;
                            dgvAsset.DataSource = list;
                            txtBusqueda.Clear();
                            break;
                        case 1:
                            dgvAsset.DataSource = null;
                            dgvAsset.DataSource = assetService.FindByName(txtBusqueda.Text);
                            txtBusqueda.Clear();
                            break;
                        case 2:
                            Asset asset2 = assetService.FindByCode(txtBusqueda.Text);
                            List<Asset> list2 = new() { asset2 };
                            dgvAsset.DataSource = null;
                            dgvAsset.DataSource = list2;
                            txtBusqueda.Clear();
                            break;
                        default:
                            MessageBox.Show("Error");
                            txtBusqueda.Clear();
                            break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ha ocurrido un error","Error");
                return;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            //DataTable dt = FillDataTable();
            
            
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int id = (int)dgvAsset.CurrentRow.Cells[0].Value;

            Asset asset = assetService.FindById(id);
            assetService.Delete(asset);

            dgvAsset.DataSource=null;
            LoadDataGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = (int)dgvAsset.CurrentRow.Cells[0].Value;

            FrmAgregar frmAgregar = new FrmAgregar();
            frmAgregar.assetService = assetService;

            frmAgregar.Id = id;
            frmAgregar.txtNombre.Text = dgvAsset.CurrentRow.Cells[1].Value.ToString();
            frmAgregar.txtDescripcion.Text = dgvAsset.CurrentRow.Cells[2].Value.ToString();
            frmAgregar.nudValor.Value = (decimal)dgvAsset.CurrentRow.Cells[3].Value;
            frmAgregar.nudValorResidual.Value = (decimal)dgvAsset.CurrentRow.Cells[4].Value;
            frmAgregar.nudTerms.Value = (int)dgvAsset.CurrentRow.Cells[5].Value;
            frmAgregar.Code = dgvAsset.CurrentRow.Cells[6].Value.ToString();
            frmAgregar.cmbEstado.SelectedItem = dgvAsset.CurrentRow.Cells[7].Value;

            frmAgregar.btnAgregar.Enabled = false;
            frmAgregar.ShowDialog();

            LoadDataGridView();
        }
        
        private void ClearTexts()
        {
            txtNombre.Clear();
            txtDescripcion.Clear();
            nudValor.Value = 0.00M;
            nudValorResidual.Value = 0.00M;
            nudTerms.Value = 0;
            cmbEstado.SelectedIndex = -1;
        }
        private DataTable FillDataTable()
        {
            DataTable dt = new DataTable();
            foreach(DataGridViewColumn column in dgvAsset.Columns)
            {
                dt.Columns.Add(column.Name);
            }
            foreach(DataGridViewRow row in dgvAsset.Rows)
            {
                DataRow dr = dt.NewRow();
                foreach(DataGridViewCell cell in row.Cells)
                {
                    dr[cell.ColumnIndex] = cell.Value;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
