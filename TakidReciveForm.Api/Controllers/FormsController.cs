using Microsoft.AspNetCore.Mvc;

using TakidReciveForm.Api.Services;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Models;
using TakidReciveForm.Domain.Services;

namespace TakidReciveForm.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class FormsController : ControllerBase
{
    private readonly IFormRepository _formRepository;
    private readonly IImagesService _imagesService;
    private readonly IAttachmentService _attachmentService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly string _rootPath;

    public FormsController(
        IFormRepository formRepository,
        IImagesService imagesService,
        IAttachmentService attachmentService,
        IWebHostEnvironment webHostEnvironment)
    {
        _formRepository = formRepository;
        _imagesService = imagesService;
        _attachmentService = attachmentService;
        _webHostEnvironment = webHostEnvironment;
        _rootPath = $"{_webHostEnvironment.WebRootPath}{FileSettings.ImagesPath}";
    }

    [HttpPost]
    public async Task<IActionResult> Insert([FromForm] FormWriteDto formWriteDto)
    {
        return Ok(await _formRepository.InsertAsync(formWriteDto, _rootPath));
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
        _imagesService.ConvertToImage(form.ImageBase64, form.ImageName);
        return Ok(await _formRepository.UpdateAsync(form));
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] Guid id)
    {
        return Ok(await _formRepository.DeleteAsync(id));
    }
}