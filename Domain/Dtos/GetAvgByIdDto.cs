using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Domain.Dtos;
public class GetAvg
{
    
    public TimeSpan StartDate {get; set;}
}