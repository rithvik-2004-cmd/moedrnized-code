using Microsoft.AspNetCore.Mvc;
using Invoice.Api.Models;
using Invoice.Api.Services;

namespace Invoice.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _service;

    public InvoiceController(IInvoiceService service) => _service = service;

    [HttpGet]
    public ActionResult<IEnumerable<Invoice>> GetAll() => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Invoice> Get(int id)
    {
        var item = _service.GetById(id);
        return item is null ? NotFound() : Ok(item);
    }

    [HttpPost]
    public ActionResult<Invoice> Create(Invoice item)
    {
        try
        {
            var created = _service.Create(item);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("{id:int}/close")]
    public IActionResult Close(int id) => _service.Close(id) ? NoContent() : NotFound();
}
