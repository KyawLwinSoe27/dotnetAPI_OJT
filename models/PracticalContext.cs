using Dapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;

namespace PracticeApi.Models
{
    public class PracticalContext : DbContext
    {
        // public PracticalContext(DbContextOptions<PracticalContext> options)
        //     : base(options)
        // {
        // }

        private static readonly IConfiguration _configuration = Startup.StaticConfiguration!;
        private readonly string _connectionString = _configuration.GetSection("connectionStrings:DefaultConnection").Get<string>();
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IActionContextAccessor _actionContextAccessor;

        public PracticalContext(DbContextOptions<PracticalContext> options, IHttpContextAccessor httpContextAccessor, IActionContextAccessor actionContextAccessor)
        :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            _actionContextAccessor = actionContextAccessor;
        }

        public DbSet<TodoItem> TodoItems { get; set; } = null!;
        public DbSet<Hero> Heroes {get; set;} = null!;
        public DbSet<CustomerType> CustomerTypes {get; set;} = null!;
        public DbSet<Customer> Customers { get; set;} = null!;
        public DbSet<AdminLevel> adminLevels {get; set;} = null!;
        public DbSet<Admin> admins { get; set; } = null!;

        public DbSet<OTP> OTPs { get; set; } = null!;

        public DbSet<EventLog> EventLogs { get; set; } = null!;


        public async Task<int> RunExecuteNonQuery(string Query, ExpandoObject queryFilter)
        {
            if(queryFilter == null)
            {
                queryFilter = new ExpandoObject();
            }

            using (MySqlConnection conn = new (_connectionString))
            {
                var result = await conn.ExecuteAsync(Query, queryFilter);
                
                return result;
            } 
        }

        public async Task<dynamic> RunExecuteSelectQuery(string Query, ExpandoObject? queryFilter = null)
        {
            if(queryFilter == null)
            {
                queryFilter = new ExpandoObject();
            }

            using (var conn = new MySqlConnection(_connectionString))  
            {  
                var result = await conn.QueryAsync(Query, queryFilter);
                return result;
            } 
        }

        public async Task<List<T>> RunExecuteSelectQuery<T>(string Query, ExpandoObject? queryFilter = null)
        {
            if(queryFilter == null)
            {
                queryFilter = new ExpandoObject();
            }

            using (var conn = new MySqlConnection(_connectionString))  
            {  
                var result = await conn.QueryAsync<T>(Query, queryFilter);
                return result.ToList();
            } 
        }
    }
}