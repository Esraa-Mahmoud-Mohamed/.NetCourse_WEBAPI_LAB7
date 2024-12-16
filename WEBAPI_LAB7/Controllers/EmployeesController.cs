﻿using Day6.MppingConfigs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WEBAPI_LAB7.DTOs;
using WEBAPI_LAB7.Models;
using WEBAPI_LAB7.UnitOfWorks;

namespace WEBAPI_LAB7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        //EmployeeRepository employeeRepository;
        //public EmployeesController(EmployeeRepository employeeRepository)
        //{
        //    this.employeeRepository = employeeRepository;
        //}

        UnitOfWork unitOfWork;
        IMapper mapper;
        public EmployeesController(UnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;   
            this.mapper = mapper; 
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            //List<Employee> emps = employeeRepository.SelectAll();
            List<Employee> emps = unitOfWork.empreps.SelectAll();
            //List<DisplayEmployeeDTO> empsDTO = new List<DisplayEmployeeDTO>();

            //foreach(Employee emp in emps)
            //{
            //    DisplayEmployeeDTO empDTO = new DisplayEmployeeDTO()
            //    {
            //        Name = emp.Name,
            //        Salary = (int)emp.Salary,
            //        Email = emp.Email,
            //        Username = emp.Username,
            //        HireDate = (DateOnly)emp.HireDate
            //    };
            //    empsDTO.Add(empDTO);
            //}

            List<DisplayEmployeeDTO> empsDTO = mapper.Map<List<DisplayEmployeeDTO>>(emps);

            if (empsDTO != null) return Ok(empsDTO);
            else return NotFound();
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) 
        {
            Employee emp = unitOfWork.empreps.SelectById(id);
            if (emp != null)
            {
                //DisplayEmployeeDTO empDTO = new DisplayEmployeeDTO()
                //{
                //    Name = emp.Name,
                //    Salary = (int)emp.Salary,
                //    Email = emp.Email,
                //    Username = emp.Username,
                //    HireDate = (DateOnly)emp.HireDate
                //};
                DisplayEmployeeDTO empDTO = mapper.Map<DisplayEmployeeDTO>(emp);
                return Ok(empDTO);
            }
            else return NotFound();
        }
        [HttpPost]
        public IActionResult Add(AddEmployeeDTO empDTO) 
        {
            if (ModelState.IsValid)
            {
                //Employee emp = new Employee()
                //{
                //    Name= empDTO.Name,
                //    Salary = empDTO.Salary,
                //    Email = empDTO.Email,
                //    Username = empDTO.Username,
                //    HireDate = empDTO.HireDate,
                //    Password = empDTO.Password,
                //    Photo = empDTO.Photo.FileName
                //};
                Employee emp = mapper.Map<Employee>(empDTO);
                unitOfWork.empreps.Add(emp);
                unitOfWork.save();
                return Ok(unitOfWork.empreps.SelectAll());
            }
            else
            {
                return BadRequest(ModelState);
            }

        }

    }
}
