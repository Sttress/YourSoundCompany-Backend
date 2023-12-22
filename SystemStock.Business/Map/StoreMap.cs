using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.Store;
using SystemStock.Business.Model.User;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Map
{
    public class StoreMap : Profile
    {
        public StoreMap()
        {
            CreateMap<StoreEntity, StoreModel>().ReverseMap();
        }
    }
}
