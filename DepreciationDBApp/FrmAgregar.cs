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
    public partial class FrmAgregar : Form
    {
        public IAssetService assetService { get; set; }
        public int Id { get; set; }
        public string Code { get; set; }
        public FrmAgregar()
        {
            InitializeComponent();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
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

            Dispose();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Asset asset = assetService.FindById(Id);

            asset.Name = txtNombre.Text;
            asset.Description = txtDescripcion.Text;
            asset.Terms = (int)nudTerms.Value;
            asset.Amount = nudValor.Value;
            asset.AmountResidual = nudValorResidual.Value;
            asset.Status = cmbEstado.Text;
            asset.Code = Code;
            asset.IsActive = true;

            assetService.Update(asset);

            Dispose();
        }

        private void FrmAgregar_Load(object sender, EventArgs e)
        {

        }
    }
}
