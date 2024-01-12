using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.Product;
using SystemStock.Business.Model.Store;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Model.StoreProduct
{
    public class StoreProductModel
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public ProductModel Product { get; set; }
        public long StoreId { get; set; }
        public StoreModel Store { get; set; }
        public long Amount { get; set; }
    }
}
