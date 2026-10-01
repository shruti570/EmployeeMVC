using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int slNo);
        Task<int> CreateAsync(Employee emp);
        Task<int> UpdateAsync(Employee emp);
        Task<int> UpdateApiAsync(int SlNo, int DepartmentId, int ReportingPersonId);
  
        Task<int> DeleteAsync(int SlNo);

        Task<IEnumerable<Employee>> GetStateAsync();
        Task<IEnumerable<Employee>> GetDistrictAsync(int StateId);
        Task<IEnumerable<Employee>> GetCityAsync(int DistrictId);
        Task<IEnumerable<Employee>> GetPincodeAsync(int CityId);
        Task<IEnumerable<Employee>> GetAreaAsync(int PincodeId);

        Task<IEnumerable<Employee>> GetReportingPersonAsync();
      


       














    }
}
  
