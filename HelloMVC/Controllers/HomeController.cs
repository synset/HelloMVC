using HelloMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Caching;
using System.Security.Cryptography;

namespace HelloMVC.Controllers
{
    public class HomeController : Controller
    {
        ObjectCache cache = MemoryCache.Default;
        List<Customer> customers;

        //constructor
        public HomeController()
        {
            customers = cache["customers"] as List<Customer>;
            if(customers == null)
            {
                customers = new List<Customer>();
            }
        }

        public void SaveCache()
        {
            cache["customers"] = customers;
        }
       

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.MySuperProperty = "This is my first app";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult ViewCustomer(Customer postedCustomer)
        {
            Customer customer = new Customer();
            customer.Id = Guid.NewGuid().ToString();
            customer.Name = postedCustomer.Name;
            customer.Telephone = postedCustomer.Telephone;

            return View(customer);
        }

        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            customer.Id = Guid.NewGuid().ToString();
            customers.Add(customer);
            SaveCache();

            return RedirectToAction("customerList");
        }

        public ActionResult CustomerList()
        {
            return View(customers);
        }
    }
}