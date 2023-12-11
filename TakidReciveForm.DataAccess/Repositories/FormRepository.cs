using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TakidReciveForm.DataAccess.Data;
using TakidReciveForm.Domain.Data;
using TakidReciveForm.Domain.DTOs.ReadDTOs;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Interfaces;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.DataAccess.Repositories;

public class FormRepository : IFormRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly IMapper _mapper;

    public FormRepository(AppDbContext context, IMapper mapper)
    {
        _appDbContext = context;
        _mapper = mapper;
    }

    public async Task<FormReadDto?> DeleteAsync(Guid id)
    {
        var result = await _appDbContext.Forms.FirstOrDefaultAsync(f => f.FormId == id);
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

    public PagedResult<Form> GetAll(int page = 1)
    {
        int pageSize = 5;

        var result = _appDbContext.Forms
            .AsNoTracking()
            .OrderBy(f => f.FormId)
            .GetPaged(page, pageSize);

        return result;
    }

    public async Task<FormReadDto> GetByIdAsync(Guid id)
    {
        var result = await _appDbContext.Forms
                .AsNoTracking()
                .FirstOrDefaultAsync(f =>
                    f.FormId == id);
        var formDto = _mapper.Map<FormReadDto>(result);
        if (result != null)
        {
            return formDto;
        }
        else
        {
            throw new KeyNotFoundException("Form not found");
        }
    }

    public async Task<FormReadDto> InsertAsync(FormWriteDto formWriteDto, string imageName)
    {
        formWriteDto.Image = imageName;
        var form = _mapper.Map<Form>(formWriteDto);
        var result = await _appDbContext.Forms.AddAsync(form);
        await _appDbContext.SaveChangesAsync();
        var formReadDto = _mapper.Map<FormReadDto>(result.Entity);
        return formReadDto;
    }

    public async Task<FormReadDto> UpdateAsync(Form form, string imageName)
    {
        var result = await _appDbContext.Forms.FirstOrDefaultAsync(f => f.FormId == form.FormId);
        form.Image = imageName;
        if (result is not null)
        {
            // Update existing person
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
}