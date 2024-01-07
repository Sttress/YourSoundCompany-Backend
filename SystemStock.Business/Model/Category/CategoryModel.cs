using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStock.RelationalData.Entities;

namespace SystemStock.Business.Model.Category
{
    public class CategoryModel
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public long? UserId { get; set; }
        public bool Active { get; set; }

    }
}
