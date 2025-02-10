using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;
using tasknote.Models;
using System.Data;

namespace tasknote.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string _connectionString;
        public AuthController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                ?? throw new ArgumentNullException("Connection string is missing in the configuration.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuth([FromBody] Auth auth)
        {
            if (auth == null)
            {
                return BadRequest();
            }

            const string query = @"INSERT INTO Auth (Username, Password) 
                                   VALUES (@Username, @Password);
                                   SELECT LAST_INSERT_ID();";

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var authId = await dbConnection.QuerySingleAsync<int>(query, auth);
                auth.AuthId = authId;
                return CreatedAtAction(nameof(CreateAuth), new { id = auth.AuthId }, auth);
            }
        }

    }

}