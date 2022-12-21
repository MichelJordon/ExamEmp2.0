using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
public class JobHistoryService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public JobHistoryService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetJobHistoryDto>>> GetJobHistories()
    {
        var list = _mapper.Map<List<GetJobHistoryDto>>(_context.JobHistories.ToList());
        return new Response<List<GetJobHistoryDto>>(list.ToList());
    }
    public async Task<Response<TimeSpan>> GetAvgById(int id, DateTime start, DateTime end)
    {
        try
        {
            // var linq = (
            // from j in _context.JobHistories
            // where j.EmployeeId == id && j.StartDate >= start && j.EndDate <= end  
            // select new GetAvg(j.StartDate.TimeOfDay)
            // ).ToList();

            var totalTime = _context.JobHistories
                .Where(j => j.EmployeeId == id && j.CreatedAt >= start && j.CreatedAt <= end)
                .Select(x=>x.StartWork).ToList();
            if(totalTime.Count == 0)
            {
                return new Response<TimeSpan>(HttpStatusCode.NotFound, "Not Found");
            }
            
            var average = totalTime.Average(x=>x.TotalMilliseconds);
            var time = TimeSpan.FromMilliseconds(average);

            return new Response<TimeSpan>(time);    
        }
        catch (Exception ex)
        {
            return new Response<TimeSpan>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetJobHistoryDto>> StartAndEndJob(AddJobHistoryDto jobHistory)
    {
        try
        {
            var employee = _context.Employees.Find(jobHistory.EmployeeId);
            if(employee == null)
            {
                return new Response<GetJobHistoryDto>(HttpStatusCode.NotFound, "Not Found");
            }

            var existingDate = _context.JobHistories
                .FromSqlRaw(
                    $"select * from \"JobHistories\" where \"EmployeeId\" = {jobHistory.EmployeeId} and Date(\"CreatedAt\") = '{DateTime.Now.Date.ToString("yyyy-MM-dd")}'")
                .FirstOrDefault();
               
            if(existingDate == null)
            {
                var job = new JobHistory(jobHistory.EmployeeId);
                job.JobId = jobHistory.JobId;
                _context.JobHistories.Add(job);
                await _context.SaveChangesAsync();
            }
            else
            {
                existingDate.EndWork = DateTime.UtcNow.TimeOfDay;
                _context.JobHistories.Update(existingDate);
                await _context.SaveChangesAsync();
            }

            return new Response<GetJobHistoryDto>(new GetJobHistoryDto()
            {
                EmployeeId = jobHistory.EmployeeId,
                End = existingDate.EndWork == null ? TimeSpan.Zero.ToString() : existingDate.EndWork.ToString(),
                Start = existingDate.StartWork.ToString()
            });

        }
        catch (Exception ex)
        {
            return new Response<GetJobHistoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    

    public async Task<Response<string>> DeleteJobHistory(int id)
    {
        try
        {
            var find = await _context.JobHistories.FindAsync(id);
            _context.Remove(find);
            var response = await _context.SaveChangesAsync();
            if (response > 0)
                return new Response<string>("Object deleted successfully");
            return new Response<string>(HttpStatusCode.BadRequest, "Object not found");  
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

}