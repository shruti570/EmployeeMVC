using WebProjectMVC.ApiModels;
using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Employee>> GetDeptAsync();
        Task<Employee?> GetDeptByIdAsync(int slNo);
        Task<IEnumerable<Department>> GetAllAsync();
    }
}
