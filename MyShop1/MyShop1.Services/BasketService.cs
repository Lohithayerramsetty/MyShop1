using MyShop.Core.Models;
using MyShop.Core.ViesModels;
using MyShop1.Core.Contracts;
using MyShop1.Services;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace MyShop1.Services
{
    public class BasketService:IBasketService
    {
        IRepository<Product> productcontext;
        IRepository<Basket> basketcontext;
        public const string BasketSessionName = "eCommerceBasket";


        public BasketService(IRepository<Product> Productcontext, IRepository<Basket> Basketcontext)
        {
            this.basketcontext = Basketcontext;
            this.productcontext = Productcontext;
        }
        private Basket Getbasket(HttpContextBase httpContext, bool CreateIfNull)
        {
            HttpCookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (Cookie != null)
            {
                string BasketID = cookie.Value;
                if (!string.IsNullOrEmpty(basketID))
                {
                    basket = basketcontext.Find(basketID);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);

                    }

                }
            }
            else
            {
                if (CreateIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }
        private Basket CreateNewBasket(HttpContextBase httpContext)
            Basket basket = new Basket();
        basketcontext.Insert(basket);
            basketcontext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName);
        cookie.Value=Basket.Id;
            cookie.Expires=DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;

    }

    public void AddToBasket(HttpContextBase httpContext, string productId)
    {
        Basket basket = GetBasket(httpContext, true);
        BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

        if (item == null)
        {
            item = new BasketItem()
            {
                BasketId = basket.Id;
            productId = Product.Id;
            Quantity = 1
            };

        basket.BasketItems.Add(item);
         }
        else
        {
          item.Quantity=item.Quantity+1;
    
        } 
        basketcontext.Commit();
    }
    public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
    Basket basket = GetBasket(HttpContext, true);
    BasketItem item = BasketService.BasketItems.FirstOrDefault(if=>i.Id == itemsId);

    if(item!=null)
    {
    basket.BasketItems.Remove(item);
    basketcontext.Commit();
    }

  }
    public List<BasketItemViewModel>GetBasketItems(HttpContextBase httpContext)
    { 
    Basket basket=GetBasket(httpContext,false)

        if (basket != null)
        {
        var results = (from b in basket.BasketItems
                       join p in productcontext.Collection() on b.ProductId equals p.Id
                       select new BasketItemViewModel()
                       {
                           Id = b.Id,
                           Quantity = b.Quantity,
                           ProductName = p.Name,
                           Image = p.Image,
                           Price = p.Price

                       }
                       ).ToList();
        return results;
        }else
        {
        return new List<BasketItemViewModel>();
        }
          
        
    }
     public BasketSummaryViewModel GetBasketSummaryView(HttpContextBase httpContext)
     {

         Basket basket = GetBasket(httpContext, false);
         BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);
         if(basket !=null)
         {
        int? basketCount = (from item in basket.BasketItems
                            select item.Quantity).Sum();
        decimal? basketTotal = (from item in basket.BasketItems
                                join p in productcontext.Collection() on item.ProductId equals p.Id
                                select item.Quantity * p.Price).Sum();

        model.BasketCount = basketCount ?? 0;
        model.BasketTotal = basketTotal ?? decimal.Zero;
        return model;
         }
         else 
         {
        return model;
         }
     }
  }
}
