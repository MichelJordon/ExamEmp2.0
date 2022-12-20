namespace Domain.Entities;
public class JobHistory{
    public int JobHistoryId {get; set;}
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int EmployeeId { get; set; }
    public virtual Employee Employee {get; set;}
    public int JobId{get; set;}
    public virtual Job Job{get; set;}
}