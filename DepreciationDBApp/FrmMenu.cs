using DepreciationDBApp.Applications.Interfaces;
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
    public partial class FrmMenu : Form
    {
        private IAssetService assetService;
        private IEmployeeServices employeeServices;
        private IAssetEmployeeServices assetEmployeeServices;
        public FrmMenu(IAssetService assetService, IEmployeeServices employeeServices, IAssetEmployeeServices assetEmployeeServices)
        {
            this.employeeServices = employeeServices;
            this.assetService = assetService;
            this.assetEmployeeServices = assetEmployeeServices;
            InitializeComponent();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AbrirFormHija(object formHija)
        {
            if (this.pnlContenedor.Controls.Count > 0)
            {
                this.pnlContenedor.Controls.RemoveAt(0);
            }
            Form fh = formHija as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.pnlContenedor.Controls.Add(fh);
            this.pnlContenedor.Tag = fh;
            fh.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new Form1(assetService, employeeServices)
            {

            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormHija(new FrmEmpleado()
            {
                employeeService = employeeServices,
                assetService = assetService
            });
        }

        private void button3_Click(object sender, EventArgs e)
        {

            AbrirFormHija(new FrmHistorial(assetEmployeeServices)
            {
                employeeService = employeeServices,
                assetService = assetService
            });
        }

        private void FrmMenu_Load(object sender, EventArgs e)
        {

        }
    }
}
