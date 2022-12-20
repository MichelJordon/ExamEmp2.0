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
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate {get; set;}    
}