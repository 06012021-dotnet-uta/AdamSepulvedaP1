using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModelsLibrary;
using BusinessLayer;
using Microsoft.Extensions.Logging;
using ModelsLibrary.ViewModels;
using Newtonsoft.Json;

namespace MvcLayer.Controllers
{
    public class ShopController : Controller
    {
        private readonly ILogger<ShopController> _logger;

        private readonly IAccountLogic _accountLogic;
        //create a constructor into which i'll inject the business layer.
        public ShopController(IAccountLogic accountLogic, ILogger<ShopController> logger)
        {
            this._accountLogic = accountLogic;
            this._logger = logger;
        }
        // GET: ShopController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return View(id);
        }

        // GET: ShopController/Create
        public ActionResult Create()
        {
            return View();
        }
        
        public async Task<ActionResult> Locations(int id)
        {
            List<Store> locations = await _accountLogic.LocationListAsync();
            ViewData.Add("CustomerId", id);
            return View(locations);
        }
    }
}
