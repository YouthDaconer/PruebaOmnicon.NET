using Newtonsoft.Json.Linq;
using PruebaOmnicon.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaOmnicon.Controllers
{
    class ProductController
    {

        /**
         * GetList
         * 
         * Creado por: Carlos Caicedo
         * Desc: Obtiene todos los productos
         * 
         */
        public IEnumerable<Models.PRODUCT> GetList(string productName = null, int? quantity = null, DateTime? modifiedDate = null)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                var query = from p in dbEntities.PRODUCT
                            select p;

                if (productName != null)
                {
                    query = query.Where(p => DbFunctions.Like(p.PRODUCT_NAME, "%" + productName + "%"));
                }

                if (quantity != null)
                {
                    query = query.Where(p => p.QUANTITY == quantity);
                }

                if (modifiedDate != null)
                {
                    DateTime dateParam = (DateTime)modifiedDate;
                    query = query.Where(p => EntityFunctions.TruncateTime(p.MODIFIED_DATE) == dateParam.Date);
                }

                return query.ToList();
            }
        }

        /**
         * GetProduct
         * 
         * Creado por: Carlos Caicedo
         * Desc: Obtiene un producto con un id específico
         * 
         */
        public Models.PRODUCT GetProduct(int? productId)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                return dbEntities.PRODUCT.Find(productId);
            }
        }


        /**
         * New
         * 
         * Creado por: Carlos Caicedo
         * Desc: Crea un nuevo producto
         * 
         */
        public Models.PRODUCT New(string productName, int quantity, DateTime modifiedDate)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                PRODUCT objProduct = new PRODUCT();

                objProduct.PRODUCT_NAME = productName;
                objProduct.QUANTITY = quantity;
                objProduct.MODIFIED_DATE = modifiedDate;

                dbEntities.PRODUCT.Add(objProduct);

                dbEntities.SaveChanges();

                return objProduct;
            }
        }

        /**
         * Edit
         * 
         * Creado por: Carlos Caicedo
         * Desc: Edita un producto con un id específico
         * 
         */
        public Models.PRODUCT Edit(int? productId, string productName, int quantity, DateTime modifiedDate)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                PRODUCT objProduct = this.GetProduct(productId);

                // Si existe el producto
                if (objProduct != null)
                {
                    objProduct.PRODUCT_NAME = productName;
                    objProduct.QUANTITY = quantity;
                    objProduct.MODIFIED_DATE = modifiedDate;

                    dbEntities.Entry(objProduct).State = System.Data.Entity.EntityState.Modified;

                    dbEntities.SaveChanges();
                }

                return objProduct;
            }
        }

        /**
         * Remove
         * 
         * Creado por: Carlos Caicedo
         * Desc: Elimina un producto con un id específico
         * 
         */
        public void Remove(int? productId)
        {
            using (DBEntities dbEntities = new DBEntities())
            {
                PRODUCT objProduct = dbEntities.PRODUCT.Find(productId);
                dbEntities.PRODUCT.Remove(objProduct);

                dbEntities.SaveChanges();
            }
        }
    }
}
