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
        CreateMap<JobHistory, GetJobHistoryDto>()
            .ForMember(x=>x.Start, y=>y.MapFrom(z=>z.StartWork.ToString()))
            .ForMember(x=>x.End, y=>y.MapFrom(z=>z.EndWork.ToString()))
            ;
        CreateMap<GetJobHistoryDto, AddJobHistoryDto>().ReverseMap();
    }
}