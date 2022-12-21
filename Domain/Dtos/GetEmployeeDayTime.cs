using Domain.Entities;
namespace Domain.Dtos;
using Microsoft.AspNetCore.Http;
public class GetEmployeeDayTime
{
    public int EmployeeId { get; set; }
    public string? Day {get; set;}
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime? Date { get; set; }
    public string? StartWork { get; set; }
    public string? EndWork {get; set;}    
}