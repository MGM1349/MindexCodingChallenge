using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace challenge.Models
{
    /// <summary>
    /// Marc Molnar
    /// Holds an employee's salary and effective date.
    /// </summary>
    public class Compensation
    {
        //Holds the current employee
        public Employee employee { get; set; }
        
        //Holds the current employee's salary
        public float salary { get; set; }

        //Holds the current employee's effective date
        public string effectiveDate { get; set; }

        //this will act as the Key for a compensation class.
        //This will fix the error of 'entity type requires a primary key'
        public string id { get; set; }
    }
}
