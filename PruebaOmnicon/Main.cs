using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PruebaOmnicon.Models;

namespace PruebaOmnicon
{
    public partial class Main : Form
    {
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
        private void Refresh()
        {
            using (DBEntities db = new DBEntities())
            {
                var lstProducts = from d in db.PRODUCT
                                  select d;
                dgProducts.DataSource = lstProducts.ToList();
            }
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
                using (DBEntities dbEntities = new DBEntities())
                {
                    PRODUCT objProduct = dbEntities.PRODUCT.Find(productId);
                    dbEntities.PRODUCT.Remove(objProduct);

                    dbEntities.SaveChanges();
                }

                // Refresca la tabla
                Refresh();
            }
        }

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

    }
}
