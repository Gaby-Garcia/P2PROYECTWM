using Proyect2TWM.Api.Dto;
using Proyect2TWM.Core.Entities;
using Proyect2TWM.Api.Repositories.Interfecies;
using Proyect2TWM.Api.Services.Interfaces;

namespace Proyect2TWM.Api.Services;

public class EmployeeService : IEmployeesService
{
     private readonly IEmployeesRepository _employeesRepository;
    
        public EmployeeService(IEmployeesRepository employeesRepository)
        {
            _employeesRepository = employeesRepository;
        }
    
        public async Task<bool> EmployeeExist(int id)
        {
            var employee = await _employeesRepository.GetByIdEmployee(id);
            return (employee != null);
        }
    
        public async Task<EmployeesDto> SaveAsycnE(EmployeesDto employeesDto)
        {
            var employee = new Employee
            {
                Name = employeesDto.Name,
                LastName = employeesDto.LastName,
                BirthDate = employeesDto.BirthDate,
                Gender = employeesDto.Gender,
                Address = employeesDto.Address,
                Phone = employeesDto.Phone,
                Email = employeesDto.Email,
                ID_Department = employeesDto.ID_Department,
                CreatedBy = "Gaby-Garcia",
                CreatedDate = DateTime.Now,
                UpdatedBy = "Gaby-Garcia",
                UpdatedDate = DateTime.Now
            };
            employee = await _employeesRepository.SaveAsycnEmployee(employee);
            employeesDto.id = employee.id;
    
            return employeesDto;
        }
    
        public async Task<EmployeesDto> UpdateAsyncE(EmployeesDto employeesDto)
        {
            var employee = await _employeesRepository.GetByIdEmployee(employeesDto.id);
            if (employee == null)
                throw new Exception("Employee Not Found");

            employee.Name = employeesDto.Name;
            employee.LastName = employeesDto.LastName;
            employee.BirthDate = employeesDto.BirthDate;
            employee.Gender = employeesDto.Gender;
            employee.Address = employeesDto.Address;
            employee.Phone = employeesDto.Phone;
            employee.Email = employeesDto.Email;
            employee.ID_Department = employeesDto.ID_Department;
            employee.UpdatedBy = "Gaby-Garcia";
            employee.UpdatedDate = DateTime.Now;
            
            await _employeesRepository.UpdateAsyncEmployee(employee);
            return employeesDto;
        }
    
        public async Task<List<EmployeesDto>> GetAllAsyncE()
        {
            var employee = await _employeesRepository.GetAllAsyncEmployee();
            var employeesDto = employee.Select(c => new EmployeesDto(c)).ToList();
            return employeesDto;
        }
    
        public async Task<bool> DeleteAsyncE(int id)
        {
            return await _employeesRepository.DeleteAsyncEmployee(id);
        }
    
        public async Task<EmployeesDto> GetByIdE(int id)
        {
            var employee = await _employeesRepository.GetByIdEmployee(id);
            if (employee == null)
                throw new Exception("Brand not found");
    
            var employeesDto = new EmployeesDto(employee);
            return employeesDto;
        }
        
        public async Task<bool> ExistByName(string name, int id = 0)
        {
            var employee = await _employeesRepository.GetByName(name, id);
            return employee != null;
        }
}