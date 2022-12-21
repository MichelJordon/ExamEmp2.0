namespace Domain.Entities;
public class JobHistory
{
    public int JobHistoryId {get; set;}
    public DateTime CreatedAt { get; set; }
    public TimeSpan StartWork { get; set; }
    public TimeSpan EndWork { get; set; }
    public int EmployeeId { get; set; }
    public virtual Employee Employee {get; set;}
    public int JobId{get; set;}
    public virtual Job Job{get; set;}

    public JobHistory(int employeeId)
    {
        EmployeeId = employeeId;
        CreatedAt = DateTime.UtcNow;
        StartWork = DateTime.UtcNow.TimeOfDay;
    }
}