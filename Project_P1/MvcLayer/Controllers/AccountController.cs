using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using BusinessLayer;

namespace MvcLayer.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private  readonly IAccountLogic _accountLogic;
        //create a constructor into which i'll inject the business layer.
        public AccountController(IAccountLogic accountLogic, ILogger<AccountController> logger)
        {
            this._accountLogic = accountLogic;
            this._logger = logger;
        }
        // Home Login Page
        public ActionResult Login()
        {
            return View();
        }
        // Sends Login Data to Locations
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Customer model)
        {
            P1DatabaseContext context = new P1DatabaseContext();
            AccountLogic accountLogic = new AccountLogic();

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Login");
            }
            else if (accountLogic.CheckLogin(model))
            {
                var s = context.Customers.Where(x => x.Username == model.Username).Single();
                return RedirectToAction("Locations", "Shop", new { id = s.CustomerId });
            }
            return View("Login");
        }
        // Views Signup Page
        public ActionResult SignUp()
        {
            return View();
        }
        // Passes model to Verify
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Customer model)//model binding system takes the dat from the form and matches it to the props of the model.
        {
            //check that the model binding worked.
            if (!ModelState.IsValid)
            {
                RedirectToAction("SignUp");
            }
            return View("Verify", model);
        }
        // View Verify Page
        // Sends model to CreateNewCustomer
        public ActionResult Verify()
        {
            return View();
        }
        // Creates new customer in Database
        public async Task<ActionResult> CreateNewCustomer(Customer model)
        {
            if (!ModelState.IsValid)
            {
                RedirectToAction("SignUp");
            }
            bool myBool = await _accountLogic.RegisterCustomerAsync(model);

            if (myBool)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorText = "Error";
                return View("Error");
            }
        }
        // List of all Customers
        public async Task<ActionResult> Customers()
        {
            List<Customer> users = await _accountLogic.CustomerListAsync();
            return View(users);
        }
        
        
    }
}
