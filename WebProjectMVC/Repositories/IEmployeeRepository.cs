// Repositories/IEmployeeRepository.cs
using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int slNo);
        Task<int> CreateAsync(Employee emp);
        Task<int> UpdateAsync(Employee emp);
        Task<int> DeleteAsync(int slNo);
    }
}