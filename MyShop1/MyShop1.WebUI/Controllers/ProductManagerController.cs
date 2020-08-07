using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using MyShop.Core.Models;
using MyShop.Core.ViesModels;
using MyShop1.DataAccess.InMemory;

namespace MyShop1.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> Context;
        InMemoryRepository<productcategory> productcategories;

    public ProductManagerController()
        {
            Context = new InMemoryRepository<Product>();
            productcategories = new ProductcategoryRepository();

        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = Context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.Productcategories = productcategories.Collection();
            return View(viewModel);


        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product)
            }
            else
            {
                Context.Insert(product);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }

        
        public ActionResult Edit(string Id)
        {
            Product product = Context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.Productcategories = productcategories.Collection();

                return View(viewModel);
            }
        }

        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file)
        {
            Product productToEdit = Context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = Context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = Context.Find(Id);

            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                Context.Delete(Id);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}