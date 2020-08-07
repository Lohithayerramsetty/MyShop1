using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop1.DataAccess.InMemory
{
    public class ProductcategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Productcategory> productcategories = new List<Productcategory>();

        public ProductcategoryRepository()
        {
            productcategories = cache["productscategories"] as List<Productcategory>;
            if (productcategories == null)
            {
                productcategories = new List<Productcategory>();
            }

        }
        public void Commit()
        {
            cache["productscategories"] = productcategories;

        }
        public void Insert(Product p)
        {
            productcategories.Add(p);
        }
        public void Update(Productcategory productcategory)
        {
            Productcategory productcategoryToUpdate = productcategories.Find(p => p.Id == productcategory.Id);
            if (productcategoryToUpdate != null)
            {
                productcategoryToUpdate = productcategory;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }

        public Productcategory Find(string Id)
        {
            Productcategory productcategory = productcategories.Find(p => p.Id == Id);

            if (productcategory != null)
            {
                return productcategory;

            }
            else
            {
                throw new Exception("Product not found");
            }
        }
        public IQueryable<Productcategory> Collection()
        {
            return productcategories.AsQueryable();
        }
        public void Delete(string Id)
        {
            Productcategory productcategoryToDelete = productcategories.Find(p => p.Id == Id);
            if (productcategoryToDelete != null)
            {
                productcategories.Remove(productcategoryToDelete);
            }
            else
            {
                throw new Exception("Product not found");
            }
        }

    }

}
