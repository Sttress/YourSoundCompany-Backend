using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.StoreProduct;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Map
{
    public class StoreProductMap:Profile
    {
        public StoreProductMap() 
        {
            CreateMap<StoreProductEntity, StoreProductModel>().ReverseMap();
        }
    }
}
