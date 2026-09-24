using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebProjectMVC.Models;
using WebProjectMVC.Repositories;

namespace WebProjectMVC.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _repository;
        private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(IEmployeeRepository repository, IDepartmentRepository departmentRepo)
        {
            _repository = repository;
            _departmentRepo = departmentRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> GetEmployees(DataSourceLoadOptions loadOptions)
        {
            var employees = await _repository.GetAllAsync();
            return Json(DataSourceLoader.Load(employees, loadOptions));
        }

        [HttpPost]
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
        public async Task<IActionResult> Create()
        {
            await PopulateDropdownsAsync();
            return View(new Employee());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnCreateErrorViewAsync(employee, "Please fill all required fields correctly.");
            }

            if (!string.IsNullOrWhiteSpace(employee.EmpCode) && employee.EmpCode.Length < 3)
            {
                ModelState.AddModelError("EmpCode", "Employee Code must be at least 3 characters long.");
                return await ReturnCreateErrorViewAsync(employee, "Invalid Employee Code length.");
            }

            if (employee.Salary <= 0)
            {
                ModelState.AddModelError("Salary", "Salary must be a positive value greater than zero.");
                return await ReturnCreateErrorViewAsync(employee, "Invalid salary entry.");
            }

            employee.CreatedBY = "ShrutiMOre";

            var rowsAffected = await _repository.CreateAsync(employee);

            if (rowsAffected > 0)
            {
                TempData["SuccessMessage"] = "Employee profile configured successfully!";
                return RedirectToAction(nameof(Index));
            }

            return await ReturnCreateErrorViewAsync(employee, "Failed to register employee record in database systems.");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            await PopulateDropdownsAsync();
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                await PopulateDropdownsAsync();
                return View(employee);
            }

            await _repository.UpdateAsync(employee);
            return RedirectToAction(nameof(Index));
        }

        private async Task PopulateDropdownsAsync()
        {
            var employees = await _repository.GetAllAsync();
ViewBag.Employees = new SelectList(employees, "SlNo", "Empname");

            var departments = await _departmentRepo.GetDeptAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
        }

        private async Task<IActionResult> ReturnCreateErrorViewAsync(Employee model, string errorMessage)
        {
            TempData["ErrorMessage"] = errorMessage;
            await PopulateDropdownsAsync();
            return View("Create", model);
        }
    }
}