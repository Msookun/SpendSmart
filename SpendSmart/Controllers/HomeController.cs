using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Models;

namespace SpendSmart.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly SpentSmartDbContext _context;

        public HomeController(ILogger<HomeController> logger, SpentSmartDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Expenses()
        {
            var allExpenses = _context.Expenses.ToList();

            var totalExpenses = allExpenses.Sum(x => x.Value);

            ViewBag.Expenses = totalExpenses;

            return View(allExpenses);
        }

        public IActionResult CreateEditExpense(int? id)
        {
            if(id != null)
            {
                var expenseInDb = _context.Expenses.FirstOrDefault(x => x.id == id);
                return View(new Expense());
            }

            return View();
        }

        public IActionResult DeleteExpense(int? id)
        {
            var expenseInDb = _context.Expenses.FirstOrDefault(x => x.id == id);
            _context.Expenses.Remove(expenseInDb);
            _context.SaveChanges();
            return RedirectToAction("Expenses");
        }

        public IActionResult CreateEditExpenseForm(Expense model)
        {
            if(model.id == 0)
            {
                _context.Expenses.Add(model);
            } else
            {
                _context.Expenses.Update(model);
            }
           
            _context.SaveChanges();

            return RedirectToAction("Expenses");
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
