using CustompgAdmin.Services.DTOs.Table;
using CustompgAdmin.Services.Services.TableServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace CustompgAdmin.Controllers;

[Route("api/databases/{databaseId}/[controller]")]
[ApiController]
public class TablesController : ControllerBase
{
    private readonly ITableService _service;
    public TablesController(ITableService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int databaseId)
    {
        try
        {
            return Ok(await _service.All(databaseId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTable createTable,int databaseId)
    {
        try
        {
             await _service.CreateTableAsync(createTable, databaseId);
            return Ok();
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
            return Ok( await _service.GetByTableIdAsync(id));
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
            await _service.DeleteTableAsync(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("view-data")]
    public async Task<IActionResult> ViewData(int id,int databaseId)
    {
        try
        {
          return Ok(await _service.ViewData(id,databaseId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpGet("get-script")]
    public async Task<IActionResult> GetScripts(int databaseId, int id, int scriptId)
    {
        try
        {
            return Ok( await _service.GetScripts(id,databaseId,scriptId));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("write-script")]
    public async Task<IActionResult> WriteScript(int id,string script)
    {
        try
        {
            await _service.WriteScripts(id,script);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("write")]
    public async Task<IActionResult> CreateQuery(int databaseId,string query)
    {
        try
        {
            await _service.WriteQuery(databaseId, query);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost("write-query")]
    public async Task<IActionResult> WriteQuery(int databaseId,string query)
    {
        try
        {
            return Ok(await _service.WriteQuery(databaseId, query));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
