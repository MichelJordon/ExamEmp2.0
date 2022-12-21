using System.Net;
using Domain.Dtos;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;

namespace Infrastructure.Services;
public class EmployeeService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    private readonly IWebHostEnvironment _hostEnvironment;
    public EmployeeService(DataContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;

    }
   public async Task<Response<List<GetEmployeeDto>>> GetEmployees()
    {
        try
        {    
            var list = _mapper.Map<List<GetEmployeeDto>>(_context.Employees.ToList());
            return new Response<List<GetEmployeeDto>>(list);
        }
        catch (Exception ex)
        {
            return new Response<List<GetEmployeeDto>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<List<GetEmployeeByPhone>>> GetEmployeesByPhoneNumber(string num)
    {
        try
        {    
            var emp = (
                from e in _context.Employees
                where e.PhoneNumber == num
                select new GetEmployeeByPhone
                {
                    EmployeeId = e.EmployeeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                }
                ).ToList();            
            return new Response<List<GetEmployeeByPhone>>(emp);
        }
        catch (Exception ex)
        {
            return new Response<List<GetEmployeeByPhone>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    public async Task<Response<List<GetEmployeeDayTime>>> GetEmployeeDayTime( int id )
    {
        try
        {    
            var getEmpDT = (
                from jh in _context.JobHistories
                join em in _context.Employees on jh.EmployeeId equals em.EmployeeId
                where jh.EmployeeId == id
                select new GetEmployeeDayTime
                {
                    EmployeeId = em.EmployeeId,
                    Day = $"Day - {jh.JobHistoryId}",
                    FirstName = em.FirstName,
                    LastName = em.LastName,
                    PhoneNumber = em.FirstName,
                    StartWork = jh.StartWork.ToString(),
                    EndWork = jh.EndWork.ToString()
                }
                ).ToList();
            return new Response<List<GetEmployeeDayTime>>(getEmpDT);
                
        }
        catch (Exception ex)
        {
            return new Response<List<GetEmployeeDayTime>>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    
    public async Task<Response<GetEmployeeDto>> InsertEmployee(AddEmployeeDto employee)
    {
        try
        {    
            var newTodo = _mapper.Map<Employee>(employee);
            _context.Employees.Add(newTodo);
            await _context.SaveChangesAsync();
            return new Response<GetEmployeeDto>(_mapper.Map<GetEmployeeDto>(newTodo));
        }
        catch (Exception ex)
        {
            return new Response<GetEmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    
    
    public async Task<Response<AddEmployeeDto>> UpdateEmployee(AddEmployeeDto employee)
    {
        try
        {    
            var find = await _context.Employees.FindAsync(employee.EmployeeId);
            find.FirstName = employee.FirstName;
            find.LastName = employee.LastName;
            find.Email = employee.Email;
            find.PhoneNumber = employee.PhoneNumber;
            find.HireDate = employee.HireDate;
            find.JobId = employee.JobId;
            var updated = await _context.SaveChangesAsync();
            return new Response<AddEmployeeDto>(employee);
        }
        catch (Exception ex)
        {
            return new Response<AddEmployeeDto>(HttpStatusCode.InternalServerError, ex.Message);
        }
    }
    public async Task<Response<string>> DeleteEmployee(int id)
    {
        try
        {
            var find = await _context.Employees.FindAsync(id);
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