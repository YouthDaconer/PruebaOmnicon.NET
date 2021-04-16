using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PruebaOmnicon
{
    public partial class Main : Form
    {
        Controllers.ProductController productController = new Controllers.ProductController();

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Refresh();
        }

        /**
         * Refresh
         * 
         * Creado por: Carlos Caicedo
         * Desc: Refresca el listado de productos en la tabla
         * 
         */
        private void Refresh(string productName = null, int? quantity = null, DateTime? modifiedDate = null)
        {
            dgProducts.DataSource = productController.GetList(productName, quantity, modifiedDate);
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            // Instancia el formulario
            Views.New frmNew = new Views.New();
            frmNew.ShowDialog();

            // Refresca la tabla
            Refresh();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            int? productId = GetId();

            if (productId != null)
            {
                Views.New frmNew = new Views.New(productId);
                frmNew.ShowDialog();

                // Refresca la tabla
                Refresh();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int? productId = GetId();

            if (productId != null)
            {
                var confirmResult = MessageBox.Show("¿Está seguro que desea eliminar el producto?",
                    "Confirmar",
                    MessageBoxButtons.YesNo);

                if (confirmResult == DialogResult.Yes)
                {
                    // Elimina el producto
                    productController.Remove(productId);

                    // Refresca la tabla
                    Refresh();
                }
            }
        }

        /**
         * GetId
         * 
         * Creado por: Carlos Caicedo
         * Desc: Obtiene el id del producto seleccionado en la lista
         * 
         */
        private int? GetId()
        {
            try
            {
                return int.Parse(dgProducts.Rows[dgProducts.CurrentRow.Index].Cells[0].Value.ToString());
            }
            catch (Exception)
            {
                return null;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string productName = null;
            int? quantity = null;
            DateTime? modifiedDate = null;

            if (!txtProductName.Text.Trim().Equals(""))
            {
                productName = txtProductName.Text;
            }

            if (!txtQuantity.Text.Trim().Equals(""))
            {
                quantity = int.Parse(txtQuantity.Text);
            }

            if (chkSearchDate.Checked)
            {
                modifiedDate = dpModifiedDate.Value;
            }

            Refresh(productName, quantity, modifiedDate);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Refresh();
            txtProductName.Text = "";
            txtQuantity.Text = "";
            dpModifiedDate.Enabled = false;
            chkSearchDate.Checked = false;
        }

        private void chkSearchDate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSearchDate.Checked)
            {
                dpModifiedDate.Enabled = true;
            }
            else
            {
                dpModifiedDate.Enabled = false;
            }
        }
    }
}
