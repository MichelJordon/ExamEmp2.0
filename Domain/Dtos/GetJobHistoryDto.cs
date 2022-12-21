using Domain.Entities;
namespace Domain.Dtos;
public class GetJobHistoryDto{
    public int EmployeeId { get; set; }
    public string Start { get; set; }
    public string End { get; set; }
}