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
    public partial class FrmHistorial : Form
    {
        public IAssetService assetService { get; set; }
        public IEmployeeServices employeeService { get; set; }
        private IAssetEmployeeServices assetEmployeeServices;
        public FrmHistorial(IAssetEmployeeServices assetEmployeeServices)
        {
            this.assetEmployeeServices = assetEmployeeServices;
            InitializeComponent();
        }

        private void FrmHistorial_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;

            assetService.GetAll();
            employeeService.GetAll();
            assetEmployeeServices.GetAll();

            dataGridView1.DataSource = assetEmployeeServices.GetAllDTOs();
        }
    }
}
