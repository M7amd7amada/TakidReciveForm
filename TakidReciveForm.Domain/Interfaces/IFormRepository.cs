using TakidReciveForm.Domain.Data;
using TakidReciveForm.Domain.DTOs.ReadDTOs;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Domain.Interfaces;

public interface IFormRepository
{
    public PagedResult<FormReadDto> GetAll(int page);
    public Task<FormReadDto> GetByIdAsync(Guid id);
    public Task<FormReadDto?> DeleteAsync(Guid id, string attachmentsPath);
    public Task<FormReadDto> InsertAsync(FormWriteDto form, string attachmentsPath);
    public Task<FormReadDto> UpdateAsync(Form form, string attachmentsPath);
}