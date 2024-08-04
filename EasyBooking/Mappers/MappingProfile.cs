using AutoMapper;
using EasyBooking.Api.Dto;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;

namespace EasyBooking.Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Desk, DeskDto>();
        CreateMap<OfficeLocation, OfficeLocationDto>();
        CreateMap<Employee, EmployeeDto>();

    }
}
