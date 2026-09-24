using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.ApiModels;
using WebProjectMVC.Models;
using WebProjectMVC.Repositories;
using static DevExpress.Utils.Zip.Internal.SecureZipTrace;

namespace WebProjectMVC.ApiControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeesController(IEmployeeRepository repo) => _repo = repo;

        [HttpPost("GetAllEmp")]
        public async Task<IActionResult> GetAll()
        {
            var employees = await _repo.GetAllAsync();
            return Ok(employees);
        }

        [HttpPost("GetEmpById")]
        public async Task<IActionResult> GetById([FromBody] EmpID request)
        {

            var employee = await _repo.GetByIdAsync(request.slNo);
            return employee == null ? NotFound() : Ok(employee);
        }


        [HttpPost("UpdateEmpData")]
        public async Task<IActionResult> UpdateAsync([FromBody] EmpID1 request )
        {
            var rowsAffected = await _repo.UpdateApiAsync(request.SlNo, request.DepartmentId, request.ReportingPersonId);
            return rowsAffected > null ? NotFound() : Ok(rowsAffected);
        }
    }
}