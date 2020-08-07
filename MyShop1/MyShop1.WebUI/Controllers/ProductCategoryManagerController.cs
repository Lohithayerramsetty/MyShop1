using MyShop.Core.Models;
using MyShop1.Core.Contracts;
using MyShop1.DataAccess.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop1.WebUI.Controllers
{

    public class ProductcategoryManagerController : Controller
    {
        IRepository<Productcategory> Context;


        public ProductcategoryManagerController(IRepository<Productcategory>context)
        {
            this.Context =context;

        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Productcategory> productcategories = Context.Collection().ToList();
            return View(productcategories);
        }
        public ActionResult Create()
        {
            Productcategory productcategory = new Productcategory();
            return View(productcategory);
        }
        [HttpPost]
        public ActionResult Create(Productcategory productcategory)
        {
            if (!ModelState.IsValid)
            {
                return View(productcategory)
                }
            else
            {
                Context.Insert(productcategory);
                Context.Commit();
                return RedirectToAction("Index");
            }
        }


        public ActionResult Edit(string Id)
        {
            Productcategory productcategory = Context.Find(Id);
            if (productcategory == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategory);
            }
        }

        [HttpPost]
        public ActionResult Edit(Productcategory product, string Id, HttpPostedFileBase file)
        {
            Productcategory productcategoryToEdit = Context.Find(Id);

            if (productcategoryToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }

                productcategoryToEdit.category = product.category;

                Context.Commit();

                return RedirectToAction("Index");
            }
        }

        public ActionResult Delete(string Id)
        {
            Productcategory productcategoryToDelete = Context.Find(Id);

            if (productcategoryToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productcategoryToDelete);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Productcategory productcategoryToDelete = Context.Find(Id);

            if (productcategoryToDelete == null)
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