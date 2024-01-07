using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.Product;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Map
{
    public class ProductMap:Profile
    {
        public ProductMap() 
        {
            CreateMap<ProductModel, ProductEntity>().ReverseMap();
        }

    }
}
