using TakidReciveForm.Domain.Data;
using TakidReciveForm.Domain.DTOs.ReadDTOs;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Domain.Interfaces;

public interface IFormRepository
{
    public PagedResult<Form> GetAll(int page);
    public Task<FormReadDto> GetByIdAsync(Guid id);
    public Task<FormReadDto?> DeleteAsync(Guid id);
    public Task<FormReadDto> InsertAsync(FormWriteDto form, string imageName);
    public Task<FormReadDto> UpdateAsync(Form form, string imageName);
}