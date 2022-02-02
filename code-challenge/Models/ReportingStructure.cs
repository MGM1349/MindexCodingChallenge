using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /// <summary>
    /// Marc Molnar
    /// This class will hold an employee and the amount
    /// of direct reports they have in a data structure
    /// </summary>
    public class ReportingStructure
    {
        //Holds the employee being looked up
        public Employee employee { get; set; }
        //Holds the employee's amount of direct reports
        public int numberOfReports { get; set; }

        //Constructor
        public ReportingStructure(Employee vEmployee, int vNumberOfReports)
        {
            employee = vEmployee;
            numberOfReports = vNumberOfReports;
        }
    }
}
