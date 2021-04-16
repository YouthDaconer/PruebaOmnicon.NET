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
        Controllers.ProductController productController = new Controllers.ProductController();

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
            objProduct = productController.GetProduct(productId);

            if (objProduct != null)
            {
                txtName.Text = objProduct.PRODUCT_NAME.ToString();
                txtQuantity.Text = objProduct.QUANTITY.ToString();
                dtModifiedDate.Value = (DateTime)objProduct.MODIFIED_DATE;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (productId != null)
            {
                productController.Edit(
                    productId,
                    txtName.Text,
                    int.Parse(txtQuantity.Text),
                    dtModifiedDate.Value
                    );
            }
            else
            {
                productController.New(
                    txtName.Text,
                    int.Parse(txtQuantity.Text),
                    dtModifiedDate.Value
                    );
            }

            this.Close();
        }

    }
}
