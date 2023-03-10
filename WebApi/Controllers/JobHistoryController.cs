using Microsoft.AspNetCore.Mvc;
using Domain.Entities;
using Infrastructure.Services;
using Domain.Dtos;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class JobHistoryController{
    public readonly JobHistoryService _jobHistoryService;
    public JobHistoryController(JobHistoryService jobHistoryService)
    {
        _jobHistoryService = jobHistoryService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetJobHistoryDto>>> GetJobHistories(){
        return await _jobHistoryService.GetJobHistories();
    }
    [HttpGet("GetAverageById")]
    public async Task<Response<TimeSpan>> GetAvgById( int id, DateTime start, DateTime end){
        return await _jobHistoryService.GetAvgById(id, start, end);
    }
    [HttpPost("Add")]
    public async Task<Response<GetJobHistoryDto>> InsertJobHistory(AddJobHistoryDto jobHistory){
        return await _jobHistoryService.InsertJobHistory(jobHistory);
    }
    [HttpPut("Update")]
    public async Task<Response<AddJobHistoryDto>> UpdateJobHistory(AddJobHistoryDto jobHistory){
        return await _jobHistoryService.UpdateJobHistory(jobHistory);
    }
    [HttpDelete("Delete")]
    public async Task<Response<string>> DeleteJobHistory(int id){
        return await _jobHistoryService.DeleteJobHistory(id);
    }
}