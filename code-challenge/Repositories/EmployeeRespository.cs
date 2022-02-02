using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using challenge.Data;

namespace challenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;
        private readonly CompensationContext _compesnationContext;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext, CompensationContext compensationContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
            _compesnationContext = compensationContext;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            
            return employee;
        }

        public Employee GetById(string id)
        {
            _employeeContext.Employees.Include(e => e.DirectReports).ToList(); //Fixes the issue with lazy loading. Will now help load direct reports. Marc Molnar
            return _employeeContext.Employees.SingleOrDefault(e => e.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }

        /// <summary>
        /// Adds a compensation to the database
        /// </summary>
        /// <param name="compensation">An incoming compensation object to= be added to the database</param>
        /// <returns>the added compensation</returns>
        public Compensation Add(Compensation compensation)
        {
            compensation.id = Guid.NewGuid().ToString();
            _compesnationContext.compensations.Add(compensation);
            return compensation;
        }

        /// <summary>
        /// Method that will find a compensation that is the same as the incoming id
        /// </summary>
        /// <param name="id">The id used to look up a compensation in the database</param>
        /// <returns>A found compensation object</returns>
        public Compensation GetCompensationById(string id)
        {
            _compesnationContext.compensations.Include(e => e.employee).ToList(); //Fixes the issue with lazy loading. This will help with loading in the employee object. 
            _compesnationContext.compensations.Include(e => e.employee.DirectReports).ToList(); //Fixes the issue with lazy loading. Will now help load direct reports. 
            return _compesnationContext.compensations.SingleOrDefault(e => e.employee.EmployeeId == id);
        }

        //Saves any changes within the compensation database
        public Task SaveCompensation()
        {
            return _compesnationContext.SaveChangesAsync();
        }
    }
}
