using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString;

        public EmployeeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString")!;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Employee>(
                "usp_GetEmployee",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Employee?> GetByIdAsync(int slNo)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Employee>(
                "usp_GetEmployeeBySlno",
                new { SlNo = slNo },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(Employee emp)
        {
            using var connection = CreateConnection();
            var parameters = new
            {
                emp.EmpCode,
                emp.Empname,
                emp.ReportingPersonId,
                emp.DepartmentId,
                emp.Salary,
                emp.CreatedBY
            };
            return await connection.ExecuteAsync(
                "usp_InsertEmployee",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Employee emp)
        {
            using var connection = CreateConnection();
            var parameters = new
            {
                emp.SlNo,
                emp.DepartmentId,
                emp.ReportingPersonId,
            };
            return await connection.ExecuteAsync(
                "usp_UpdateEmployee",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateApiAsync(int SlNo, int DepartmentId, int ReportingPersonId)
        {
            using var connection = CreateConnection();
            var parameters = new
            {
                SlNo,
                DepartmentId,
                ReportingPersonId,
            };
            return await connection.ExecuteAsync(
                "usp_UpdateEmployee",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteAsync(int slNo)
        {
            using var connection = CreateConnection();
            return await connection.ExecuteAsync(
                "usp_DeleteEmp",
                new { SlNo = slNo },
                commandType: CommandType.StoredProcedure);
        }

        


    }
}
