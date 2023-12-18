using CustompgAdmin.Services.Services.ColumnServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace CustompgAdmin.Controllers
{
    [Route("api/tables/{tableId}/[controller]")]
    [ApiController]
    public class ColumnsController : ControllerBase
    {
        private readonly IColumnService _service;
        public ColumnsController(IColumnService service)
        {
            _service = service;
        }
        [HttpGet]
        public async ValueTask<IActionResult> All(int tableId)
        {
            try
            {
                return Ok(await _service.GetAll(tableId));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("data")]
        public async ValueTask<IActionResult> ViewDataByColumnId(int tableId,int id)
        {
            try
            {
                return Ok(await _service.ViewDataByColumnId(id, tableId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("write-query")]
        public async Task<IActionResult> WriteQuery(int tableId, string query)
        {
            try
            {
                return Ok(await _service.WriteQuery(query,tableId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
