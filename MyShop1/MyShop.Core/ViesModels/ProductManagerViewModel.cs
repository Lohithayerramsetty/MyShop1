using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViesModels
{
    public class ProductManagerViewModel
    {
        public Product product { get; set; }
        public IEnumerable<Productcategory> Productcategories { get; set; }
            

    }
}
