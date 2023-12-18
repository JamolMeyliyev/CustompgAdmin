using CustompgAdmin.Services.DTOs.Connection;
using CustompgAdmin.Services.Services.Connection;
using Microsoft.AspNetCore.Mvc;

namespace CustompgAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConnectionsController : ControllerBase
    {
        public readonly IConnectionService _service;
   

        public ConnectionsController(IConnectionService service)
        {
            _service = service;
            
        }
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _service.GetConnectionsAsync());
        }
        [HttpPost]
        public IActionResult Create([FromBody] CreateConnectionDB connection)
        {
            try
            {
                 
                return Ok(_service.ConnectServer(connection));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public IActionResult ConnectServer(int id,string password)
        {
            try
            {
                return Ok(_service.ConnectServerForChaeckPassword(id, password));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
