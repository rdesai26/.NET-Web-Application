using Final_Desai.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Final_Desai.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private OrderContext _context;

        public HomeController(OrderContext orderContext)
        {
            _context = orderContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Submit(UserOrder userOrder)
        {

            if (ModelState.IsValid)
            {
                try
                {

                    userOrder.Fries = userOrder.Fries.ToUpper(); // allows only capital Y/N (2 values) to be stored into database instead of Y/N/y/n (4 values)
                    userOrder.Cost = (decimal)(5.00 + (0.50 * userOrder.TotalPatties)); // calculates cost with additional patties.
                    userOrder.TotalPatties = userOrder.TotalPatties + 1; // +1 to take into account the single patty always included.

                    if (userOrder.Bacon == true) // increases price if bacon is selected.
                    {
                        userOrder.Cost += 1.00m;
                    }
                    if (userOrder.Fries.ToUpper().Equals("Y")) //increases price if fries are selected.
                    {
                        userOrder.Cost += 3.00m;
                    }
                    _context.Add(userOrder); //adds and saves information to database.
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    return View("Error", e);
                }
                UserOrderViewModel orderInfo = new UserOrderViewModel();
                orderInfo.Order = userOrder;
                return View("Submit", orderInfo);
            }
            else
            {
                return View("Error");
            } 
        }
        public IActionResult Orders()
        {
           // adds to list all orders from the database and shows in on the view.
            List<UserOrder> orders;
            orders = (from r in _context.Orders
                             select r).ToList();
            return View(orders);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
