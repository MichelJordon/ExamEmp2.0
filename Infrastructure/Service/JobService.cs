using System.Net;
using AutoMapper;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Services;
public class JobService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public JobService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<List<GetJobDto>>> GetJobs()
    {
        try
        {
            var list = _mapper.Map<List<GetJobDto>>(_context.Jobs.ToList());
            return new Response<List<GetJobDto>>(list.ToList());
        }
        catch (Exception ex)
        {
            return new Response<List<GetJobDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<GetJobDto>> InsertJob(AddJobDto job)
    {
        try
        {
            var newTodo = _mapper.Map<Job>(job);
            _context.Jobs.Add(newTodo);
            await _context.SaveChangesAsync();
            return new Response<GetJobDto>(_mapper.Map<GetJobDto>(newTodo));
        }
        catch (Exception ex)
        {
            return new Response<GetJobDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }

    public async Task<Response<AddJobDto>> UpdateJob(AddJobDto job)
    {
        try
        {
            var find = await _context.Jobs.FindAsync(job.JobId);
            find.JobTitle = job.JobTitle;
            find.MinSalary = job.MinSalary;
            find.MaxSalary = job.MaxSalary;
            var updated = await _context.SaveChangesAsync();
            return new Response<AddJobDto>(job);
        }
        catch (Exception ex)
        {
            return new Response<AddJobDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<string>> DeleteJob(string id)
    {
        try
        {
            var find = await _context.Jobs.FindAsync(id);
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