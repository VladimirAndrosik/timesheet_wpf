using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using timesheet.data.Models;
using TimeTask = timesheet.data.Models.Task;
using Task = System.Threading.Tasks.Task;

namespace timesheet.data.Contracts
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetEmployees();
        Task<List<Timesheet>> GetEmployeeWeekTimesheets(int employeeId, DateTime firstDayOfTheWeek);
        Task<List<TimeTask>> GetTasks();
        Task UpdateTimesheets(List<Timesheet> timesheets);
        Task InsertTimesheets(List<Timesheet> timesheets);
        Task DeleteTimesheets(List<Timesheet> timesheets);
    }
}
