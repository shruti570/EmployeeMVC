using Microsoft.AspNetCore.Mvc;
using WebProjectMVC.Models;
using WebProjectMVC.Repositories;

namespace WebProjectMVC.ApiControllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentController(IDepartmentRepository repo) { _repo = repo; }

        [HttpGet]
        public async Task<IActionResult> GetDept()
        {
            var dept = await _repo.GetDeptAsync();
            return Ok(dept);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeptById(int id)
        {
            var dept =await _repo.GetDeptByIdAsync(id);
            return dept == null ? NotFound() : Ok(dept);

        }
    }
}