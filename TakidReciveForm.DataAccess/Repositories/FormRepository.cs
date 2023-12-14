using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TakidReciveForm.DataAccess.Data;
using TakidReciveForm.Domain.Data;
using TakidReciveForm.Domain.DTOs.ReadDTOs;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Models;
using TakidReciveForm.Domain.Services;

namespace TakidReciveForm.DataAccess.Repositories;

public class FormRepository : IFormRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IAttachmentService _attachmentService;
    private readonly IMapper _mapper;

    public FormRepository(
        AppDbContext context,
        IMapper mapper,
        IAttachmentService attachmentService)
    {
        _appDbContext = context;
        _mapper = mapper;
        _attachmentService = attachmentService;
    }

    public async Task<FormReadDto?> DeleteAsync(Guid id, string rootPath)
    {
        var result = await _appDbContext.Forms.FirstOrDefaultAsync(f => f.FormId == id);

        // Delete Image Of The Form
        if (result is not null)
            _attachmentService.DeleteFile(result.ImageName, rootPath);

        var formDto = _mapper.Map<FormReadDto>(result);
        if (result != null)
        {
            _appDbContext.Forms.Remove(result);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Form not found");
        }
        return formDto;
    }

    public PagedResult<FormReadDto> GetAll(int page = 1)
    {
        int pageSize = 5;

        var result = _appDbContext.Forms
            .AsNoTracking()
            .OrderBy(f => f.ReceiverName)
            .GetPaged(page, pageSize);

        var formReadDto = _mapper.Map<PagedResult<FormReadDto>>(result);

        return formReadDto;
    }

    public async Task<FormReadDto> GetByIdAsync(Guid id)
    {
        var result = await _appDbContext.Forms
                .AsNoTracking()
                .FirstOrDefaultAsync(f =>
                    f.FormId == id);
        var formDto = _mapper.Map<FormReadDto>(result);
        return result is not null
            ? formDto
            : throw new KeyNotFoundException("Form not found");
    }

    public async Task<FormReadDto> InsertAsync(FormWriteDto formWriteDto, string rootPath)
    {
        await SaveImage(formWriteDto.ImageBase64, formWriteDto.ImageName, rootPath);
        var form = _mapper.Map<Form>(formWriteDto);
        var result = await _appDbContext.Forms.AddAsync(form);
        await _appDbContext.SaveChangesAsync();
        var formReadDto = _mapper.Map<FormReadDto>(result.Entity);
        return formReadDto;
    }

    public async Task<FormReadDto> UpdateAsync(Form form, string rootPath)
    {
        var result = await _appDbContext.Forms.FirstOrDefaultAsync(f => f.FormId == form.FormId);

        if (form.ImageBase64 != result!.ImageBase64)
        {
            // Delete Old Image
            _attachmentService.DeleteFile(result.ImageName, rootPath);
            // Add The New Image
            await SaveImage(form.ImageBase64, form.ImageName, rootPath);
        }

        if (result is not null)
        {
            _appDbContext.Entry(result).CurrentValues.SetValues(form);
            await _appDbContext.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Person not found");
        }
        var formReadDto = _mapper.Map<FormReadDto>(result);
        return formReadDto;
    }

    private async Task SaveImage(string imageBase64, string imageName, string rootPath)
    {
        if (_attachmentService.IsBase64String(imageBase64))
        {
            byte[] bytes = _attachmentService.GetBase64Bytes(imageBase64);
            string path = _attachmentService.GetFilePath(imageName, rootPath);
            await _attachmentService.SaveFileAsync(bytes, path);
        }
    }
}