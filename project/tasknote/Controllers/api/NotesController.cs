using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading.Tasks;
using tasknote.Models;

namespace tasknote.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly string _connectionString;

        // Mark connection string as nullable and validate it in the constructor
        public NotesController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") 
                                ?? throw new ArgumentNullException("Connection string is missing in the configuration.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            // If the note object is null, return a BadRequest response
            // if (note == null)
            // {
            //     return BadRequest("Note cannot be null.");
            // }

            // Set CreatedAt and UpdatedAt to the current time
            note.CreatedAt = DateTime.Now;
            note.UpdatedAt = DateTime.Now;

            // SQL query to insert a new note into the database
            const string query = @"
                INSERT INTO Notes (Title, Content, UserId, CreatedAt, UpdatedAt) 
                VALUES (@Title, @Content, @UserId, @CreatedAt, @UpdatedAt);
                SELECT LAST_INSERT_ID();";  // Get the Last Inserted ID to return the created note

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                // Execute the query and retrieve the last inserted ID
                var noteId = await dbConnection.QuerySingleAsync<int>(query, note);

                // Set the NoteId for the note object (this is the ID we just inserted)
                note.NoteId = noteId;

                // Return the newly created note with a Created status (HTTP 201)
                return CreatedAtAction(nameof(GetNote), new { id = note.NoteId }, note);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetNote(int id)
        {
            const string query = "SELECT * FROM Notes WHERE NoteId = @Id";

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var note = await dbConnection.QuerySingleOrDefaultAsync<Note>(query, new { Id = id });
                if (note == null)
                {
                    return NotFound();
                }
                return Ok(note);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(int id, [FromBody] Note updateNote)
        {
            

            const string query = @"UPDATE Notes 
                                   SET Title = @Title, Content = @Content, UserId = @UserId, UpdatedAt = @UpdatedAt 
                                   WHERE NoteId = @NoteId";

            // Update the UpdatedAt before passing it to Dapper
            updateNote.UpdatedAt = DateTime.Now;

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var affectedRows = await dbConnection.ExecuteAsync(query, new
                {
                    updateNote.Title,
                    updateNote.Content,
                    updateNote.UserId,
                    updateNote.UpdatedAt,
                    NoteId = id
                });

                if (affectedRows == 0)
                {
                    return NotFound(); // Return 404 if the note doesn't exist
                }

                return NoContent();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllNotes([FromQuery] int? userId = null)
        {
            const string query = "SELECT * FROM Notes WHERE (@UserId IS NULL OR UserId = @UserId)";

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var notes = await dbConnection.QueryAsync<Note>(query, new { UserId = userId });
                return Ok(notes);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(int id)
        {
            const string query = "DELETE FROM Notes WHERE NoteId = @Id";

            using (IDbConnection dbConnection = new MySqlConnection(_connectionString))
            {
                var affectedRows = await dbConnection.ExecuteAsync(query, new { Id = id });

                if (affectedRows == 0)
                {
                    return NotFound(); // Return 404 if the note doesn't exist
                }

                return NoContent();
            }
        }
    }
}
