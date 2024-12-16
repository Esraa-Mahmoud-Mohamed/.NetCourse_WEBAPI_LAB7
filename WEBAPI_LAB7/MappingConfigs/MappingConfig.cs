using AutoMapper;
using WEBAPI_LAB7.DTOs;
using WEBAPI_LAB7.Models;
namespace Day6.MppingConfigs
{
    public class MappingConfig:Profile
    {
        public MappingConfig()
        {
            CreateMap<Employee, AddEmployeeDTO>().ReverseMap();
            CreateMap<Employee, DisplayEmployeeDTO>().ReverseMap();
            //CreateMap<Employee, DisplayEmployeeDTO>().AfterMap(
            //    (src, dest) =>
            //    {
            //        dest.departmentName = src.dept.name;
            //    });
        }
    }
}
