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

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public async Task<Department?> GetDeptByIdAsync(int slNo)
        {
            using var connection = CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Department>(
                "Usp_GetEmpDeptBySlno",
                new { SlNo = slNo },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Department>> GetDeptAsync()
        {
            using var connection = CreateConnection();

            return await connection.QueryAsync<Department>(
                "usp_GetDept",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            using var connection = CreateConnection();

            return await connection.QueryAsync<Department>(
                "usp_GetDepartment",
                commandType: CommandType.StoredProcedure);
        }
    }
}