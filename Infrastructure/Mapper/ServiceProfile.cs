using AutoMapper;
using Domain.Dtos;
using Domain.Entities;

namespace Infrastucture.Meppers;
public class ServicesProfile:Profile
{
    public ServicesProfile()
    {
        CreateMap<Employee, GetEmployeeDto>().ReverseMap();
        CreateMap<Employee, AddEmployeeDto>().ReverseMap();
        CreateMap<GetEmployeeDto, AddEmployeeDto>().ReverseMap();

        CreateMap<Job, AddJobDto>().ReverseMap();
        CreateMap<Job, GetJobDto>().ReverseMap();
        CreateMap<GetJobDto, AddJobDto>().ReverseMap();

        CreateMap<JobHistory, AddJobHistoryDto>().ReverseMap();
        CreateMap<JobHistory, GetJobHistoryDto>().ReverseMap();
        CreateMap<GetJobHistoryDto, AddJobHistoryDto>().ReverseMap();
    }
}