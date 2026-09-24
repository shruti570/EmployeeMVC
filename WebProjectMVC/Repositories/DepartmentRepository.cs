using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using WebProjectMVC.ApiModels;
using WebProjectMVC.Models;

namespace WebProjectMVC.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly string _connectionString;

        public DepartmentRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MyConnectionString")!;
        }

        private IDbConnection CreateConnection() => new SqlConnection(_connectionString);

        public async Task<Employee?> GetDeptByIdAsync(int slNo)
        {
            using var connection = CreateConnection();
            return await connection.QuerySingleOrDefaultAsync<Employee>(
                "Usp_GetEmpDeptBySlno",
                new { SlNo = slNo },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Employee>> GetDeptAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Employee>(
                "usp_GetDepartment",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Department>> GetAllAsync()
        {
            using var connection = CreateConnection();
            return await connection.QueryAsync<Department>(
                "usp_GetDepartment",
                commandType: CommandType.StoredProcedure);
        }
    }
}
