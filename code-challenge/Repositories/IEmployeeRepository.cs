using challenge.Models;
using System;
using System.Threading.Tasks;

namespace challenge.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetById(String id);
        Employee Add(Employee employee);
        Employee Remove(Employee employee);
        Task SaveAsync();
        Compensation Add(Compensation compensation);
        Task SaveCompensation();
        Compensation GetCompensationById(string id);
    }
}