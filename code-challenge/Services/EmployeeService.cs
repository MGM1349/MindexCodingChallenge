using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Models;
using Microsoft.Extensions.Logging;
using challenge.Repositories;

namespace challenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        /// <summary>
        /// Marc Molnar
        /// This method will add a new compensation to the database
        /// </summary>
        /// <param name="compensation">A new compensation to be added to the compensation database</param>
        /// <returns>The added compensation</returns>
        public Compensation CreateCompensation(Compensation compensation)
        {
            if(compensation != null)
            {
                _employeeRepository.Add(compensation);
                _employeeRepository.SaveCompensation();
            }

            return compensation;
        }

        /// <summary>
        /// Marc Molnar
        /// This method will take in an id and go to the database to search a compensation
        /// object with the same id. If not this method will return null.
        /// </summary>
        /// <param name="id">The id that will be used to get a compensation object from the databse</param>
        /// <returns>A compensation object</returns>
        public Compensation GetCompensationById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetCompensationById(id);
            }

            return null;
        }

        /// <summary>
        /// Marc Molnar
        /// Method that will take an employee and loop their all their direct reports. 
        /// It will recurse through the current employees direct reports and count them up. 
        //// </summary>
        /// <param name="employee">Accepts an employee to view their direct reports</param>
        /// <returns>Value of total reports under a single employee</returns>
        public int CalculateDirectReports(Employee employee)
        {
            int directReports = 0;
            
            directReports += employee.DirectReports.Count;

            for (int i = 0; i < employee.DirectReports.Count; i++)
            {
                return directReports + CalculateDirectReports(employee.DirectReports[i]);
            }

            return 0; 
        }

    }
}
