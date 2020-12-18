using System;
using System.Collections.Generic;
using System.Linq;
using timesheet.business.Helpers;
using timesheet.data;
using timesheet.model;
using ThreadTask = System.Threading.Tasks.Task;

namespace timesheet.business
{
    public class EmployeeService
    {
        public TimesheetDb Db { get; }

        public EmployeeService(TimesheetDb dbContext)
        {
            Db = dbContext;
        }

        public IQueryable<Employee> GetEmployees()
        {
            List<Employee> employess = Db.Employees.ToList();
            DateTime startDate = DateTimeHelper.StartOfWeek(DateTime.Today);
            DateTime lastDate = startDate.AddDays(7);
            try
            {
                foreach (Employee employee in employess)
                {
                    List<Timesheet> timesheets = Db.Timesheets.Where(t => t.EmployeeId == employee.Id).ToList();
                    if (timesheets.Any())
                    {
                        employee.Average = timesheets.GroupBy(t => t.StartDay).Select(g => new
                        {
                            Sum = g.Sum(t =>
                                t.FridayEffort + t.MondayEffort + t.SaturdayEffort + t.SundayEffort + t.ThursdayEffort +
                                t.TuesdayEffort + t.WednesdayEffort)
                        }).Average(t => t.Sum);

                        employee.Sum = timesheets.Where(t => t.StartDay >= startDate && t.StartDay < lastDate).Sum(
                            t =>
                                t.FridayEffort + t.MondayEffort + t.SaturdayEffort + t.SundayEffort + t.ThursdayEffort +
                                t.TuesdayEffort + t.WednesdayEffort);
                    }
                }
            }
            catch (Exception exception)
            {

            }
            return employess.AsQueryable();
        }

        public IQueryable<Timesheet> GetEmployeeWeekTimesheets(int employeeId, DateTime firstDayOfTheWeek)
        {
            DateTime lastDayOfTheWeek = firstDayOfTheWeek.AddDays(7);
            return Db.Timesheets.Where(t => t.EmployeeId == employeeId && t.StartDay >= firstDayOfTheWeek && t.StartDay < lastDayOfTheWeek);
        }

        public IQueryable<Task> GetEmployeeTasks()
        {
            return Db.Tasks;
        }

        public async ThreadTask UpdateTimesheets(IEnumerable<Timesheet> timesheets)
        {
            Db.Timesheets.UpdateRange(timesheets);
            await Db.SaveChangesAsync();
        }

        public async ThreadTask InsertTimesheets(IEnumerable<Timesheet> timesheets)
        {
            await Db.Timesheets.AddRangeAsync(timesheets);
            await Db.SaveChangesAsync();
        }

        public async ThreadTask DeleteTimesheets(IEnumerable<Timesheet> timesheets)
        {
            Db.Timesheets.RemoveRange(timesheets);
            await Db.SaveChangesAsync();
        }
    }
}
