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

namespace PruebaOmnicon.Views
{
    public partial class New : Form
    {
        public int? productId;
        public PRODUCT objProduct = null;

        public New(int? productId = null)
        {
            InitializeComponent();

            this.productId = productId;

            if (productId != null)
            {
                lblTitle.Text = "Editar producto";
                LoadData();
            }
        }


        /**
         * LoadData
         * 
         * Creado por: Carlos Caicedo
         * Desc: Carga los datos del producto
         * 
         */
        private void LoadData()
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                objProduct = dbEntities.PRODUCT.Find(productId);

                txtName.Text = objProduct.PRODUCT_NAME.ToString();
                txtQuantity.Text = objProduct.QUANTITY.ToString();
                dtModifiedDate.Value = (DateTime)objProduct.MODIFIED_DATE;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                if (productId == null)
                    objProduct = new PRODUCT();

                objProduct.PRODUCT_NAME = txtName.Text;
                objProduct.QUANTITY = int.Parse(txtQuantity.Text);
                objProduct.MODIFIED_DATE = dtModifiedDate.Value;

                if (productId != null)
                {
                    dbEntities.Entry(objProduct).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    dbEntities.PRODUCT.Add(objProduct);
                }

                dbEntities.SaveChanges();

                this.Close();
            }
        }

    }
}
