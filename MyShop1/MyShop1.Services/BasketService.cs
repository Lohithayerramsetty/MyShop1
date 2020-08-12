using MyShop.Core.Models;
using MyShop1.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop1.Services
{
    public class BasketService
    {
        IRepository<Product> productcontext;
        IRepository<Basket> basketcontext;
        public const string BasketSessionName = "eCommerceBasket";


        public BasketService(IRepository<Product>Productcontext,IRepository<Basket> Basketcontext)
        {
            this.basketcontext = Basketcontext;
            this.productcontext = Productcontext;
        }
        private Basket Getbasket(HttpContextBase httpContext,bool CreateIfNull)
        {
            HttpCookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if(Cookie!=null)
            {
                string BasketID = cookie.Value;
                if(!string.IsNullOrEmpty(basketID))
                {
                    basket = basketcontext.Find(basketID);
                }
                else
                {
                    if(createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);

                    }

                }
            }
            else
            {
                if(CreateIfNull)
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

    }
}
