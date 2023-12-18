using CustompgAdmin.Services.DTOs.Query;
using CustompgAdmin.Services.Services.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustompgAdmin.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QueriesController : ControllerBase
{
    private readonly IQueryService _service;
    public QueriesController(IQueryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] QueryFilter filter)
    {
        try
        {
            return Ok(await _service.GetAll(filter));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
    [HttpPost]
    public async Task<IActionResult> Create(string query)
    {
        try
        {
            await _service.CreateAsync(query);
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
            return Ok(await _service.GetByIdAsync(id));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);  
        }
    }
}
