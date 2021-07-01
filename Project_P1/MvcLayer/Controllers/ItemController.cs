using BusinessLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelsLibrary;
using ModelsLibrary.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace MvcLayer.Controllers
{
    public class ItemController : Controller
    {
        private readonly ILogger<ItemController> _logger;

        private readonly IItemLogic _itemLogic;
        //private readonly IItemLogic _cart;
        //create a constructor into which i'll inject the business layer.
        public ItemController(IItemLogic itemLogic)
        {
            this._itemLogic = itemLogic;
            //this._logger = logger;
        }
        // GET: ItemController
        public ActionResult Index()
        {
            return View();
            
        }
        // From Locations View, To Items View
        // Initializes Cart
        public ActionResult CreateCart(IDictionary<String, String> parms)
        {
            parms.TryGetValue("CustomerId", out string customerIdStr);
            parms.TryGetValue("StoreId", out string storeIdStr);
            int customerId = Int32.Parse(customerIdStr);
            int storeId = Int32.Parse(storeIdStr);
            var cart = new CartViewModel()
            {
                CustomerId = customerId,
                StoreId = storeId,
                Items = new List<ItemViewModel>(),
            };
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Items", new { id = storeId });
        }
        // From CreateCart or Cart, To AddtoCart
        public async Task<ActionResult> Items(int id)
        {
            List<ItemViewModel> items = await _itemLogic.QueryItemView(id);
            return View(items);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Items(ItemViewModel model)//model binding system takes the dat from the form and matches it to the props of the model.
        {
            
            //check that the model binding worked.
            if (!ModelState.IsValid)
            {
                RedirectToAction("Items");
            }
            
            return View("AddToCart", model);
        }
        public ActionResult AddToCart(IDictionary<String, String> parms)
        {
            parms.TryGetValue("ProductId", out string id);
            parms.TryGetValue("Stock", out string stock);
            return View(_itemLogic.GetItemViewModelFromProductId(Int32.Parse(id),Int32.Parse(stock)));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddToCart(ItemViewModel model)//model binding system takes the dat from the form and matches it to the props of the model.
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else if(model.OrderQuantity>model.Stock)
            {
                //ViewBag.ErrorText = "Hey guy, there was an error!";
                ModelState.AddModelError(nameof(ItemViewModel.OrderQuantity), "This store does not have that amount of items");
                return View(model);
            }
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(HttpContext.Session.GetString("CartSession"));
            cart = _itemLogic.AddItemToCart(cart, model);
            HttpContext.Session.SetString("CartSession", JsonConvert.SerializeObject(cart));
            return RedirectToAction("Cart");
        }
        public ActionResult Cart()
        {
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(HttpContext.Session.GetString("CartSession"));
            ViewData["storeId"] = cart.StoreId;
            ViewData["customerId"] = cart.CustomerId;
            return View(cart.Items);
        }
        public ActionResult VerifyCart()
        {
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(HttpContext.Session.GetString("CartSession"));
            return View(cart.Items);
        }
        public async Task<ActionResult> CreateNewOrder()
        {
            //if (!ModelState.IsValid)
            //{
            //    RedirectToAction("SignUp");
            //}
            CartViewModel cart = JsonConvert.DeserializeObject<CartViewModel>(HttpContext.Session.GetString("CartSession"));
            bool myBool = await _itemLogic.CreateOrderAsync(cart);

            if (myBool)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorText = "Hey guy, there was an error!";
                return View("Error");
            }
            //_itemLogic.CreateCustomer(model);
            return View("Verify"/*, model*/);
        }




    }
}
