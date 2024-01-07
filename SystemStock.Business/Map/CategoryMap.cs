using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.Business.Model.Category;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Map
{
    public class CategoryMap : Profile
    {
        public CategoryMap() 
        {
            CreateMap<CategoryEntity, CategoryModel>().ReverseMap();
        }
    }
}
