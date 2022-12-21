using Domain.Entities;
namespace Domain.Dtos;
public class AddJobHistoryDto
{
    public int EmployeeId { get; set; }
    public int JobId { get; set; }
}