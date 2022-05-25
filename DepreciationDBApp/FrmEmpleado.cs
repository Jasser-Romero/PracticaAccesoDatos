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
    public partial class FrmEmpleado : Form
    {
        public IAssetService assetService { get; set; }
        public IEmployeeServices employeeService { get; set; }
        public FrmEmpleado()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FrmEmpleado_Load(object sender, EventArgs e)
        {
            dgvAsset.DataSource = null;
            dgvAsset.DataSource = employeeService.GetAll();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Employee employee = new()
            {
                Names = txtNombre.Text,
                Lastnames = txtApellidos.Text,
                Dni = txtDni.Text,
                Address = txtAdress.Text,
                Email = txtEmail.Text,
                Phone = txtTelefono.Text,
                Status = cmbEstado.Text
            };
            employeeService.Create(employee);

            ClearTexts();
            LoadDatagridView();
        }

        private void LoadDatagridView()
        {
            dgvAsset.DataSource = null;
            dgvAsset.DataSource = employeeService.GetAll();
        }

        private void ClearTexts()
        {
            txtAdress.Clear();
            txtApellidos.Clear();
            txtBusqueda.Clear();
            txtDni.Clear();
            txtEmail.Clear();
            txtNombre.Clear();
            txtTelefono.Clear();
            cmbEstado.SelectedIndex = -1;
        }
        private void btnSet_Click(object sender, EventArgs e)
        {
            string dni = dgvAsset.CurrentRow.Cells[6].Value.ToString();
            Employee employee = employeeService.FindByDni(dni);
            if (employee.Status.Equals("No activo"))
            {
                MessageBox.Show("Este empleado no esta activo");
                return;
            }

            FrmAsignar frmAsignar = new FrmAsignar();
            frmAsignar.assetService = assetService;
            frmAsignar.employeeService = employeeService;
            frmAsignar.Dni = dni;
            frmAsignar.button2.Visible = false;

            frmAsignar.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //string dni = dgvAsset.CurrentRow.Cells[6].Value.ToString();
            //Employee employee = employeeService.FindByDni(dni);

            //employeeService.Delete(employee);
            //LoadDatagridView();
        }

        private void txtBusqueda_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
                {
                    LoadDatagridView();
                    return;
                }

                switch (comboBox1.SelectedIndex)
                {
                    case 0:
                        Employee employee = employeeService.FindByDni(txtBusqueda.Text);
                        if(employee == null)
                        {
                            MessageBox.Show("No se encontro al empleado");
                            return;
                        }
                        List<Employee> list = new() { employee };
                        dgvAsset.DataSource = null;
                        dgvAsset.DataSource = list;
                        break;
                    case 1:
                        Employee employee2 = employeeService.FindByEmail(txtBusqueda.Text);
                        if (employee2 == null)
                        {
                            MessageBox.Show("No se encontro al empleado");
                            return;
                        }
                        List<Employee> list2 = new() { employee2 };
                        dgvAsset.DataSource = null;
                        dgvAsset.DataSource = list2;
                        break;
                    case 2:
                        List<Employee> employees = employeeService.FindByLastnames(txtBusqueda.Text).ToList();
                        dgvAsset.DataSource = null;
                        dgvAsset.DataSource = employees;
                        break;
                }
            }
        }

        private void btnUnset_Click(object sender, EventArgs e)
        {
            string dni = dgvAsset.CurrentRow.Cells[6].Value.ToString();
            FrmAsignar frmAsignar = new FrmAsignar();
            frmAsignar.employeeService = employeeService;
            frmAsignar.assetService = assetService;
            frmAsignar.Dni = dni;
            frmAsignar.button1.Visible = false;
            frmAsignar.dateTimePicker1.Visible = false;

            frmAsignar.ShowDialog();
        }
    }
}
