using Microsoft.AspNetCore.Mvc;

using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FormsController : ControllerBase
{
    private readonly IFormRepository _formRepository;

    public FormsController(IFormRepository formRepository)
    {
        _formRepository = formRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] FormWriteDto formWriteDto)
    {
        return Ok(await _formRepository.InsertAsync(formWriteDto));
    }

    [HttpGet]
    public IActionResult GetAll([FromQuery] int page = 1)
    {
        return Ok(_formRepository.GetAll(page));
    }

    [HttpGet()]
    public async Task<IActionResult> GetById([FromQuery] Guid id)
    {
        return Ok(await _formRepository.GetByIdAsync(id));
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] Form form)
    {
        return Ok(await _formRepository.UpdateAsync(form));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        return Ok(await _formRepository.DeleteAsync(id));
    }
}