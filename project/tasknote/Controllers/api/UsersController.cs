using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Dapper;
using tasknote.Models;
using System.Data;

namespace tasknote.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly string _connectionString;

       public UsersController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                ?? throw new ArgumentNullException("Connection string is missing in the configuration.");
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            // if (user == null)
            // {
            //     return BadRequest();
            // }

            // Define your SQL insert query
            var query = "INSERT INTO Users (Name) VALUES (@Name); SELECT LAST_INSERT_ID();";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var newUserId = await connection.QuerySingleAsync<int>(query, new { user.Name });
                user.UserId = newUserId;
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var query = "SELECT * FROM Users WHERE UserId = @UserId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var user = await connection.QuerySingleOrDefaultAsync<User>(query, new { UserId = id });
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User updateUser)
        {
            if (updateUser == null || id != updateUser.UserId)
            {
                return BadRequest(); // Invalid request data
            }

            var query = "UPDATE Users SET Name = @Name WHERE UserId = @UserId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { updateUser.Name, UserId = id });
                if (rowsAffected == 0)
                {
                    return NotFound(); // User not found
                }
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var query = "SELECT * FROM Users";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var users = await connection.QueryAsync<User>(query);
                return Ok(users);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var query = "DELETE FROM Users WHERE UserId = @UserId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                var rowsAffected = await connection.ExecuteAsync(query, new { UserId = id });
                if (rowsAffected == 0)
                {
                    return NotFound(); // User not found
                }
            }

            return NoContent();
        }
    }
}
