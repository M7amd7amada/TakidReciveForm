using Microsoft.AspNetCore.Mvc;

using TakidReciveForm.Api.Services;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FormsController : ControllerBase
{
    private readonly IFormRepository _formRepository;
    private readonly IImagesService _imagesService;

    public FormsController(IFormRepository formRepository, IImagesService imagesService)
    {
        _formRepository = formRepository;
        _imagesService = imagesService;
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] FormWriteDto formWriteDto, IFormFile image)
    {
        var imageName = await _imagesService.SaveImage(image);
        return Ok(await _formRepository.InsertAsync(formWriteDto, imageName));
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
    public async Task<IActionResult> Update([FromForm] Form form, IFormFile image)
    {
        _imagesService.DeleteImage(form.Image);
        var imageName = await _imagesService.SaveImage(image);
        return Ok(await _formRepository.UpdateAsync(form, imageName));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        return Ok(await _formRepository.DeleteAsync(id));
    }
}