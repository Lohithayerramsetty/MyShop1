using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViesModels
{
   public class BasketItemViewModel
    {
        public string Id { get; set; }
        public int Quantity { get; set; }
        public String ProductName { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
