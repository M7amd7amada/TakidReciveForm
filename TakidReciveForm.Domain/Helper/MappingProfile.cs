using AutoMapper;
using TakidReciveForm.Domain.Data;
using TakidReciveForm.Domain.DTOs.ReadDTOs;
using TakidReciveForm.Domain.DTOs.WriteDTOs;
using TakidReciveForm.Domain.Models;

namespace TakidReciveForm.Domain.Helper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // Forms
        CreateMap<Form, FormReadDto>();
        CreateMap<FormWriteDto, Form>();
    }
}