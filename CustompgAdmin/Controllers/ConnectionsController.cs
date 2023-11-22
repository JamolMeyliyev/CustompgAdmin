using CustompgAdmin.DataAccess.Entities;
using CustompgAdmin.DataAccess.Repositories.Connection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustompgAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        public readonly IConnectionRepository _repos;
        public readonly IConfiguration _config;

        public ConnectionsController(IConnectionRepository repos, IConfiguration config)
        {
            _repos = repos;
            _config = config;
        }
        [HttpPost]
        public  IActionResult Create([FromBody] ConnectionDB connection)
        {
            try
            {
                 _repos.CreateMigrateUpdateDatabase(connection);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
