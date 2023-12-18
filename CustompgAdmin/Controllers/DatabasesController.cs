using CustompgAdmin.Services.DTOs.Database;
using CustompgAdmin.Services.Services.Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustompgAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatabasesController : ControllerBase
    {
        private readonly IDatabaseService _service;
        public DatabasesController(IDatabaseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _service.GetDatabasesAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDatabase([FromBody] CreateDatabase createDatabase)
        {
            try
            {
                return Ok(await _service.CreateDatabaseAsync(createDatabase));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                return Ok(await _service.GetDatabaseById( id));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("sql")]
        public IActionResult ViewSQLCode([FromBody] CreateDatabase createDatabase)
        {
            return Ok(_service.ViewSQLCode(createDatabase));
        }
        
       

        [HttpPost("write-query")]
        public async Task<IActionResult> WriteQuery(int id, string query)
        {
            try
            {
                return Ok(await _service.WriteQuery(id, query));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
