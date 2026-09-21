using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.Models;
using WebProjectMVC.Repositories;

namespace WebProjectMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeController(IEmployeeRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        
        [HttpGet]
        public async Task<ActionResult> GetEmployees(DataSourceLoadOptions loadOptions)
        {
            var employees = await _repository.GetAllAsync();

            return Json(DataSourceLoader.Load(employees, loadOptions));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            await _repository.UpdateAsync(employee);

            return RedirectToAction(nameof(Index));
        }
    }
}