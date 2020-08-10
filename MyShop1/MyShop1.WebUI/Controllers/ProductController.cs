using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop1.WebUI.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }

    public ActionResult AddProductToShoppingCart(AddProductToCartRequest request)
    {
        if (request != null)
        {
            if (Session["cart"] == null)
            {
                Session["cart"] = new Dictionary<int, int>();
            }

            var cart = (Dictionary<int, int>)Session["cart"];

            if (cart.ContainsKey(request.ProductId))
            {
                cart[request.ProductId] = cart[request.ProductId] + request.Quantity;
            }
            else
            {
                cart.Add(request.ProductId, request.Quantity);
            }
        }

        return RedirectToAction("ProductList", "Product");
    }
    foreach (var item in allProductTypes) 
{
    if (selectedProductTypes.Contains(item))
    {
        checkBoxForType.Add(new CheckBoxForProductType() { Name = item, IsSelected = true });
    }
    else
    {
        checkBoxForType.Add(new CheckBoxForProductType() { Name = item, IsSelected = false });
    }
     for (int i = 0; i<selectedProductTypes.Count; i++) 
{
    string proType = selectedProductTypes[i];
var pros = db.Products.Where(p => p.ProductType == proType);
    if (pros != null)
    {
        products.AddRange(pros);
    }
}

}
}



