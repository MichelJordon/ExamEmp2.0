using Domain.Entities;
using Microsoft.AspNetCore.Http;
namespace Domain.Dtos;
public class GetEmployeeByPhone
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
}