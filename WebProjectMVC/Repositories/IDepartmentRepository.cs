using WebProjectMVC.ApiModels;
using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public interface IDepartmentRepository


    {
        Task<IEnumerable<Department>> GetDeptAsync();
     

        Task<Department?> GetDeptByIdAsync(int slNo);

        Task<IEnumerable<Department>> GetDepartmentsAsync();



    }
}