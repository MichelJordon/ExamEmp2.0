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
            var linq = (
            from j in _context.JobHistories
            where j.EmployeeId == id && j.StartDate <= start && j.EndDate >= end  
            select new GetAvg
            {
                StartDate = j.StartDate.TimeOfDay
            }
            ).ToList();

            TimeSpan total = default(TimeSpan);

            var sortedDates = linq.OrderBy(x => x.StartDate );

            foreach (var dateTime in sortedDates)
            {
                total += dateTime.StartDate;
            }
            var time = TimeSpan.FromMilliseconds(total.TotalMilliseconds/sortedDates.Count());

            return new Response<TimeSpan>(time);    
        }
        catch (Exception ex)
        {
            return new Response<TimeSpan>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<GetJobHistoryDto>> InsertJobHistory(AddJobHistoryDto jobHistory)
    {
        try
        {    
            var newTodo = _mapper.Map<JobHistory>(jobHistory);
            _context.JobHistories.Add(newTodo);
            await _context.SaveChangesAsync();
            return new Response<GetJobHistoryDto>(_mapper.Map<GetJobHistoryDto>(newTodo));
        }
        catch (Exception ex)
        {
            return new Response<GetJobHistoryDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<AddJobHistoryDto>> UpdateJobHistory(AddJobHistoryDto jobHistory)
    {
        try
        {    
            var find = await _context.JobHistories.FindAsync(jobHistory.EmployeeId);
            find.EmployeeId = jobHistory.EmployeeId;
            find.StartDate = jobHistory.StartDate;
            find.EndDate = jobHistory.EndDate;
            find.JobId = jobHistory.JobId;
            var updated = await _context.SaveChangesAsync();
            return new Response<AddJobHistoryDto>(jobHistory);
        }
        catch (Exception ex)
        {
            return new Response<AddJobHistoryDto>(HttpStatusCode.InternalServerError, ex.Message);
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