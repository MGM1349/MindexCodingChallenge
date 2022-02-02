using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using challenge.Services;
using challenge.Models;
using Newtonsoft.Json;

namespace challenge.Controllers
{
    [Route("api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Employee employee)
        {
            _logger.LogDebug($"Received employee create request for '{employee.FirstName} {employee.LastName}'");

            _employeeService.Create(employee);

            return CreatedAtRoute("getEmployeeById", new { id = employee.EmployeeId }, employee);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }

        /// <summary>
        /// Marc Molnar
        /// Method that will get an ID then find an employee from that id.
        /// The number of direct reports from the employee will be counted up
        /// Then a reporting structure will be created using the number of direct reports and an employee.
        /// </summary>
        /// <param name="id">An Employee's ID</param>
        /// <returns>A Webpage</returns>
        [HttpGet("reporting/{id}")]
        public IActionResult GetEmployeeReportingStructure(String id)
        {
            var employee = _employeeService.GetById(id);

            if(employee == null)
            {
                return NotFound();
            }

            int directReports = _employeeService.CalculateDirectReports(employee);

            ReportingStructure structure = new ReportingStructure(employee, directReports);

            return Ok(structure);
        }

        /// <summary>
        /// Will search for a compensation entity in the compensation database then if it is not null
        /// the method will return the compensation entity to the webpage.
        /// </summary>
        /// <param name="id">string value that links back to an employee with a compensation</param>
        /// <returns>a compensation entity</returns>
        [HttpGet("compensation/{id}")]
        public IActionResult GetCompensation(String id)
        {
            var compensation = _employeeService.GetCompensationById(id);

            if(compensation == null)
            {
                return NotFound();
            }

            return Ok(compensation);
        }

        /// <summary>
        /// Will gather the compensation http post request and build out a compensation entity. 
        /// Then will add the compensation entity to a compensation database.
        /// </summary>
        /// <param name="compensation">Creates the compensation type from the post http request</param>
        /// <returns></returns>
        [HttpPost("compensation")]
        public IActionResult CreateCompensation([FromBody] Compensation compensation)
        {
            _employeeService.CreateCompensation(compensation);

            return CreatedAtRoute("getCompensationById", new { id = compensation.id }, compensation);
        }

        /// <summary>
        /// Marc Molnar
        /// I created this to test the compensation and show that it is working. This endpoint requires an id of an
        /// employee it will then create a compensation type. It will add the new compensation type to the compensation
        /// database where it will then retrieve that compensation using the id before displaying the compensation to the
        /// webpage. This was strictly created because I ran out of time to finish test methods after running out of time 
        /// on this challenge. 
        /// </summary>
        /// <param name="id">Is the id of an employee to create a compensation from</param>
        /// <returns>A webpage with the compensation type showing</returns>
        [HttpGet("compensation/create/{id}")]
        public IActionResult CreateAndReturnCompensation(String id)
        {
            //get the employee
            var employee = _employeeService.GetById(id);

            //create compensation
            var testCompensation = new Compensation()
            {
                employee = _employeeService.GetById(id),
                salary = 750000,
                effectiveDate = "1/23/2012",
                id = employee.EmployeeId
            };

            _employeeService.CreateCompensation(testCompensation);

            //find the newly added compensation type
            var compensation = _employeeService.GetCompensationById(id);

            if (compensation == null)
            {
                return NotFound();
            }

            //display the compensation type.
            return Ok(compensation);
        }
    }
}
