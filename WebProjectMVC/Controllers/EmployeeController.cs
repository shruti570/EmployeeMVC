
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
        private readonly IDepartmentRepository _departmentRepo;

        public EmployeeController(
            IEmployeeRepository repository,
            IDepartmentRepository departmentRepo)
        {
            _repository = repository;
            _departmentRepo = departmentRepo;
        }


        // ==============================
        // Employee List
        // ==============================
        public IActionResult Index()
        {
            return View();
        }


        // ==============================
        // Get Employees - DevExtreme Grid
        // ==============================
        [HttpPost]
        public async Task<ActionResult> GetEmployees(
            DataSourceLoadOptions loadOptions)
        {
            var employees = await _repository.GetAllAsync();

            return Json(
                DataSourceLoader.Load(employees, loadOptions)
            );
        }


        // ==============================
        // Get Reporting Persons
        // ==============================
        [HttpGet]
        public async Task<IActionResult> GetReportingPersons()
        {
            var employees = await _repository.GetAllAsync();

            var reportingPersons = employees
                .Select(x => new
                {
                    id = x.SlNo,
                    name = x.Empname
                })
                .ToList();

            return Json(reportingPersons);
        }


        // ==============================
        // Get Departments
        // ==============================
        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _departmentRepo.GetAllAsync();

            var result = departments
                .Select(x => new
                {
                    id = x.Id,
                    name = x.Name
                })
                .ToList();

            return Json(result);
        }


        // ==============================
        // Details
        // ==============================
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


        // ==============================
        // Create - GET
        // ==============================
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(new Employee());
        }


        // ==============================
        // Create - POST
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (!ModelState.IsValid)
            {
                return await ReturnCreateErrorViewAsync(
                    employee,
                    "Please fill all required fields correctly."
                );
            }


            // Employee Code validation
            if (!string.IsNullOrWhiteSpace(employee.EmpCode)
                && employee.EmpCode.Length < 3)
            {
                ModelState.AddModelError(
                    "EmpCode",
                    "Employee Code must be at least 3 characters long."
                );

                return await ReturnCreateErrorViewAsync(
                    employee,
                    "Invalid Employee Code length."
                );
            }


            // Salary validation
            if (employee.Salary <= 0)
            {
                ModelState.AddModelError(
                    "Salary",
                    "Salary must be a positive value greater than zero."
                );

                return await ReturnCreateErrorViewAsync(
                    employee,
                    "Invalid salary entry."
                );
            }


            // Created By
            employee.CreatedBY = "ShrutiMOre";


            var rowsAffected =
                await _repository.CreateAsync(employee);


            if (rowsAffected > 0)
            {
                TempData["SuccessMessage"] =
                    "Employee profile configured successfully!";

                return RedirectToAction(nameof(Index));
            }


            return await ReturnCreateErrorViewAsync(
                employee,
                "Failed to register employee record in database systems."
            );
        }


        // ==============================
        // Edit - GET
        // ==============================
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee =
                await _repository.GetByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }


        // ==============================
        // Edit - POST
        // ==============================
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


        // ==============================
        // Delete
        // ==============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _repository.DeleteAsync(id);

            if (result > 0)
            {
                TempData["SuccessMessage"] =
                    "Employee deleted successfully.";
            }

            return RedirectToAction(nameof(Index));
        }


        // ==============================
        // Create Error View
        // ==============================
        private async Task<IActionResult>
            ReturnCreateErrorViewAsync(
                Employee model,
                string errorMessage)
        {
            TempData["ErrorMessage"] = errorMessage;

            return View("Create", model);
        }
    }
}

