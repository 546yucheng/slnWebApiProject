using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApiProject.Models;
using System.Text.Json;

namespace WebApiProject.Repositories
{
    public class MyOfficeRepository
    {
        private readonly string _connectionString;

        public MyOfficeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<MyOfficeModel>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            return await connection.QueryAsync<MyOfficeModel>("EXEC usp_GetMyoffice_ACPD");
        }

        public async Task<int> InsertAsync(MyOfficeModel model)
        {
            using var connection = new SqlConnection(_connectionString);
            var jsonData = System.Text.Json.JsonSerializer.Serialize(model);
            return await connection.ExecuteAsync("EXEC usp_InsertMyoffice_ACPD @JsonData", new { JsonData = jsonData });
        }

        public async Task UpdateAsync(MyOfficeModel model)
        {
            using var connection = new SqlConnection(_connectionString);
            var json = JsonSerializer.Serialize(model);
            await connection.ExecuteAsync("EXEC usp_UpdateMyoffice_ACPD @JsonData", new { JsonData = json });
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = new SqlConnection(_connectionString);
            var json = $"{{ \"ID\": \"{id}\" }}";
            await connection.ExecuteAsync("EXEC usp_DeleteMyoffice_ACPD @JsonData", new { JsonData = json });
        }
    }
}
