using Domain.Entities;
using Domain.Dtos;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services;
namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]

public class EmployeeController{
    public readonly EmployeeService _employeeService;
    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet("GetAll")]
    public async Task<Response<List<GetEmployeeDto>>> GetEmployees(){
        return await _employeeService.GetEmployees();
    }
    [HttpGet("GetEmployeesByPhoneNumber")]
    public async Task<Response<List<GetEmployeeByPhone>>> GetEmployeesByPh( string num){
        return await _employeeService.GetEmployeesByPhoneNumber(num);
    }
    
    [HttpGet("GetEmployeeDayTime")]
    public async Task<Response<List<GetEmployeeDayTime>>> GetEmployeeDay(int id){
        return await _employeeService.GetEmployeeDayTime(id);
    }
    [HttpPost("Add")]
    public async Task<Response<GetEmployeeDto>> InsertEmployee(AddEmployeeDto employee){
        return await _employeeService.InsertEmployee(employee);
    }
    [HttpPut("Update")]
    public async Task<Response<AddEmployeeDto>> UpdateEmployee(AddEmployeeDto employee){
        return await _employeeService.UpdateEmployee(employee);
    }
    [HttpDelete("Delete")]
    public async Task<Response<string>> DeleteEmployee(int id){
        return await _employeeService.DeleteEmployee(id);
    }
}